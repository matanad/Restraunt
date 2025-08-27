using backend.Models;

namespace backend.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation> AddReservationAsync(NewReservation reservation);
        Task DeleteReservationAsync(int id);
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<Reservation?> GetReservationByIdAsync(int id);
        Task<Reservation> UpdateReservationAsync(int id, Reservation updatedReservation);
    }
}