import { createAction, props } from '@ngrx/store';
import { IDepartment } from 'src/app/shared/models/department';

export const loadDepartments = createAction('[department list] Load');
export const loadDepartmentsSuccess = createAction(
  '[department list] Success',
  props<{ departments: IDepartment[] }>()
);
