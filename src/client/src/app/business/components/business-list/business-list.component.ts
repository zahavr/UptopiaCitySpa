import { Component, OnInit } from '@angular/core';
import {DefaultParams} from '../../../shared/params/defaultParams';
import {BusinessService} from '../../business.service';
import {IPagination} from '../../../shared/interfaces/pagination.interface';
import {IBusiness} from '../../../shared/interfaces/business.interface';
import {BsModalService} from 'ngx-bootstrap/modal';
import {CreateBusinessRequestComponent} from './create-business-request/create-business-request.component';

@Component({
  selector: 'app-business-list',
  templateUrl: './business-list.component.html',
  styleUrls: ['./business-list.component.scss']
})
export class BusinessListComponent implements OnInit {
  businesses: IBusiness[];
  defaultParams = new DefaultParams();
  totalCount: number;

  constructor(private businessService: BusinessService,
              private modalService: BsModalService) { }

  ngOnInit(): void {
    this.loadBusinesses();
  }

  loadBusinesses(): void {
    this.businessService.getUserBusiness(this.defaultParams).subscribe((res: IPagination) => {
      this.businesses = res.data;
      this.totalCount = res.count;
      this.defaultParams.pageSize = res.pageSize;
      this.defaultParams.pageIndex = res.pageIndex;
    });
  }

  onPageChanged(event): void {
    if (this.defaultParams.pageIndex !== event) {
      this.defaultParams.pageIndex = event;
      this.loadBusinesses();
    }
  }

  createNewBusiness(): void {
    this.modalService.show(CreateBusinessRequestComponent, {
      class: 'modal-dialog-centered modal-lg',
      backdrop: true,
    });
  }
}
