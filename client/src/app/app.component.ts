import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { AccountService } from './account/account.service';
import { loadUser, loadUserSuccess } from './state/account/account.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'client';
  public loadUserSuccessSubscription = new Subscription();
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
  }
  loadCurrentUser() {
    const token = localStorage.getItem('token');
    this.store.dispatch(loadUser({ token: token }));
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
        error: (error) => {
          console.log(error);
          this.router.navigate(['/account/login']);
        },
      });
  }
}
