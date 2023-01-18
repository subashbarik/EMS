import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { EmployeeService } from '../employee/employee.service';
import { IEmployee } from '../shared/models/employee';
import { IUser } from '../shared/models/user';
import { selectUser } from '../state/account/account.selectors';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  public currentUser$: Observable<IUser>;
  constructor(private store: Store) {}

  ngOnInit(): void {
    this.initializeValues();
  }
  initializeValues(): void {
    this.currentUser$ = this.store.pipe(select(selectUser));
  }
}
