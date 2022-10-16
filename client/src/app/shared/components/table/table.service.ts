import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TableService {
  private rowClickedSubject = new BehaviorSubject<number>(-1);
  public rowClicked$ = this.rowClickedSubject.asObservable();
  constructor() {}
  onRowClicked(id: number) {
    this.rowClickedSubject.next(id);
  }
}
