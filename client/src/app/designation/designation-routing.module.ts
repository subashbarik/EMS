import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DesignationListComponent } from './designation-list.component';

const route: Routes = [{ path: '', component: DesignationListComponent }];
@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(route)],
  exports: [RouterModule],
})
export class DesignationRoutingModule {}
