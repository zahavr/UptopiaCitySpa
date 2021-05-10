import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HomeModule} from './home/home.module';
import {CoreModule} from './core/core.module';
import {ErrorInterceptor} from './core/interceptors/error.interceptor';
import {PersonalCabinetModule} from './personal-cabinet/personal-cabinet.module';
import {JwtInterceptor} from './core/interceptors/jwt.interceptor';
import {BuildingModule} from './building/building.module';
import {FriendsModule} from './friends/friends.module';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    HomeModule,
    PersonalCabinetModule,
    BuildingModule,
    FriendsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
