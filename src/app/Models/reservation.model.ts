import { Table } from './table.model';

export interface Reservation {
  id: number;
  customerName: string;
  reservationTime: Date;
  tableId: number;
}

export interface NewReservation {
  customerName: string;
  reservationTime: Date;
  table: Table;
}