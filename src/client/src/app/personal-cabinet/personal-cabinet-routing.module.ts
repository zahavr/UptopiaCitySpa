import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AppRoute} from '../shared/constants/shared.route.constants';
import {PersonalCabinetComponent} from './components/personal-cabinet/personal-cabinet.component';
import {AuthGuard} from '../core/guards/auth.guard';

const routes: Routes = [
  {
    path: AppRoute.PersonalCabinet.Main,
    component: PersonalCabinetComponent,
    canActivate: [AuthGuard]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PersonalCabinetRoutingModule { }
