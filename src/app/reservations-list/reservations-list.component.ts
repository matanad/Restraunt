import { Component, effect, Input, OnInit } from '@angular/core';
import { Reservation } from '../Models/reservation.model';
import { ReservationService } from '../services/reservation.service';

@Component({
  selector: 'app-reservations-list',
  standalone: false,
  templateUrl: './reservations-list.component.html',
  styleUrl: './reservations-list.component.scss',
})
export class ReservationsListComponent implements OnInit {
  @Input() tableId!: number;
  reservations: Reservation[] = [];

  constructor(private reservationService: ReservationService) {
    effect(() => {
      this.reservations = this.reservationService
        .reservations$()
        .filter((reservation) => reservation.tableId === this.tableId);
    });
  }

  ngOnInit(): void {
    this.reservationService.getReservations();
  }

  onDelete(id: number) {
    this.reservationService.deleteReservation(id);
  }
}
