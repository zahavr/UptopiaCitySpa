import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BusinessRoutingModule } from './business-routing.module';
import { BusinessListComponent } from './components/business-list/business-list.component';
import { BusinessItemComponent } from './components/business-item/business-item.component';
import { BusinessRequestsComponent } from './components/business-requests/business-requests.component';
import {SharedModule} from '../shared/shared.module';
import {TableModule} from 'primeng-lts/table';


@NgModule({
  declarations: [BusinessListComponent, BusinessItemComponent, BusinessRequestsComponent],
  imports: [
    CommonModule,
    BusinessRoutingModule,
    SharedModule,
  ]
})
export class BusinessModule { }
