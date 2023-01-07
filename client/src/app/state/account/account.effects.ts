import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { from, of } from 'rxjs';
import { switchMap, map, catchError, tap } from 'rxjs/operators';
import { AccountService } from 'src/app/account/account.service';
import {
  loadUser,
  loadUserSuccess,
  loginUser,
  loginUserError,
  loginUserSuccess,
  logOutUser,
  logOutUserSuccess,
  registerUser,
  registerUserError,
  registerUserSuccess,
} from './account.actions';

@Injectable()
export class AccountEffects {
  constructor(
    private actions$: Actions,
    private accService: AccountService,
    private router: Router
  ) {}

  //run this code when loginUser action is dispatched
  loginUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loginUser),
      switchMap((request) =>
        from(this.accService.login(request.login)).pipe(
          map((response: any) =>
            loginUserSuccess({ user: response, login: request.login })
          ),
          catchError((error) => of(loginUserError({ error: error })))
        )
      )
    )
  );

  redirectAfterloginSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(loginUserSuccess),
        tap((response) => {
          if (response.user && response.login.rememberMe) {
            this.accService.setJwtTokenInLocalStorage(response.user.token);
          }
          this.router.navigate(['/main']);
        })
      ),
    { dispatch: false }
  );

  // loginUserTest$ = createEffect(() =>
  //   this.actions$.pipe(
  //     ofType(loginUser),
  //     switchMap((request) =>{
  //         return this.accService.login(request.login).pipe(
  //           map((response: any) =>{
  //               return loginUserSuccess({ user: response })
  //             }),
  //             catchError((error) =>{
  //               return of(loginUserError({ error: error }))
  //             })
  //           }
  //         )
  //       )
  //     );

  //run this code when logout action is dispatched
  logoutUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(logOutUser),
      switchMap((request) =>
        from(this.accService.logout()).pipe(
          map((response: boolean) => logOutUserSuccess({ success: response }))
        )
      )
    )
  );

  //run this code when registerUser action is dispatched
  registerUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(registerUser),
      switchMap((request) =>
        from(this.accService.register(request.register)).pipe(
          map((response: any) => registerUserSuccess({ user: response })),
          catchError((error) => of(registerUserError({ error: error })))
        )
      )
    )
  );

  redirectAfterRegisterSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(registerUserSuccess),
        tap((response) => {
          if (response.user) {
            this.router.navigate(['/main']);
          }
        })
      ),
    { dispatch: false }
  );

  //run this code when loadUser action is dispatched
  loadUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadUser),
      switchMap((request) =>
        from(this.accService.loadCurrentUser(request.token)).pipe(
          map((response: any) => loadUserSuccess({ user: response }))
        )
      )
    )
  );
}
