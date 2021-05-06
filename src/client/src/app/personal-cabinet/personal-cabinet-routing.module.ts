import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AppRoute} from '../shared/constants/shared.route.constants';
import {PersonalCabinetComponent} from './components/personal-cabinet/personal-cabinet.component';

const routes: Routes = [
  {path: AppRoute.PersonalCabinet.Main, component: PersonalCabinetComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PersonalCabinetRoutingModule { }
