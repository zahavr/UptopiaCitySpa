import {Component, EventEmitter, Input, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {AccountService} from '../../../account/account.service';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {IUserProfile} from '../../../shared/interfaces/user-profile.interface';
import {UserService} from '../../../shared/services/user.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {
  @Input('profileInfo') profileInfo: IUserProfile;
  title: string;
  closeBtnName: string;
  userProfileInfo: IUserProfile;
  editForm: FormGroup;
  public newUserProfile: EventEmitter<any> = new EventEmitter();

  constructor(private fb: FormBuilder,
              private accountService: AccountService,
              private bsModal: BsModalRef,
              private userService: UserService) { }

  ngOnInit(): void {
    this.createEditProfileForm(this.userProfileInfo);
  }

  createEditProfileForm(currentProfile: IUserProfile): void {
    this.editForm = this.fb.group({
      firstName: new FormControl(currentProfile.firstName, [Validators.required]),
      lastName: new FormControl(currentProfile.lastName, [Validators.required]),
      email: new FormControl(currentProfile.email, [Validators.required, Validators.email], [this.accountService.validateEmailNotTaken()]),
      phoneNumber: new FormControl(currentProfile.phoneNumber, [Validators.required]),
      birthDate: new FormControl(currentProfile.birthDate, [Validators.required]),
    });
  }

  onSubmit(): void {
    this.userService.updateUserProfile(this.editForm.value).subscribe((res: IUserProfile) => {
      this.newUserProfile.emit(res);
      this.closeModal();
    });
  }

  closeModal(): void {
    this.bsModal.hide();
  }
}
