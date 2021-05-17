export enum TypeMessage {
  Send = 0,
  Recieve = 1
}

export interface IMessage {
  sender: string;
  recipient: string;
  messageText: string;
  typeMessage: TypeMessage;
  date: Date;
}

export interface IDialogRequest {
  senderEmail: string;
  recipientEmail: string;
  date: Date;
}
