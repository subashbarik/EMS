import { createAction, props } from '@ngrx/store';
import { IDesignation } from 'src/app/shared/models/designation';

export const loadDesignations = createAction('[designation list] Load');
export const loadDesignationsSuccess = createAction(
  '[designation list] Success',
  props<{ designations: IDesignation[] }>()
);
