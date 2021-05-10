import { Component, OnInit } from '@angular/core';
import {IUser} from '../../../../shared/interfaces/user.interface';
import {AccountService} from '../../../../account/account.service';

@Component({
  selector: 'app-profile-main',
  templateUrl: './profile-main.component.html',
  styleUrls: ['./profile-main.component.scss']
})
export class ProfileMainComponent implements OnInit {
  user: IUser;
  selectTab = 'Friends';

  constructor(private accountService: AccountService) {
    accountService.currentUser$.subscribe(res => {
      this.user = res;
    }).unsubscribe();
  }

  ngOnInit(): void {
  }

  selectedTab($event): void {
    this.selectTab = $event.heading;
  }
}
