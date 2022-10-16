import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentListComponent } from './department-list.component';
import { DepartmentRoutingModule } from './department-routing.module';

@NgModule({
  declarations: [DepartmentListComponent],
  imports: [CommonModule, DepartmentRoutingModule],
})
export class DepartmentModule {}
