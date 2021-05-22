import {Component, OnInit} from '@angular/core';
import {PoliceService} from '../../police.service';
import {ITableData} from '../../../shared/interfaces/tableData.interface';
import {PoliceParams} from '../../../shared/params/policeParams';
import {IUserProfile} from '../../../shared/interfaces/user-profile.interface';
import {MenuItem} from 'primeng-lts/api';
import {Router} from '@angular/router';

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
      label: 'View Profile', icon: 'fa fa-search', command: () => this.viewUser()
    }
  ];
  selectedUser: IUserProfile;

  constructor(private policeService: PoliceService,
              private router: Router) {
  }

  ngOnInit(): void {
  }

  viewUser(): void {
    this.router.navigateByUrl(`police/${this.selectedUser.id}`);
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
}
