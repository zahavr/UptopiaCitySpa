import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PersonalCabinetRoutingModule } from './personal-cabinet-routing.module';
import { PersonalCabinetComponent } from './components/personal-cabinet/personal-cabinet.component';
import { ProfileInfoComponent } from './components/personal-cabinet/profile-info/profile-info.component';
import { ProfileMainComponent } from './components/personal-cabinet/profile-main/profile-main.component';
import {SharedModule} from '../shared/shared.module';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { PhotoUploaderComponent } from './components/photo-uploader/photo-uploader.component';
import {FileUploadModule} from 'ng2-file-upload';
import {FriendsModule} from '../friends/friends.module';
import {BusinessModule} from '../business/business.module';
import { DialogComponent } from './components/personal-cabinet/dialog/dialog.component';
import { ChatComponent } from './components/personal-cabinet/dialog/chat/chat.component';


@NgModule({
  declarations: [PersonalCabinetComponent, ProfileInfoComponent, ProfileMainComponent, EditProfileComponent, PhotoUploaderComponent, DialogComponent, ChatComponent],
  imports: [
    CommonModule,
    PersonalCabinetRoutingModule,
    SharedModule,
    FileUploadModule,
    FriendsModule,
    BusinessModule
  ]
})
export class PersonalCabinetModule { }
