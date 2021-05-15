import {Component, Input, OnInit, Output} from '@angular/core';
import {IVacancy} from '../../../../shared/interfaces/vacancy.interface';
import {BusinessService} from '../../../business.service';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-vacancy-item',
  templateUrl: './vacancy-item.component.html',
  styleUrls: ['./vacancy-item.component.scss']
})
export class VacancyItemComponent implements OnInit {
  @Input('vacancy') vacancy: IVacancy;

  constructor(private businessService: BusinessService,
              private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  respondVacancy(): void {
    this.businessService.respondVacancy(this.vacancy.vacancyId).subscribe((res: boolean) => {
      if (res) {
        this.toastrService.success('You`ve responded to the request');
      } else {
        this.toastrService.error('You`re already responded');
      }
    });
  }
}
