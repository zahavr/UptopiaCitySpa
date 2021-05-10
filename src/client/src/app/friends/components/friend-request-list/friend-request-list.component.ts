import {Component, Input, OnInit} from '@angular/core';
import {IFriend} from '../../../shared/interfaces/friend.interface';
import {FriendItemTypes} from '../../friend.item.types';
import {FriendService} from '../../friend.service';
import {IUser} from '../../../shared/interfaces/user.interface';
import {UserFriendParams} from '../../../shared/params/userFriendParams';
import {IPagination} from '../../../shared/interfaces/pagination.interface';

@Component({
  selector: 'app-friend-request-list',
  templateUrl: './friend-request-list.component.html',
  styleUrls: ['./friend-request-list.component.scss']
})
export class FriendRequestListComponent implements OnInit {
  @Input('user') user: IUser;

  friends: IFriend[];
  friendItemType = FriendItemTypes;
  friendParams = new UserFriendParams();
  totalCount: number;

  constructor(private friendService: FriendService) { }

  ngOnInit(): void {
    this.loadRequestFriends();
  }

  loadRequestFriends(): void {
    this.friendParams.userEmail = this.user.email;
    this.friendService.getAllFriendRequests(this.friendParams).subscribe((res: IPagination) => {
      this.friends = res.data;
      this.friendParams.pageIndex = res.pageIndex;
      this.friendParams.pageSize = res.pageSize;
      this.totalCount = res.count;
    });
  }

  onPageChanged(event: any): void {
    if (this.friendParams.pageIndex !== event) {
      this.friendParams.pageIndex = event;
      this.loadRequestFriends();
    }
  }

  removeFromList(event: IFriend): void {
    this.friends = this.friends.filter(friend => friend.id !== event.id);
    this.loadRequestFriends();
  }
}
