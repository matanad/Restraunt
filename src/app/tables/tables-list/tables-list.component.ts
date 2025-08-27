import { Component, OnInit, Signal, signal } from '@angular/core';
import { TableService } from '../../services/table.service';
import { Table } from '../../Models/table.model';

@Component({
  selector: 'app-tables-list',
  standalone: false,
  templateUrl: './tables-list.component.html',
  styleUrl: './tables-list.component.scss',
})
export class TablesListComponent implements OnInit {
  tables$!: Signal<Table[]>;

  constructor(private tableService: TableService) {}
  ngOnInit(): void {
    this.tables$ = this.tableService.tables$;
    this.tableService.getTables();
  }
}
