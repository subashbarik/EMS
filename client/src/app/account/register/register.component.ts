import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Actions, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { Register } from 'src/app/shared/models/user';
import {
  registerUser,
  registerUserError,
  registerUserSuccess,
} from 'src/app/state/account/account.actions';
import { accountLoadingStatus } from 'src/app/state/account/account.selectors';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit, OnDestroy {
  public registerForm: FormGroup;
  public errors: string[];
  public registerUserSuccessSubscription = new Subscription();
  public registerUserFailureSubscription = new Subscription();
  public registerStatus$ = this.store.select(accountLoadingStatus);
  public registerInProgress = false;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private store: Store,
    private actions$: Actions
  ) {}

  ngOnInit(): void {
    this.createRegisterForm();
    this.registerUserSuccessSubscription = this.actions$
      .pipe(ofType(registerUserSuccess))
      .subscribe({
        next: (response) => {
          //check the user type and redirect to the proper page
          if (response.user) {
            this.router.navigate(['/main']);
          }
          // If there is any api response error then show it to the user
          this.registerInProgress = false;
        },
      });
    this.registerUserFailureSubscription = this.actions$
      .pipe(ofType(registerUserError))
      .subscribe({
        next: (response) => {
          this.errors = response.error.errors.Password || response.error.errors;
          this.registerInProgress = false;
        },
      });
  }
  createRegisterForm() {
    this.registerForm = this.fb.group({
      displayName: [null, [Validators.required]],
      email: [
        null,
        [
          Validators.required,
          Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
        ],
      ],
      password: [null, Validators.required],
    });
  }
  ngOnDestroy(): void {
    this.registerUserSuccessSubscription.unsubscribe();
  }
  onSubmit() {
    let register = new Register(
      this.registerForm.value.displayName,
      this.registerForm.value.email,
      this.registerForm.value.password
    );
    this.store.dispatch(registerUser({ register: register }));
  }
}
