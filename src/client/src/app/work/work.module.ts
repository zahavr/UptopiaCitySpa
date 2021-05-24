import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WorkRoutingModule } from './work-routing.module';
import {SharedModule} from '../shared/shared.module';
import { WorkComponent } from './work.component';


@NgModule({
  declarations: [WorkComponent],
  imports: [
    CommonModule,
    WorkRoutingModule,
    SharedModule
  ]
})
export class WorkModule { }
