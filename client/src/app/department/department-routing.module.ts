import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DepartmentListComponent } from './department-list.component';

const route: Routes = [{ path: '', component: DepartmentListComponent }];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(route)],
  exports: [RouterModule],
})
export class DepartmentRoutingModule {}
