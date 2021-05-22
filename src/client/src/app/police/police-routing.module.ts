import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AppRoute} from '../shared/constants/shared.route.constants';
import {UsersComponent} from './components/users/users.component';
import {AuthGuard} from '../core/guards/auth.guard';
import {UserDetailComponent} from './components/users/user-detail/user-detail.component';

const routes: Routes = [
  {
    path: AppRoute.Police.Users,
    component: UsersComponent,
    canActivate: [AuthGuard]
  },
  {
    path: AppRoute.Police.UserDetail,
    component: UserDetailComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PoliceRoutingModule { }
