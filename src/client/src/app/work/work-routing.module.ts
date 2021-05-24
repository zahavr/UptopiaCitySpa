import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AppRoute} from '../shared/constants/shared.route.constants';
import {WorkComponent} from './work.component';
import {AuthGuard} from '../core/guards/auth.guard';

const routes: Routes = [
  {
    path: AppRoute.Work.UserWork,
    component: WorkComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WorkRoutingModule { }
