import {Component, Input, OnInit, TemplateRef} from '@angular/core';
import {IFriend} from '../../../shared/interfaces/friend.interface';
import {FriendService} from '../../friend.service';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ToastrService} from 'ngx-toastr';
import {FriendItemTypes} from '../../friend.item.types';
import {IUser} from '../../../shared/interfaces/user.interface';
import {UserFriendParams} from '../../../shared/params/userFriendParams';
import {IPagination} from '../../../shared/interfaces/pagination.interface';

@Component({
  selector: 'app-friends-list',
  templateUrl: './friends-list.component.html',
  styleUrls: ['./friends-list.component.scss']
})
export class FriendsListComponent implements OnInit {
  @Input('user') user: IUser;
  foundedFriend: IFriend;
  friends: IFriend[];
  modalRef: BsModalRef;
  friendItemType = FriendItemTypes;
  friendParams = new UserFriendParams();
  totalCount: number;

  constructor(private friendService: FriendService,
              private toastrService: ToastrService,
              private modalService: BsModalService) { }

  ngOnInit(): void {
    this.loadFriends();
  }

  loadFriends(): void {
    this.friendParams.userEmail = this.user.email;
    this.friendService.getListFriends(this.friendParams).subscribe((res: IPagination) => {
      this.friends = res.data;
      this.friendParams.pageSize = res.pageSize;
      this.friendParams.pageIndex = res.pageIndex;
      this.totalCount = res.count;
    });
  }

  onPageChanged(event: any): void {
    if (this.friendParams.pageIndex !== event) {
      this.friendParams.pageIndex = event;
      this.loadFriends();
    }
  }

  findFriend(value: string, template: TemplateRef<any>): void {
    this.friendService.findFriend(value).subscribe((res: IFriend) => {
      this.foundedFriend = res;
      this.modalRef = this.modalService.show(template);
    });
  }

  addFriend(): void {
    this.friendService.createFriendRequest(this.foundedFriend).subscribe(() => {
      this.toastrService.success('Request for friend sent');
      this.closeDialog();
    });
  }

  closeDialog(): void {
    this.modalService.hide();
    this.foundedFriend = null;
  }

  removeFromList(event: IFriend): void {
      this.friends = this.friends.filter(friend => friend.id !== event.id);
      this.loadFriends();
  }
}
