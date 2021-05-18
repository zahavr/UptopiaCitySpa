import {EventEmitter, Inject, Injectable} from '@angular/core';
import {IDialogRequest, IMessage} from '../interfaces/chat.interface';
import {HubConnection, HubConnectionBuilder} from '@aspnet/signalr';
import {AccountService} from '../../account/account.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  messageReceived = new EventEmitter<IMessage>();
  connectionEstablished = new EventEmitter<boolean>();
  createRequestChat = new EventEmitter<IDialogRequest>();
  connectToDialog = new EventEmitter<boolean>();

  private connectionIsEstablished = false;
  private hubConnection: HubConnection;

  constructor(private accountService: AccountService,
              @Inject('BASE_URL') private baseUrl: string) {
    this.createConnection();
    this.startConnection();
    this.registrationServerEvents();
  }

  sendMessage(message: IMessage): void {
    this.hubConnection.invoke('NewMessage', message);
  }

  createRequestForChat(dialogRequest: IDialogRequest): void {
    this.hubConnection.invoke('SendRequestForChat', dialogRequest);
  }

  connectDialog(email: string): void {
    this.hubConnection.invoke('ConnectToDialog', email);
  }

  private createConnection(): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.baseUrl + 'message', { accessTokenFactory: () => this.accountService.getToken()})
      .build();
  }

  private startConnection(): void {
    this.hubConnection
      .start()
      .then(() => {
        this.connectionIsEstablished = true;
        console.log('Hub connected');
        this.connectionEstablished.emit(true);
      })
      .catch((err) => {
        setTimeout(() => {
          this.startConnection();
        }, 5000);
      });
  }

  private registrationServerEvents(): void {
    this.hubConnection.on('MessageReceived', (data: IMessage) => {
      this.messageReceived.emit(data);
    });
    this.hubConnection.on('NewChatRequest', (dialogRequest: IDialogRequest) => {
      this.createRequestChat.emit(dialogRequest);
    });
    this.hubConnection.on('ConnectToDialog', (res: boolean) => {
      this.connectToDialog.emit(res);
    });
  }
}
