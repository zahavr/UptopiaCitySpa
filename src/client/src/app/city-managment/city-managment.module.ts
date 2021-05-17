import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CityManagmentRoutingModule } from './city-managment-routing.module';
import { BusinessApplicationsComponent } from './components/business-applications/business-applications.component';
import {ReactiveFormsModule} from '@angular/forms';
import {SharedModule} from '../shared/shared.module';


@NgModule({
  declarations: [BusinessApplicationsComponent],
  imports: [
    CommonModule,
    CityManagmentRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class CityManagmentModule { }
