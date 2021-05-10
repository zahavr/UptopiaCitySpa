import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {IFriend} from '../../../shared/interfaces/friend.interface';
import {FriendService} from '../../friend.service';
import {ToastrService} from 'ngx-toastr';
import {FriendItemTypes} from '../../friend.item.types';

@Component({
  selector: 'app-friend-item',
  templateUrl: './friend-item.component.html',
  styleUrls: ['./friend-item.component.scss']
})
export class FriendItemComponent implements OnInit {
  @Input('friend') friend: IFriend;
  @Input('itemType') itemType: FriendItemTypes;
  @Output('removeFromRequestList') removeFromRequestList = new EventEmitter<IFriend>();
  friendItemType = FriendItemTypes;

  constructor(private friendService: FriendService,
              private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  deleteFriend(): void {
    this.friendService.deleteFriend(this.friend.id).subscribe((res) => {
      this.toastrService.success('Friend deleted');
      this.removeFromRequestList.emit(this.friend);
    });
  }

  openDialog(): void {
    console.log('dialog');
  }

  acceptFriend(): void {
    this.friendService.acceptFriend(this.friend.id).subscribe((res: boolean) => {
      this.removeFromRequestList.emit(this.friend);
      this.toastrService.success('Friend accepted');
   });
  }

  rejectFriend(): void {
    this.friendService.rejectFriend(this.friend.id).subscribe((res: boolean) => {
      this.removeFromRequestList.emit(this.friend);
      this.toastrService.error('Friend request declined');
    });
  }
}
