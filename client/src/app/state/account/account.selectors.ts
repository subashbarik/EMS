import { createSelector } from '@ngrx/store';
import { IAppState } from '../app.state';
import { IAccountState } from './account.reducer';

export const accountState = (state: IAppState) => state.accState;
export const selectUser = createSelector(
  accountState,
  (state: IAccountState) => state.user
);
//status selector
export const accountLoadingStatus = createSelector(
  accountState,
  (state: IAccountState) => state.status
);
