import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PersonalCabinetRoutingModule } from './personal-cabinet-routing.module';
import { PersonalCabinetComponent } from './components/personal-cabinet/personal-cabinet.component';
import { ProfileInfoComponent } from './components/personal-cabinet/profile-info/profile-info.component';
import { ProfileMainComponent } from './components/personal-cabinet/profile-main/profile-main.component';
import {SharedModule} from '../shared/shared.module';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';


@NgModule({
  declarations: [PersonalCabinetComponent, ProfileInfoComponent, ProfileMainComponent, EditProfileComponent],
  imports: [
    CommonModule,
    PersonalCabinetRoutingModule,
    SharedModule
  ]
})
export class PersonalCabinetModule { }
