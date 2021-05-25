import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PoliceRoutingModule } from './police-routing.module';
import { UsersComponent } from './components/users/users.component';
import {SharedModule} from '../shared/shared.module';
import { UserDetailComponent } from './components/users/user-detail/user-detail.component';
import { SetViolationComponent } from './components/users/user-detail/set-violation/set-violation.component';
import { ViolationTableComponent } from './components/users/user-detail/violation-table/violation-table.component';
import { ViolationsComponent } from './components/violations/violations.component';


@NgModule({
  declarations: [
    UsersComponent,
    UserDetailComponent,
    SetViolationComponent,
    ViolationTableComponent,
    ViolationsComponent
  ],
  imports: [
    CommonModule,
    PoliceRoutingModule,
    SharedModule
  ]
})
export class PoliceModule { }
