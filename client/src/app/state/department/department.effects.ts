import { Actions, createEffect, ofType } from '@ngrx/effects';
import { switchMap, map } from 'rxjs/operators';
import { from } from 'rxjs';
import { DepartmentService } from 'src/app/department/department.service';
import { loadDepartments, loadDepartmentsSuccess } from './department.actions';
import { Injectable } from '@angular/core';

@Injectable()
export class DepartmentEffects {
  constructor(
    private actions$: Actions,
    private deptService: DepartmentService
  ) {}

  //run this code when loadDepartments action is dispatched
  loadEmployee$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadDepartments),
      switchMap(() =>
        // Call department service as a side effect and return departments
        from(this.deptService.getDepartments()).pipe(
          // Take the returned value and return a new success action containing the departments
          map((response: any) =>
            loadDepartmentsSuccess({ departments: response.data })
          )
        )
      )
    )
  );
}
