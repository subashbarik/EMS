import { createReducer, on } from '@ngrx/store';
import { IDepartment } from 'src/app/shared/models/department';
import { loadDepartments, loadDepartmentsSuccess } from './department.actions';

export interface IDepartmentState {
  departments: IDepartment[];
  status: string;
  error: string;
}

export const initialState: IDepartmentState = {
  departments: [],
  status: '',
  error: '',
};

export const departmentReducer = createReducer(
  initialState,
  on(loadDepartments, (state) => ({
    ...state,
    status: 'loading',
  })),
  on(loadDepartmentsSuccess, (state, { departments }) => ({
    ...state,
    departments: departments,
    status: 'success',
    error: '',
  }))
);
