import { createReducer, on } from '@ngrx/store';
import { IDesignation } from 'src/app/shared/models/designation';
import {
  loadDesignations,
  loadDesignationsSuccess,
} from './designation.actions';

export interface IDesignationState {
  designations: IDesignation[];
  status: string;
  error: string;
}

export const initialState: IDesignationState = {
  designations: [],
  status: '',
  error: '',
};

export const designationReducer = createReducer(
  initialState,
  on(loadDesignations, (state) => ({
    ...state,
    status: 'loading',
  })),
  on(loadDesignationsSuccess, (state, { designations }) => ({
    ...state,
    designations: designations,
    status: 'success',
    error: '',
  }))
);
