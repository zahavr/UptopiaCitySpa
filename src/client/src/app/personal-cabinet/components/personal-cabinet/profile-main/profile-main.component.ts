import { Component, OnInit } from '@angular/core';
import {IUser} from '../../../../shared/interfaces/user.interface';
import {AccountService} from '../../../../account/account.service';
import {ChatService} from '../../../../shared/services/chat.service';
import {IDialogRequest} from '../../../../shared/interfaces/chat.interface';

@Component({
  selector: 'app-profile-main',
  templateUrl: './profile-main.component.html',
  styleUrls: ['./profile-main.component.scss']
})
export class ProfileMainComponent implements OnInit {
  user: IUser;
  selectTab = 'Friends';
  dialogRequests: IDialogRequest[] = [];
  constructor(private accountService: AccountService,
              private chatService: ChatService) {
    this.subscribeToEvents();
    accountService.currentUser$.subscribe(res => {
      this.user = res;
    }).unsubscribe();
  }

  ngOnInit(): void {
  }

  selectedTab($event): void {
    this.selectTab = $event.heading;
  }

  private subscribeToEvents(): void {
    this.chatService.createRequestChat.subscribe((res: IDialogRequest) => {
      if (this.dialogRequests.findIndex(x => x.senderEmail === res.senderEmail) === -1) {
        this.dialogRequests.push(res);
      }
    });
  }
}
