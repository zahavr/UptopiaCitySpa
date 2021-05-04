import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import {RouterModule} from '@angular/router';
import {SharedModule} from '../shared/shared.module';
import {ToastrModule} from 'ngx-toastr';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { TestErrorComponent } from './components/test-error/test-error.component';



@NgModule({
  declarations: [
    NavBarComponent,
    NotFoundComponent,
    ServerErrorComponent,
    TestErrorComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    })
  ], exports: [
    NavBarComponent
  ]
})
export class CoreModule { }
