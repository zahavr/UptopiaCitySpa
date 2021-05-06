import {Component, OnInit} from '@angular/core';
import {IUserProfile} from '../../../../shared/interfaces/user-profile.interface';
import {UserService} from '../../../../shared/services/user.service';
import {BsModalRef, BsModalService, ModalOptions} from 'ngx-bootstrap/modal';
import {PhotoUploaderComponent} from '../../photo-uploader/photo-uploader.component';
import {EditProfileComponent} from '../../edit-profile/edit-profile.component';

@Component({
  selector: 'app-profile-info',
  templateUrl: './profile-info.component.html',
  styleUrls: ['./profile-info.component.scss']
})
export class ProfileInfoComponent implements OnInit {
  userProfile: IUserProfile;
  bsModalRef: BsModalRef;

  constructor(private userService: UserService,
              private modalService: BsModalService) {

    this.userService.getNewPhotoUrl().subscribe(url => {
      if (url) {
        this.userProfile.pictureUrl = url;
      }
    });
  }

  ngOnInit(): void {
    this.userService.getUserProfile().subscribe((user: IUserProfile) => {
      this.userProfile = user;
    });
  }

  openUploadDialog(): BsModalRef {
    return this.modalService.show(
      PhotoUploaderComponent,
      {
        class: 'modal-dialog-centered modal-lg',
        ignoreBackdropClick: true,
        keyboard: false,
        backdrop: true,
      });
  }

  openEditProfileDialog(): void {
    const initialState = {
      userProfileInfo: this.userProfile
    };
    this.bsModalRef = this.modalService.show(EditProfileComponent, {
      class: 'modal-dialog-centered',
      initialState});

    this.bsModalRef.content.newUserProfile.subscribe(userInfo => {
      console.log(userInfo);
      this.userProfile = userInfo;
    });
  }
}
