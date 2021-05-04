import { Component, OnInit } from '@angular/core';
import {IUserProfile} from '../../../../shared/interfaces/user-profile.interface';
import {UserService} from '../../../../shared/services/user.service';

@Component({
  selector: 'app-profile-info',
  templateUrl: './profile-info.component.html',
  styleUrls: ['./profile-info.component.scss']
})
export class ProfileInfoComponent implements OnInit {
  userProfile: IUserProfile;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUserProfile().subscribe((user: IUserProfile) => {
      this.userProfile = user;
    });
  }
}
