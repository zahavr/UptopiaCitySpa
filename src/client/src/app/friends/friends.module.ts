import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FriendsRoutingModule } from './friends-routing.module';
import { FriendsListComponent } from './components/friends-list/friends-list.component';
import { FriendItemComponent } from './components/friend-item/friend-item.component';
import {SharedModule} from '../shared/shared.module';
import { FriendRequestListComponent } from './components/friend-request-list/friend-request-list.component';


@NgModule({
  declarations: [FriendsListComponent, FriendItemComponent, FriendRequestListComponent],
    exports: [
        FriendsListComponent,
        FriendRequestListComponent
    ],
  imports: [
    CommonModule,
    FriendsRoutingModule,
    SharedModule
  ]
})
export class FriendsModule { }
