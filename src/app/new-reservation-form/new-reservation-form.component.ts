import { Component, effect, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReservationService } from '../services/reservation.service';
import { NewReservation } from '../Models/reservation.model';
import { TableService } from '../services/table.service';
import { Table } from '../Models/table.model';

@Component({
  selector: 'app-new-reservation-form',
  standalone: false,
  templateUrl: './new-reservation-form.component.html',
  styleUrl: './new-reservation-form.component.scss',
})
export class NewReservationFormComponent implements OnInit {
  reservationForm!: FormGroup;
  tables: Table[] = [];

  constructor(
    private fb: FormBuilder,
    private reservationService: ReservationService,
    private tableService: TableService
  ) {
    effect(() => {
      this.tables = this.tableService.tables$();
    });
  }

  ngOnInit(): void {
    this.reservationForm = this.fb.group({
      tableNumber: [null, Validators.required],
      customerName: ['', Validators.required],
      reservationTime: [null, Validators.required],
      guests: [1, [Validators.required, Validators.min(1)]],
    });
    this.tableService.getTables();
  }

  submit() {
    if (this.reservationForm.invalid) return;

    const newReservation: NewReservation = this.reservationForm.value;
    this.reservationService.createReservation(newReservation);

    this.reservationForm.reset({ guests: 1 });
  }
}
