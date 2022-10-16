import { createSelector } from '@ngrx/store';
import { IAppState } from '../app.state';
import { IGlobalState } from './appglobal.reducer';

//select the department state from the app state
export const globalState = (state: IAppState) => state.globalState;

//selector for all departments
export const selectGlobal = createSelector(
  globalState,
  (state: IGlobalState) => state.global
);

//status selector
export const globalLoadingStatus = createSelector(
  globalState,
  (state: IGlobalState) => state.status
);
