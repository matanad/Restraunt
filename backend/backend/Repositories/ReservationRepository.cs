using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly TableContext _context;
        public ReservationRepository(TableContext context)
        {
            _context = context;
        }
        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await Task.FromResult(_context.Reservations.ToList());
        }
        public async Task<Reservation> AddReservationAsync(NewReservation reservation)
        {
            var table = await _context.Tables.FirstOrDefaultAsync(t => t.Number == reservation.TableNumber);

            if (table == null)
            {
                throw new InvalidOperationException("Table doesnt exist.");
            }

            var reservationDate = reservation.ReservationTime.Date;
            var minTime = reservation.ReservationTime.AddMinutes(-30);
            var maxTime = reservation.ReservationTime.AddMinutes(30);

            var dayStart = reservationDate;
            var dayEnd = reservationDate.AddDays(1).AddTicks(-1);

            var reservations = _context.Reservations
                .Where(r => r.TableId == table.Id
                            && r.ReservationTime >= minTime
                            && r.ReservationTime <= maxTime
                            && r.ReservationTime >= dayStart
                            && r.ReservationTime <= dayEnd)
                .ToList();

            if (reservations.Any())
            {
                throw new InvalidOperationException("Table is already reserved for this time");
            }

            var newReservation = new Reservation
            {
                CustomerName = reservation.CustomerName,
                ReservationTime = reservation.ReservationTime,
                TableId = table.Id
            };


            var entity = _context.Reservations.Add(newReservation);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }
        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id).AsTask();
        }
        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found");
            }
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

        }
        public async Task<Reservation> UpdateReservationAsync(int id, Reservation updatedReservation)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found");
            }

            if (reservation.TableId != updatedReservation.TableId ||
               reservation.ReservationTime != updatedReservation.ReservationTime)
            {
                var conflictingReservations = _context.Reservations
                    .Where(r => r.TableId == updatedReservation.TableId &&
                                Math.Abs((r.ReservationTime - updatedReservation.ReservationTime).TotalMinutes) < 30 &&
                                r.Id != id)
                    .ToList();
                if (conflictingReservations.Any())
                {
                    throw new InvalidOperationException("Table is already reserved for this time");
                }
            }
            reservation.TableId = updatedReservation.TableId;
            reservation.CustomerName = updatedReservation.CustomerName;
            reservation.ReservationTime = updatedReservation.ReservationTime;

            await _context.SaveChangesAsync();

            return reservation;
        }
    }
}
