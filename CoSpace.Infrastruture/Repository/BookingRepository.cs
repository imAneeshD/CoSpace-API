using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class BookingRepository(ApplicationDbContext dbContext, ICurrentUserService currentUserService, RepositoryBase<Booking> repositoryBase) : IBookingRepository
    {
        public async Task<Booking> AddBooking(Booking Booking)
        {
            repositoryBase.SetAuditFields(Booking, currentUserService.UserId, "INSERT");

            dbContext.Booking.Add(Booking);
            await dbContext.SaveChangesAsync();
            return Booking;
        }

        public async Task<bool> UpdateBooking(Booking Booking)
        {
            var existingBooking = await dbContext.Booking.FirstOrDefaultAsync(x => x.Id == Booking.Id);
            if (existingBooking != null)
            {
                repositoryBase.SetAuditFields(existingBooking, currentUserService.UserId, "UPDATE");

                existingBooking.RoomId = Booking.RoomId;
                existingBooking.UserId = Booking.UserId;
                existingBooking.StartTime= Booking.EndTime;
                existingBooking.OrganizationId= Booking.OrganizationId;

    

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteBooking(int id)
        {
            var existingBooking = await dbContext.Booking.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBooking is not null)
            {
                repositoryBase.SetAuditFields(existingBooking, currentUserService.UserId, "DELETE");

                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }
        public async Task<Booking> GetBookingById(int id)
        {
            var Booking = await dbContext.Booking.FirstOrDefaultAsync(x => x.Id == id);

            if (Booking is not null)
            {
                return Booking;
            }

            return null;
        }

        public async Task<IEnumerable<Booking>> GetBookings()
        {
            return await dbContext.Booking.ToListAsync();
        }

    }
}
