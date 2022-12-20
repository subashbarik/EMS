import {
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Actions, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { Login } from 'src/app/shared/models/user';
import {
  loginUser,
  loginUserError,
  loginUserSuccess,
} from 'src/app/state/account/account.actions';
import { accountLoadingStatus } from 'src/app/state/account/account.selectors';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit, OnDestroy {
  public signInProgress = false;
  public loginForm: FormGroup;
  public loginUserSuccessSubscription = new Subscription();
  public loginUserFailureSubscription = new Subscription();

  public loginStatus$ = this.store.select(accountLoadingStatus);
  @ViewChild('rememberme', { static: false }) rememberMe: ElementRef;
  constructor(
    private fb: FormBuilder,
    private store: Store,
    private router: Router,
    private actions$: Actions,
    private accService: AccountService
  ) {}

  ngOnInit(): void {
    this.createLoginForm();
    this.loginUserSuccessSubscription = this.actions$
      .pipe(ofType(loginUserSuccess))
      .subscribe({
        next: (response) => {
          //check the user type and redirect to the proper page
          if (response.user) {
            if (this.rememberMe.nativeElement.checked) {
              this.accService.setJwtTokenInLocalStorage(response.user.token);
            }
            this.router.navigate(['/main']);
          }
          this.signInProgress = false;
          // If there is any api response error then show it to the user
        },
      });
    this.loginUserFailureSubscription = this.actions$
      .pipe(ofType(loginUserError))
      .subscribe({
        next: (response) => {
          this.signInProgress = false;
        },
      });
  }
  ngOnDestroy(): void {
    this.loginUserSuccessSubscription.unsubscribe();
    this.loginUserFailureSubscription.unsubscribe();
  }
  createLoginForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [
        Validators.required,
        Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
      ]),
      password: new FormControl('', Validators.required),
    });
  }
  onSubmit() {
    this.signInProgress = true;
    let login = new Login(
      this.loginForm.value.email,
      this.loginForm.value.password
    );
    this.store.dispatch(loginUser({ login: login }));
  }
}
