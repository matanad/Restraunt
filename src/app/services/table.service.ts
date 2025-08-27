import { HttpClient } from '@angular/common/http';
import { Injectable, signal, Signal, WritableSignal } from '@angular/core';
import { map, take } from 'rxjs';
import { Table } from '../Models/table.model';

@Injectable({
  providedIn: 'root',
})
export class TableService {
  private API_KEY = 'https://localhost:44333/api/table' as const;
  public tables$: WritableSignal<Table[]> = signal([]);

  constructor(private http: HttpClient) {}

  getTables() {
    this.http
      .get<Table[]>(this.API_KEY)
      .pipe(take(1))
      .subscribe((tables) => this.tables$.set(tables));
  }
}
