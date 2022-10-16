import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DesignationListComponent } from './designation-list.component';
import { DesignationRoutingModule } from './designation-routing.module';

@NgModule({
  declarations: [DesignationListComponent],
  imports: [CommonModule, DesignationRoutingModule],
})
export class DesignationModule {}
