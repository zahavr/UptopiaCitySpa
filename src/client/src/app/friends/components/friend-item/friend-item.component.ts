import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {IFriend} from '../../../shared/interfaces/friend.interface';
import {FriendService} from '../../friend.service';
import {ToastrService} from 'ngx-toastr';
import {FriendItemTypes} from '../../friend.item.types';
import {ChatService} from '../../../shared/services/chat.service';
import {IDialogRequest, IMessage} from '../../../shared/interfaces/chat.interface';
import {AccountService} from '../../../account/account.service';
import {IUser} from '../../../shared/interfaces/user.interface';
import {BsModalService} from 'ngx-bootstrap/modal';
import {ChatComponent} from '../../../personal-cabinet/components/personal-cabinet/dialog/chat/chat.component';

@Component({
  selector: 'app-friend-item',
  templateUrl: './friend-item.component.html',
  styleUrls: ['./friend-item.component.scss']
})
export class FriendItemComponent implements OnInit {
  @Input('friend') friend: IFriend;
  @Input('itemType') itemType: FriendItemTypes;
  @Output('removeFromRequestList') removeFromRequestList = new EventEmitter<IFriend>();
  @Output('addToDialog') addToDialog = new EventEmitter<IDialogRequest>();
  currentUser: IUser;
  friendItemType = FriendItemTypes;

  constructor(private friendService: FriendService,
              private toastrService: ToastrService,
              private accountService: AccountService,
              private chatService: ChatService,
              private modalService: BsModalService) { }

  ngOnInit(): void {
  }

  deleteFriend(): void {
    this.friendService.deleteFriend(this.friend.id).subscribe((res) => {
      this.toastrService.success('Friend deleted');
      this.removeFromRequestList.emit(this.friend);
    });
  }

  openDialog(): void {
    this.accountService.currentUser$.subscribe(res => {
      this.currentUser = res;
    }).unsubscribe();

    const dialogRequest: IDialogRequest = {
      senderEmail: this.currentUser.email,
      recipientEmail: this.friend.friendEmail,
      date: new Date(),
    };
    this.modalService.show(ChatComponent, {
      class: 'modal-lg',
      ignoreBackdropClick: true,
      initialState: {
        recipientEmail: this.friend.friendEmail
      }
    });
    this.chatService.createRequestForChat(dialogRequest);
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
