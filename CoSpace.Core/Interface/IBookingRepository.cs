using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface IBookingRepository
    {
        Task<Booking> AddBooking(Booking Booking);
        Task<bool> UpdateBooking(Booking Booking);
        Task<bool> DeleteBooking(int id);
        Task<Booking> GetBookingById(int id);
        Task<IEnumerable<Booking>> GetBookings();
    }
}
