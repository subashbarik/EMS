import { createSelector } from '@ngrx/store';
import { IAppState } from '../app.state';
import { IDepartmentState } from './department.reducer';

//select the department state from the app state
export const departmentState = (state: IAppState) => state.deptState;

//selector for all departments
export const selectAllDepartments = createSelector(
  departmentState,
  (state: IDepartmentState) => state.departments
);

//status selector
export const departmentLoadingStatus = createSelector(
  departmentState,
  (state: IDepartmentState) => state.status
);
