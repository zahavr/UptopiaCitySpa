import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BusinessRoutingModule } from './business-routing.module';
import { BusinessListComponent } from './components/business-list/business-list.component';
import { BusinessItemComponent } from './components/business-item/business-item.component';
import { BusinessRequestsComponent } from './components/business-requests/business-requests.component';
import {SharedModule} from '../shared/shared.module';
import { VacancyFormComponent } from './components/business-item/vacancy-form/vacancy-form.component';
import { VacancyComponent } from './components/vacancy/vacancy.component';
import { VacancyItemComponent } from './components/vacancy/vacancy-item/vacancy-item.component';
import { UserVacanciesRespondsComponent } from './components/vacancy/user-vacancies-responds/user-vacancies-responds.component';
import { BusinessVacanciesRespondComponent } from './components/business-item/business-vacancies-respond/business-vacancies-respond.component';
import { WorkerListComponent } from './components/business-item/worker-list/worker-list.component';
import { CreateBusinessRequestComponent } from './components/business-list/create-business-request/create-business-request.component';


@NgModule({
  declarations: [
    BusinessListComponent,
    BusinessItemComponent,
    BusinessRequestsComponent,
    VacancyFormComponent,
    VacancyComponent,
    VacancyItemComponent,
    UserVacanciesRespondsComponent,
    BusinessVacanciesRespondComponent,
    WorkerListComponent,
    CreateBusinessRequestComponent
  ],
  exports: [
    UserVacanciesRespondsComponent
  ],
  imports: [
    CommonModule,
    BusinessRoutingModule,
    SharedModule,
  ]
})
export class BusinessModule { }
