import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { AccountService } from './account/account.service';
import {
  loadUser,
  loadUserError,
  loadUserSuccess,
} from './state/account/account.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'client';
  public loadUserSuccessSubscription = new Subscription();
  public loadUserErrorSubscription = new Subscription();
  constructor(
    private store: Store,
    private actions: Actions,
    private router: Router,
    private accService: AccountService
  ) {}

  ngOnInit(): void {
    this.setupSubscription();
    this.loadCurrentUser();
  }
  ngOnDestroy(): void {
    this.loadUserSuccessSubscription.unsubscribe();
    this.loadUserErrorSubscription.unsubscribe();
  }
  loadCurrentUser() {
    const token = localStorage.getItem('token');
    if (token !== null) {
      this.store.dispatch(loadUser({ token: token }));
    } else {
      this.router.navigate(['/account/login']);
    }
  }
  setupSubscription() {
    this.loadUserSuccessSubscription = this.actions
      .pipe(ofType(loadUserSuccess))
      .subscribe({
        next: (response) => {
          if (response.user) {
            this.accService.setJwtTokenInLocalStorage(response.user.token);
            this.router.navigate(['/main/home']);
          } else {
            this.router.navigate(['/account/login']);
          }
        },
      });

    this.loadUserErrorSubscription = this.actions
      .pipe(ofType(loadUserError))
      .subscribe({
        next: (response) => {
          if (response) {
            // Error can happen becasue of token expiration, so remove it.
            localStorage.removeItem('token');
            this.router.navigate(['/account/login']);
          }
        },
      });
  }
}
