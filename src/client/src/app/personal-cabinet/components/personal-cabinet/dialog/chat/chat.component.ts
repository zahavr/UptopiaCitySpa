import {Component, ElementRef, HostListener, Input, OnInit, ViewChild} from '@angular/core';
import {IMessage, TypeMessage} from '../../../../../shared/interfaces/chat.interface';
import {ChatService} from '../../../../../shared/services/chat.service';
import {IUser} from '../../../../../shared/interfaces/user.interface';
import {AccountService} from '../../../../../account/account.service';
import {BsModalService} from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  @Input('userJoined') userJoined = false;
  @Input('recipientEmail') recipientEmail: string;
  @ViewChild('messageInput') messageInput: ElementRef;
  currentUser: IUser;
  messages: IMessage[] = [];
  typeMessage = TypeMessage;

  constructor(private chatService: ChatService,
              private accountService: AccountService,
              private modalService: BsModalService
  ) {
  }

  @HostListener('window:keyup', ['$event'])
  keyEvent(event: KeyboardEvent): void {
    console.log(event);
    if (event.key === 'Enter') {
      this.sendMessage(this.messageInput.nativeElement.value);
      this.messageInput.nativeElement.value = ' ';
    }
  }

  ngOnInit(): void {
    this.subscribeEvents();
    this.accountService.currentUser$.subscribe((res: IUser) => {
      this.currentUser = res;
    }).unsubscribe();
  }

  sendMessage(messageText: string): void {
    const message: IMessage = {
      sender: this.currentUser.email,
      recipient: this.recipientEmail,
      date: new Date(),
      typeMessage: TypeMessage.Send,
      messageText
    };
    this.messages.push(message);
    console.log(message);
    this.chatService.sendMessage(message);
  }

  private subscribeEvents(): void {
    this.chatService.connectToDialog.subscribe((res: boolean) => {
      this.userJoined = res;
    });

    this.chatService.messageReceived.subscribe((res: IMessage) => {
      console.log(res);
      this.messages.push(res);
    });
  }

  closeDialog(): void {
    this.modalService.hide();
  }
}
