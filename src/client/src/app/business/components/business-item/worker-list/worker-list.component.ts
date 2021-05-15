import {Component, Input, OnInit} from '@angular/core';
import {IBusinessWorker} from '../../../../shared/interfaces/business.interface';
import {TableParams} from '../../../../shared/params/tableParams';
import {BusinessService} from '../../../business.service';
import {ITableData} from '../../../../shared/interfaces/tableData.interface';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-worker-list',
  templateUrl: './worker-list.component.html',
  styleUrls: ['./worker-list.component.scss']
})
export class WorkerListComponent implements OnInit {
  @Input('businessId') businessId: number;
  workers: IBusinessWorker[];
  tableParams = new TableParams();
  totalCount: number;
  loading: boolean;

  constructor(private businessService: BusinessService,
              private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  uploadWorkers(): void {
    this.loading = true;
    this.businessService.getWorkers(this.tableParams, this.businessId).subscribe((res: ITableData) => {
      this.loading = false;
      this.workers = res.data;
      this.totalCount = res.count;
    });
  }

  onChanePage(event: any): void {
    this.tableParams.tableSkip = event.first;
    this.tableParams.tableTake = event.rows;
    this.uploadWorkers();
  }

  dismissWorker(id: number): void {
    this.businessService.dismissWorker(id).subscribe((res) => {
      if (res) {
        this.toastrService.success('Worker was deleted');
        this.uploadWorkers();
      }
    });
  }
}
