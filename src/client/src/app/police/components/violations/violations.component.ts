import {Component, OnInit} from '@angular/core';
import {UserService} from '../../../shared/services/user.service';
import {IViolation} from '../../../shared/interfaces/violation.interface';
import {DefaultParams} from '../../../shared/params/defaultParams';
import {IPagination} from '../../../shared/interfaces/pagination.interface';
import {IApiResponse} from '../../../shared/interfaces/api-response.interface';
import {ToastrService} from 'ngx-toastr';
import {AccountService} from '../../../account/account.service';
import {IUser} from '../../../shared/interfaces/user.interface';

@Component({
  selector: 'app-violations',
  templateUrl: './violations.component.html',
  styleUrls: ['./violations.component.scss']
})
export class ViolationsComponent implements OnInit {
  violations: IViolation[] = [];
  violationParams: DefaultParams = new DefaultParams();
  totalCount: number;

  constructor(private userService: UserService,
              private accountService: AccountService,
              private toastrService: ToastrService) {
  }

  ngOnInit(): void {
    this.loadViolations();
  }

  loadViolations(): void {
    this.userService.getUserViolations(this.violationParams).subscribe((res: IPagination) => {
      this.totalCount = res.count;
      this.violations = res.data;
    });
  }

  onPageChanged(event: number): void {
    if (this.violationParams.pageIndex !== event) {
      this.violationParams.pageIndex = event;
      this.loadViolations();
    }
  }

  payForViolation(violation: IViolation): void {
    this.userService.payForViolation(violation.id).subscribe((res: IApiResponse) => {
      if (res.statusCode === 200) {
        this.toastrService.success(res.message);
        this.violations = this.violations.filter(v => v.id !== violation.id);
        let user: IUser;
        this.accountService.currentUser$.subscribe((res: IUser) => {
          user = res;
          user.money -= violation.penalty;
        }).unsubscribe();
        this.accountService.updateUserState(user);
      }
    });
  }
}
