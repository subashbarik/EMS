import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainRoutingModule } from './main-routing.module';
import { SharedModule } from '../shared/shared.module';
import { MainComponent } from './main.component';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [MainComponent],
  imports: [CommonModule, CoreModule, SharedModule, MainRoutingModule],
})
export class MainModule {}
