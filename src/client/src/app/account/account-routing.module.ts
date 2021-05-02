import {RouterModule, Routes} from '@angular/router';
import {AppRoute} from '../shared/constants/shared.route.constants';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import {NgModule} from '@angular/core';

const routes: Routes = [
  {path: AppRoute.Account.Login, component: LoginComponent},
  {path: AppRoute.Account.Register, component: RegisterComponent}
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
