import { Component, OnInit } from '@angular/core';
import {IUserRespondVacancy} from '../../../../shared/interfaces/vacancy.interface';
import {TableParams} from '../../../../shared/params/tableParams';
import {BusinessService} from '../../../business.service';
import {ITableData} from '../../../../shared/interfaces/tableData.interface';

@Component({
  selector: 'app-vacancies-responds',
  templateUrl: './user-vacancies-responds.component.html',
  styleUrls: ['./user-vacancies-responds.component.scss']
})
export class UserVacanciesRespondsComponent implements OnInit {
  vacanciesResponds: IUserRespondVacancy[];
  tableParams = new TableParams();
  totalCount: number;
  loading: boolean;

  constructor(private businessService: BusinessService) { }

  ngOnInit(): void {
  }

  uploadVacanciesRespond(): void {
    this.loading = true;
    this.businessService.getUserVacancies(this.tableParams).subscribe((res: ITableData) => {
      this.loading = false;
      this.vacanciesResponds = res.data;
      this.totalCount = res.count;
    });
  }

  onChanePage(event: any): void {
    this.tableParams.tableSkip = event.first;
    this.tableParams.tableTake = event.rows;
    this.uploadVacanciesRespond();
  }
}
