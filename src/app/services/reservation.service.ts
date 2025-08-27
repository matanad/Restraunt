import { HttpClient } from '@angular/common/http';
import { Injectable, signal, Signal, WritableSignal } from '@angular/core';
import { map, take } from 'rxjs';
import { NewReservation, Reservation } from '../Models/reservation.model';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  private API_KEY = 'https://localhost:44333/api/reservation' as const;
  public reservations$: WritableSignal<Reservation[]> = signal([]);

  constructor(private http: HttpClient) {}

  getReservations() {
    this.http
      .get<Reservation[]>(this.API_KEY)
      .pipe(take(1))
      .subscribe((reservations) => {
        this.reservations$.set(reservations);
        console.log(reservations)
      });
  }
  getReservationById(id: number) {
    return this.http.get(this.API_KEY + '/' + id).pipe(take(1));
  }
  deleteReservation(id: number) {
    this.http
      .delete(this.API_KEY + '/' + id)
      .pipe(take(1))
      .subscribe({ next: () => this.getReservations() });
  }
  createReservation(newReservation: NewReservation) {
    this.http
      .post(this.API_KEY, newReservation)
      .pipe(take(1))
      .subscribe({ next: () => this.getReservations() });
  }
  updateReservation(reservation: Reservation) {
    this.http
      .put(this.API_KEY, reservation)
      .pipe(take(1))
      .subscribe({ next: () => this.getReservations() });
  }
}
