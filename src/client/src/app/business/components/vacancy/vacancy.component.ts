import { Component, OnInit } from '@angular/core';
import {BusinessService} from '../../business.service';
import {IVacancy} from '../../../shared/interfaces/vacancy.interface';
import {DefaultParams} from '../../../shared/params/defaultParams';
import {IPagination} from '../../../shared/interfaces/pagination.interface';

@Component({
  selector: 'app-vacancy',
  templateUrl: './vacancy.component.html',
  styleUrls: ['./vacancy.component.scss']
})
export class VacancyComponent implements OnInit {
  vacancies: IVacancy[];
  vacancyParameters = new DefaultParams();
  totalCount: number;


  constructor(private businessService: BusinessService) { }

  ngOnInit(): void {
    this.uploadVacancies();
  }

  uploadVacancies(): void {
    this.businessService.getAllVacancy(this.vacancyParameters).subscribe((res: IPagination) => {
      this.vacancies = res.data;
      this.totalCount = res.count;
      this.vacancyParameters.pageIndex = res.pageIndex;
      this.vacancyParameters.pageSize = res.pageSize;
    });
  }

  changePage(event): void {
    if (this.vacancyParameters.pageIndex !== event.pageIndex) {
      this.vacancyParameters.pageIndex = event.pageIndex;
      this.uploadVacancies();
    }
  }
}
