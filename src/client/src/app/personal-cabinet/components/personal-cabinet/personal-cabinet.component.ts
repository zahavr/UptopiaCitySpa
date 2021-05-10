import { Component, OnInit } from '@angular/core';
import {AccountService} from '../../../account/account.service';
import {Observable} from 'rxjs';
import {IUser} from '../../../shared/interfaces/user.interface';

@Component({
  selector: 'app-personal-cabinet',
  templateUrl: './personal-cabinet.component.html',
  styleUrls: ['./personal-cabinet.component.scss']
})
export class PersonalCabinetComponent implements OnInit {
  currentUser$: Observable<IUser>;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

}
