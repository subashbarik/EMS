import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import {
  logOutUser,
  logOutUserSuccess,
} from 'src/app/state/account/account.actions';
import { selectUser } from 'src/app/state/account/account.selectors';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  public currentUser$ = this.store.select(selectUser);
  public logoutUserSuccessSubscription = new Subscription();

  constructor(
    private store: Store,
    private router: Router,
    private actions$: Actions
  ) {}

  ngOnInit(): void {
    this.setupSubscriptions();
  }
  ngOnDestroy(): void {
    this.logoutUserSuccessSubscription.unsubscribe();
  }
  logout() {
    this.store.dispatch(logOutUser());
  }
  setupSubscriptions() {
    this.logoutUserSuccessSubscription = this.actions$
      .pipe(ofType(logOutUserSuccess))
      .subscribe({
        next: (response) => {
          if (response) {
            this.router.navigate(['/account/login']);
          }
        },
        error: (error) => {},
      });
  }
}
