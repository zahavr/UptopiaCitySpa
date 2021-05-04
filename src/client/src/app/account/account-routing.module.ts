import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from './login/login.component';
import {AppRoute} from '../shared/constants/shared.route.constants';
import {RegisterComponent} from './register/register.component';

const routes: Routes = [
  {path: AppRoute.Account.Login, component: LoginComponent},
  {path: AppRoute.Account.Register, component: RegisterComponent},
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AccountRoutingModule { }
