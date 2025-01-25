namespace CoSpace.Core.Entities
{
    public class Activity : Base
    {
        public string ActivityType { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime CreatedAt { get; set; } 
        public int UserId { get; set; }
        public string Icon { get; set; } = "";

        public virtual User User { get; set; }
    }

    public class DashboardStats
    {
        public int TotalAdmins { get; set; }
        public int AdminsChange { get; set; }
        public int TotalOrganizations { get; set; }
        public int OrganizationsChange { get; set; }
        public int ActiveBookings { get; set; }
        public int ActiveBookingsChange { get; set; }
        public int OpenTickets { get; set; }
        public int OpenTicketsChange { get; set; }
        public int[] BookingTrends { get; set; }
        public int[] RoomUtilization { get; set; }
        public List<Activity> RecentActivities { get; set; }
        public KeyMetrics KeyMetrics { get; set; }
    }

    public class KeyMetrics
    {
        public string AverageBookingDuration { get; set; } = "";
        public string PeakBookingTime { get; set; } = "";
        public string MostBookedRoom { get; set; } = "";
        public string TicketResolutionTime { get; set; } = "";
    }
}
