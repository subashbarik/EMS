import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeListComponent } from './employee-list.component';
import { EmployeeFormComponent } from './employee-form/employee-form.component';
import { EmployeeDetailsComponent } from './employee-details/employee-details.component';

const route: Routes = [
  { path: '', component: EmployeeListComponent },
  { path: 'details/:id', component: EmployeeDetailsComponent },
  { path: 'add', component: EmployeeFormComponent },
  { path: 'update/:id', component: EmployeeFormComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(route)],
  exports: [RouterModule],
})
export class EmployeeRoutingModule {}
