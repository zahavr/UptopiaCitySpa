import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {TestErrorComponent} from './core/components/test-error/test-error.component';
import {ServerErrorComponent} from './core/components/server-error/server-error.component';
import {NotFoundComponent} from './core/components/not-found/not-found.component';
import {HomeComponent} from './home/home.component';
import {AuthGuard} from './core/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [AuthGuard]
  },
  {path: 'test-error', component: TestErrorComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: 'not-found', component: NotFoundComponent},
  {
    path: 'account',
    loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule)
  },
  {
    path: 'personal-cabinet',
    loadChildren: () => import('./personal-cabinet/personal-cabinet.module').then(mod => mod.PersonalCabinetModule)
  },
  {
    path: 'buildings',
    loadChildren: () => import('./building/building.module').then(mod => mod.BuildingModule),
  },
  {path: '**', redirectTo: 'not-found', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
