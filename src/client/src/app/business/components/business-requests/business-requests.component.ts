import { Component, OnInit } from '@angular/core';
import {IBusiness} from '../../../shared/interfaces/business.interface';
import {TableParams} from '../../../shared/params/tableParams';
import {BusinessService} from '../../business.service';
import {ITableData} from '../../../shared/interfaces/tableData.interface';

@Component({
  selector: 'app-business-requests',
  templateUrl: './business-requests.component.html',
  styleUrls: ['./business-requests.component.scss']
})
export class BusinessRequestsComponent implements OnInit {
  businessApplications: IBusiness[];
  totalCount: number;
  loading: boolean;
  tableParams = new TableParams();

  constructor(private businessService: BusinessService) { }

  ngOnInit(): void {
  }

  uploadBusinessApplications(event: any): void {
    this.tableParams.tableSkip = event.first;
    this.tableParams.tableTake = event.rows;
    this.getUserBusinessApplications();
  }

  getUserBusinessApplications(): void {
    this.loading = true;
    this.businessService.getUserPendingBusiness(this.tableParams).subscribe((res: ITableData) => {
      this.totalCount = res.count;
      this.businessApplications = res.data;
      this.loading = false;
    });
  }
}
