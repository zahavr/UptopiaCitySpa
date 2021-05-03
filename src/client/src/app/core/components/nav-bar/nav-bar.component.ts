import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs';
import {IUser} from '../../../shared/interfaces/user.interface';
import {AccountService} from '../../../account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  currentUser$: Observable<IUser>;
  constructor(private  accountService: AccountService) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

  logout(): void {
    this.accountService.logout();
  }
}
