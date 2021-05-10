import {Component, Inject, Input, OnInit} from '@angular/core';
import {FileUploader} from 'ng2-file-upload';
import {AccountService} from '../../../account/account.service';
import {IUser} from '../../../shared/interfaces/user.interface';
import {ApiUrl} from '../../../shared/constants/shared.url.constants';
import {BsModalRef} from 'ngx-bootstrap/modal';
import {UserService} from '../../../shared/services/user.service';

@Component({
  selector: 'app-photo-uploader',
  templateUrl: './photo-uploader.component.html',
  styleUrls: ['./photo-uploader.component.scss']
})
export class PhotoUploaderComponent implements OnInit {
  uploader: FileUploader;
  private currentUser: IUser;
  hasBaseDropZoneOver = false;

  constructor(private accountService: AccountService,
              private userService: UserService,
              private bsModal: BsModalRef,
              @Inject('BASE_URL') private baseUrl) {
    this.accountService.currentUser$.subscribe((user: IUser) => {
      this.currentUser = user;
    }).unsubscribe();

    this.initializeUploader();
  }

  ngOnInit(): void {
  }

  initializeUploader(): void {
    this.uploader = new FileUploader({
      url: this.baseUrl + ApiUrl.User.UploadPhoto, // shared component
      authToken: 'Bearer ' + this.currentUser.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        this.userService.updateUserPhotoUrl(response);
        this.closeModal();
      }
    };
  }

  closeModal(): void {
    this.bsModal.hide();
  }
}
