import {Component, OnInit} from '@angular/core';
import {PoliceService} from '../../police.service';
import {ITableData} from '../../../shared/interfaces/tableData.interface';
import {PoliceParams} from '../../../shared/params/policeParams';
import {IUserProfile} from '../../../shared/interfaces/user-profile.interface';
import {MenuItem} from 'primeng-lts/api';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {AccountService} from '../../../account/account.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  users: IUserProfile[] = [];
  loading: boolean;
  totalCount: number;
  actions: MenuItem[] = [
    {
      label: 'View Profile', icon: 'fa fa-search', command: () => this.viewUser(),
    },
    {
      label: 'Make officer', icon: 'fa fa-gavel', command: () => this.makeOfficer(), visible:  this.accountService.isInRole('Sheriff')
    }
  ];
  selectedUser: IUserProfile;

  constructor(private policeService: PoliceService,
              private router: Router,
              private toastrService: ToastrService,
              private accountService: AccountService) {
  }

  ngOnInit(): void {
  }

  viewUser(): void {
    this.router.navigateByUrl(`police/users/${this.selectedUser.id}`);
  }

  uploadUsers(event: any): void {
    const params = new PoliceParams(event);
    this.loading = true;
    this.policeService.getUsers(params).subscribe((res: ITableData) => {
      this.loading = false;
      this.users = res.data;
      this.totalCount = res.count;
    });
  }

  makeOfficer(): void {
    this.policeService.makeOfficer(this.selectedUser.id).subscribe((res: boolean) => {
      if (res) {
        this.toastrService.success('User is policeman now');
      }
    });
  }
}
