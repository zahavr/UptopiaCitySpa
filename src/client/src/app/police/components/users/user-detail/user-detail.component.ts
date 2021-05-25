import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {PoliceService} from '../../../police.service';
import {IFullUserProfile} from '../../../../shared/interfaces/full-profile-user.interface';
import {IPoliceAppartament} from '../../../../shared/interfaces/building.interface';
import {IFriend} from '../../../../shared/interfaces/friend.interface';
import {IBusiness} from '../../../../shared/interfaces/business.interface';
import {DefaultParams} from '../../../../shared/params/defaultParams';
import {MenuItem} from 'primeng-lts/api';
import {BsModalService} from 'ngx-bootstrap/modal';
import {ViolationTableComponent} from './violation-table/violation-table.component';
import {SetViolationComponent} from './set-violation/set-violation.component';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent implements OnInit {
  user: IFullUserProfile;
  userId: string;
  userAppartaments: IPoliceAppartament[] = [];
  userFriends: IFriend[] = [];
  selectedFriend: IFriend;
  userBusiness: IBusiness[] = [];
  selectedBusiness: IBusiness;
  appartamentsOpen = false;
  businessOpen = false;
  friendsOpen = false;
  disableLoad: boolean;
  friendParams = new DefaultParams();
  actions: MenuItem[] = [
    {
      label: 'View Profile', icon: 'fa fa-search', command: () => this.viewUser()
    }
  ];

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private policeService: PoliceService,
              private modalService: BsModalService) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => {
      return false;
    };
  }

  ngOnInit(): void {
    this.userId = this.activatedRoute.snapshot.paramMap.get('id');
    this.policeService.getUser(this.userId).subscribe((res: IFullUserProfile) => {
      this.user = res;
    });
  }

  loadUserAppartament(): void {
    this.appartamentsOpen = !this.appartamentsOpen;
    if (this.appartamentsOpen) {
      this.policeService.getUserAppartaments(this.userId).subscribe((res: IPoliceAppartament[]) => {
        this.userAppartaments = res;
      });
    }
  }

  loadUserBusiness(): void {
    this.businessOpen = !this.businessOpen;
    if (this.businessOpen) {
      this.policeService.getUserBusiness(this.userId).subscribe((res: IBusiness[]) => {
        this.userBusiness = res;
      });
    }
  }

  loadFriendsUsersOpen(): void {
    this.friendsOpen = !this.friendsOpen;
    if (this.friendsOpen) {
      this.loadFriendsUsers();
    } else {
      this.friendParams = new DefaultParams();
      this.userFriends = [];
    }
  }

  loadFriendsUsers(): void {
    this.policeService.getUserFriend(this.friendParams, this.userId).subscribe((res: IFriend[]) => {
      this.disableLoad = false;
      if (res.length > 0) {
        res.forEach(friend => {
          this.userFriends.push(friend);
        });
        this.friendParams.pageIndex += 1;
      } else {
        this.disableLoad = true;
      }
    });
  }

  viewUser(): void {
    this.router.navigateByUrl(`police/${this.selectedFriend.friendId}`);
  }

  openSetViolationDialog(): void {
    this.modalService.show(SetViolationComponent, {
      initialState: {
        userId: this.userId
      },
    });
  }

  openViolationsDialog(): void {
    this.modalService.show(ViolationTableComponent, {
      initialState: {
        userId: this.userId
      },
      class: 'modal-lg'
    });
  }
}
