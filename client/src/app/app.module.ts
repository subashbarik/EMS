import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { employeeReducer } from './state/employee/employee.reducer';
import { EmployeeEffects } from './state/employee/employee.effects';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { departmentReducer } from './state/department/department.reducer';
import { DepartmentEffects } from './state/department/department.effects';
import { designationReducer } from './state/designation/designation.reducer';
import { DesignationEffect } from './state/designation/designation.effects';
import { globalReducer } from './state/appglobal/appglobal.reducer';
import { GlobalEffects } from './state/appglobal/appglobal.effects';

@NgModule({
  declarations: [AppComponent, HomeComponent, AboutComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    StoreModule.forRoot({
      empState: employeeReducer,
      deptState: departmentReducer,
      desigState: designationReducer,
      globalState: globalReducer,
    }),
    EffectsModule.forRoot([
      EmployeeEffects,
      DepartmentEffects,
      DesignationEffect,
      GlobalEffects,
    ]),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
