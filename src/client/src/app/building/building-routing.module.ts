import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AppRoute} from '../shared/constants/shared.route.constants';
import {BuildingsComponent} from './components/buildings/buildings.component';
import {AuthGuard} from '../core/guards/auth.guard';
import {BuildingDetailComponent} from './components/building-detail/building-detail.component';
import {UserAppartamentsComponent} from './user-appartaments/user-appartaments.component';

const routes: Routes = [
  {
    path: AppRoute.Building.Main,
    component: BuildingsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: AppRoute.Building.Detail,
    component: BuildingDetailComponent,
    canActivate: [AuthGuard]
  },
  {
    path: AppRoute.Building.OwnAppartaments,
    component: UserAppartamentsComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BuildingRoutingModule { }
