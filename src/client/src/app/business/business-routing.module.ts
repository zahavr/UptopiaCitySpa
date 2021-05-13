import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AppRoute} from '../shared/constants/shared.route.constants';
import {BusinessListComponent} from './components/business-list/business-list.component';
import {AuthGuard} from '../core/guards/auth.guard';
import {BusinessRequestsComponent} from './components/business-requests/business-requests.component';

const routes: Routes = [
  {
    path: AppRoute.Business.Main,
    component: BusinessListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: AppRoute.Business.RequestList,
    component: BusinessRequestsComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BusinessRoutingModule { }
