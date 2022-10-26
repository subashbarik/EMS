import { createAction, props } from '@ngrx/store';
import { ILogin, IRegister, IUser } from 'src/app/shared/models/user';

export const loginUser = createAction(
  '[account] login',
  props<{ login: ILogin }>()
);
export const loginUserSuccess = createAction(
  '[account] login success',
  props<{ user: IUser }>()
);
export const logOutUser = createAction('[account] logout');
export const logOutUserSuccess = createAction(
  '[account] logout success',
  props<{ success: boolean }>()
);
export const registerUser = createAction(
  '[account] register',
  props<{ register: IRegister }>()
);
export const registerUserSuccess = createAction(
  '[account] register success',
  props<{ user: IUser }>()
);
export const registerUserError = createAction(
  '[account] register error',
  props<{ error: any }>()
);
export const loadUser = createAction(
  '[account] load user',
  props<{ token: string }>()
);
export const loadUserSuccess = createAction(
  '[account] load user success',
  props<{ user: IUser }>()
);
