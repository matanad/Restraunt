import { Component, Input } from '@angular/core';
import { Table } from '../../Models/table.model';

@Component({
  selector: 'app-table',
  standalone: false,
  templateUrl: './table.component.html',
  styleUrl: './table.component.scss',
})
export class TableComponent {
  @Input() table!: Table;
}
