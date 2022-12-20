import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { from, of } from 'rxjs';
import { switchMap, map, catchError } from 'rxjs/operators';
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
  constructor(private actions$: Actions, private accService: AccountService) {}

  //run this code when loginUser action is dispatched
  loginUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loginUser),
      switchMap((request) =>
        from(this.accService.login(request.login)).pipe(
          map((response: any) => loginUserSuccess({ user: response })),
          catchError((error) => of(loginUserError({ error: error })))
        )
      )
    )
  );

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
