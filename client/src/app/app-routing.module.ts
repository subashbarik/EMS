import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { InitialLoadComponent } from './core/components/initial-load/initial-load.component';
import { NotFoundComponent } from './core/components/not-found/not-found.component';
import { ServerErrorComponent } from './core/components/server-error/server-error.component';
import { UnknownErrorComponent } from './core/components/unknown-error/unknown-error.component';
import { CoreModule } from './core/core.module';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'about', component: AboutComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'unknown-error', component: UnknownErrorComponent },
  { path: 'loadingapp', component: InitialLoadComponent },
  {
    path: 'employees',
    loadChildren: () =>
      import('./employee/employee.module').then((mod) => mod.EmployeeModule),
  },
  {
    path: 'departments',
    loadChildren: () =>
      import('./department/department.module').then(
        (mod) => mod.DepartmentModule
      ),
  },
  {
    path: 'designations',
    loadChildren: () =>
      import('./designation/designation.module').then(
        (mod) => mod.DesignationModule
      ),
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
