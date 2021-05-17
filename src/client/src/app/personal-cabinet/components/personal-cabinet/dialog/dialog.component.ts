import {Component, Input, OnInit} from '@angular/core';
import {ChatService} from '../../../../shared/services/chat.service';
import {IDialogRequest} from '../../../../shared/interfaces/chat.interface';
import {BsModalService} from 'ngx-bootstrap/modal';
import {ChatComponent} from './chat/chat.component';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})
export class DialogComponent implements OnInit {
  @Input('dialogRequests') dialogRequests: IDialogRequest[];

  constructor(private chatService: ChatService,
              private modalService: BsModalService) {

  }

  ngOnInit(): void {

  }

  openDialogWithUser(senderEmail: string): void {
    this.chatService.connectDialog(senderEmail);
    this.modalService.show(ChatComponent, {
      class: 'modal-lg',
      ignoreBackdropClick: true,
      initialState: {
        userJoined: true,
        recipientEmail: senderEmail,
      }
    });
  }
}
