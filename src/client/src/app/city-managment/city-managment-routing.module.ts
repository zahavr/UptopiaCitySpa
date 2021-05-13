import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AppRoute} from '../shared/constants/shared.route.constants';
import {BusinessApplicationsComponent} from './components/business-applications/business-applications.component';
import {AuthGuard} from '../core/guards/auth.guard';

const routes: Routes = [
  {
    path: AppRoute.CityManagment.BusinessApplications,
    component: BusinessApplicationsComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CityManagmentRoutingModule { }
