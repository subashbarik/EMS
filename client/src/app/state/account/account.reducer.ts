import { createReducer, on } from '@ngrx/store';
import { IApiValidationErrorResponse } from 'src/app/shared/models/apierrorresponse';
import { IUser } from 'src/app/shared/models/user';
import {
  loadUser,
  loadUserSuccess,
  loginUser,
  loginUserSuccess,
  logOutUser,
  logOutUserSuccess,
  registerUser,
  registerUserError,
  registerUserSuccess,
} from './account.actions';

export interface IAccountState {
  user: IUser;
  status: string;
  error: string;
  apiErrorResponse: IApiValidationErrorResponse;
}

export const initialState: IAccountState = {
  user: null,
  status: '',
  error: '',
  apiErrorResponse: null,
};

export const accountReducer = createReducer(
  initialState,
  on(loginUser, (state) => ({
    ...state,
    status: 'logging',
    error: '',
    apiErrorResponse: null,
  })),
  on(loginUserSuccess, (state, { user: user }) => ({
    ...state,
    user: user,
    status: 'success',
    error: '',
    apiErrorResponse: null,
  })),
  on(logOutUser, (state) => ({
    ...state,
    status: 'logging out',
    error: '',
    apiErrorResponse: null,
  })),
  on(logOutUserSuccess, (state, { success: success }) => ({
    ...state,
    user: null,
    status: 'success',
    error: '',
    apiErrorResponse: null,
  })),
  on(registerUser, (state) => ({
    ...state,
    status: 'registering',
    error: '',
    apiErrorResponse: null,
  })),
  on(registerUserSuccess, (state, { user: user }) => ({
    ...state,
    user: user,
    status: 'success',
    error: '',
    apiErrorResponse: null,
  })),
  on(registerUserError, (state, { error: error }) => ({
    ...state,
    status: 'success',
    error: '',
    apiErrorResponse: error,
  })),
  on(loadUser, (state) => ({
    ...state,
    status: 'loading',
    error: '',
    apiErrorResponse: null,
  })),
  on(loadUserSuccess, (state, { user: user }) => ({
    ...state,
    user: user,
    status: 'success',
    error: '',
    apiErrorResponse: null,
  }))
);
