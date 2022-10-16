import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TextInputComponent } from './components/text-input/text-input.component';
import { TableComponent } from './components/table/table.component';
import { SelectInputComponent } from './components/select-input/select-input.component';
import { PagerComponent } from './components/pager/pager.component';

@NgModule({
  declarations: [
    TextInputComponent,
    TableComponent,
    SelectInputComponent,
    PagerComponent,
  ],
  imports: [CommonModule, ReactiveFormsModule, PaginationModule.forRoot()],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    TextInputComponent,
    TableComponent,
    SelectInputComponent,
    PagerComponent,
  ],
})
export class SharedModule {}
