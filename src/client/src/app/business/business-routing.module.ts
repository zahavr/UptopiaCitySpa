import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AppRoute} from '../shared/constants/shared.route.constants';
import {BusinessListComponent} from './components/business-list/business-list.component';
import {AuthGuard} from '../core/guards/auth.guard';
import {BusinessRequestsComponent} from './components/business-requests/business-requests.component';
import {VacancyComponent} from './components/vacancy/vacancy.component';

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
  },
  {
    path: AppRoute.Business.VacancyList,
    component: VacancyComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BusinessRoutingModule { }
