using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class AdminRepository(ApplicationDbContext dbContext, RepositoryBase<Admin> repositoryBase, ICurrentUserService currentAdminService) : IAdminRepository
    {
        public async Task<Admin> AddAdmin(Admin Admin)
        {
            repositoryBase.SetAuditFields(Admin, currentAdminService.UserId, "INSERT");

            dbContext.Admin.Add(Admin);

            await dbContext.SaveChangesAsync();

            return Admin;
        }

        public async Task<bool> UpdateAdmin(Admin Admin)
        {
            var existingAdmin = await dbContext.Admin.FirstOrDefaultAsync(x => x.Id == Admin.Id);

            if (existingAdmin is not null)
            {
                repositoryBase.SetAuditFields(existingAdmin, currentAdminService.UserId, "UPDATE");

                existingAdmin.FirstName = Admin.FirstName;
                existingAdmin.LastName = Admin.LastName;
                existingAdmin.Email = Admin.Email;
                existingAdmin.Password = Admin.Password;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAdmin(Admin existingAdmin)
        {
            repositoryBase.SetAuditFields(existingAdmin, currentAdminService.UserId, "DELETE");

            existingAdmin.IsDeleted = true;

            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Admin> GetAdminById(int id)
        {
            return await dbContext.Admin.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Admin>> GetAdmins()
        {
            return await dbContext.Admin.ToListAsync();
        }

        public async Task<Admin> Login(string email, string password)
        {
            var result = await dbContext.Admin.FirstOrDefaultAsync(x => x.Email == email && x.Password == password && x.IsDeleted == false);
            if (result is not null)
            {
                return result;
            }
            return null;
        }

        public Task<DashboardStats> GetAdminStats()
        {
            var lastMonth = DateTime.UtcNow.AddMonths(-1);


            DashboardStats dashboardStats = new DashboardStats();
            dashboardStats.TotalAdmins = dbContext.Admin.Count();
            dashboardStats.TotalOrganizations = dbContext.Organization.Count();
            dashboardStats.ActiveBookings = dbContext.Booking.Count();
            dashboardStats.OpenTickets = dbContext.HelpRequest.Count(t => t.Status == "Open");

            var addedOrganizations = dbContext.Organization.Count(o => o.CreatedDate >= lastMonth && !o.IsDeleted);
            var deletedOrganizations = dbContext.Organization.Count(o => o.CreatedDate < lastMonth && o.IsDeleted);
            var addedAdmins = dbContext.Admin.Count(a => a.CreatedDate >= lastMonth && !a.IsDeleted);
            var deletedAdmins = dbContext.Admin.Count(a => a.CreatedDate < lastMonth && a.IsDeleted);


            dashboardStats.OrganizationsChange = (addedOrganizations + deletedOrganizations) == 0 ? 0 : addedOrganizations + deletedOrganizations;

            dashboardStats.AdminsChange = (addedAdmins + deletedAdmins) == 0 ? 0 : addedAdmins + deletedAdmins;

            dashboardStats.ActiveBookingsChange = dbContext.Booking.Count(b => b.Status == "Confirmed" && b.CreatedDate >= lastMonth) - dbContext.Booking.Count(b => b.Status == "Confirmed" && b.CreatedDate < lastMonth);

            dashboardStats.OpenTicketsChange = dbContext.HelpRequest.Count(t => t.Status == "Open" && t.CreatedDate >= lastMonth) - dbContext.HelpRequest.Count(t => t.Status == "Open" && t.CreatedDate < lastMonth);

            dashboardStats.BookingTrends = dbContext.Booking.Where(b => b.CreatedDate >= lastMonth).GroupBy(b => b.CreatedDate.Date).Select(g => g.Count()).ToArray();

            dashboardStats.RoomUtilization = dbContext.Booking.Where(b => b.CreatedDate >= lastMonth).GroupBy(b => b.RoomId).Select(g => g.Count()).ToArray();

            dashboardStats.RecentActivities = dbContext.Activity.Include(a => a.User).OrderByDescending(a => a.CreatedAt).Take(5).ToList();


            var averageBookingDuration = dbContext.Booking
            .AsEnumerable()
            .Select(b => (b.EndTime - b.StartTime).TotalMinutes)
            .DefaultIfEmpty(0) // Return 0 if the sequence is empty
            .Average();


            // Convert to hours and minutes
            var hours = (int)(averageBookingDuration / 60);
            var minutes = (int)(averageBookingDuration % 60);
            string averageBookingDurationStr = (hours == 0 && minutes == 0)
                ? "NA"
                : $"{hours}h {minutes}m";

            var peakBookingTimeQuery = dbContext.Booking
             .AsEnumerable() // Fetch the data into memory
             .GroupBy(b => new { Hour = b.StartTime.Hour, Period = b.StartTime.Hour < 12 ? "AM" : "PM" }) // Group by hour and AM/PM period
             .OrderByDescending(g => g.Count()) // Order by the most frequent hour
             .FirstOrDefault(); // Get the most booked hour


            var peakTime = "";

            // Check if there is a group, and get the peak hour
            if (peakBookingTimeQuery != null)
            {
                var hour = peakBookingTimeQuery.Key.Hour;
                var period = peakBookingTimeQuery.Key.Period;

                peakTime = $"{hour % 12}:{peakBookingTimeQuery.First().StartTime.Minute:D2} {period}"; // Format the time (12-hour format)
                                                                                                           // peakTime will give you something like "3:00 PM" or "10:30 AM"
            }

            var mostBookedRoomGroup = dbContext.Booking.Include(x => x.Room)
              .AsEnumerable() // Fetch the data into memory
              .GroupBy(r => r.Room.Name) // Group by room name in memory
              .OrderByDescending(g => g.Count()) // Order by the most frequent room
              .FirstOrDefault(); // Get the room with the highest booking count

            // Get the most booked room name (or a default value if no bookings exist)
            string mostBookedRoom = mostBookedRoomGroup?.Key ?? "No bookings";


            var ticketResolutionTime = dbContext.HelpRequest
                .AsEnumerable() // Fetch the data into memory
                .Select(t => (t.ResolvedAt - t.CreatedDate).TotalMinutes) // Calculate the duration in minutes in memory
                .DefaultIfEmpty(0) // Return 0 if the sequence is empty
                .Average(); // Calculate the average of the durations


            // Convert to hours and minutes
            var resolutionHours = (int)(ticketResolutionTime / 60);
            var resolutionMinutes = (int)(ticketResolutionTime % 60);
            string ticketResolutionTimeStr = (resolutionHours == 0 && resolutionMinutes == 0)
                ? "NA"
                : $"{resolutionHours}h {resolutionMinutes}m";


            dashboardStats.KeyMetrics = new KeyMetrics
            {
                AverageBookingDuration = averageBookingDurationStr, // e.g., "1h 30m"
                PeakBookingTime = peakTime, // e.g., "10:00 AM"
                MostBookedRoom = mostBookedRoom ?? "NA", // e.g., "Room 1"
                TicketResolutionTime = ticketResolutionTimeStr // e.g., "2h 30m"
            };

            return Task.FromResult(dashboardStats);
        }
    }
}
