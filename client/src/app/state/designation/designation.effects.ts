import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { from, map, switchMap } from 'rxjs';
import { DesignationService } from 'src/app/designation/designation.service';
import {
  loadDesignations,
  loadDesignationsSuccess,
} from './designation.actions';

@Injectable()
export class DesignationEffect {
  constructor(
    private actions$: Actions,
    private designationService: DesignationService
  ) {}

  //run this code when loadDesignations action is dispatched
  loadEmployee$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadDesignations),
      switchMap(() =>
        // Call ddsignation service as a side effect and return designations
        from(this.designationService.getDesignations()).pipe(
          // Take the returned value and return a new success action containing the designations
          map((response: any) =>
            loadDesignationsSuccess({ designations: response.data })
          )
        )
      )
    )
  );
}
