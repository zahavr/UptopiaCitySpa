import {Component, Input, OnInit} from '@angular/core';
import {BusinessService} from '../../../business.service';
import {BsModalService} from 'ngx-bootstrap/modal';
import {IUserBusinessVacancy} from '../../../../shared/interfaces/vacancy.interface';
import {TableParams} from '../../../../shared/params/tableParams';
import {ITableData} from '../../../../shared/interfaces/tableData.interface';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-business-vacancies-respond',
  templateUrl: './business-vacancies-respond.component.html',
  styleUrls: ['./business-vacancies-respond.component.scss']
})
export class BusinessVacanciesRespondComponent implements OnInit {
  @Input('businessId') businessId: number;

  respondVacancies: IUserBusinessVacancy[];
  tableParams = new TableParams();
  totalCount: number;
  loading: boolean;


  constructor(private businessService: BusinessService,
              private modalService: BsModalService,
              private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  loadRespondVacancies(): void {
    this.businessService.getUserRespondVacancies(this.tableParams, this.businessId).subscribe((res: ITableData ) => {
      this.totalCount = res.count;
      this.respondVacancies = res.data;
    });
  }

  onChanePage(event: any): void {
    this.tableParams.tableSkip = event.first;
    this.tableParams.tableTake = event.rows;
    this.loadRespondVacancies();
  }

  acceptWorker(vacancyApplicationId: number): void {
    this.businessService.acceptWorker(vacancyApplicationId).subscribe((res: boolean) => {
      if (res) {
        this.toastrService.success('Applicant was accepted. Check you workers.');
        this.loadRespondVacancies();
      }
    });
  }

  rejectVacancyRespond(id: number): void {
    this.businessService.rejectVacancyRespond(id).subscribe((res: boolean) => {
      if (res) {
        this.toastrService.success('Applicant was reject.');
        this.loadRespondVacancies();
      }
    });
  }
}
