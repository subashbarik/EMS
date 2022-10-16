import { createSelector } from '@ngrx/store';
import { IAppState } from '../app.state';
import { IDesignationState } from './designation.reducer';

//select the designation state from the app state
export const designationState = (state: IAppState) => state.desigState;

//selector for all designations
export const selectAllDesignations = createSelector(
  designationState,
  (state: IDesignationState) => state.designations
);

//status selector
export const designationLoadingStatus = createSelector(
  designationState,
  (state: IDesignationState) => state.status
);
