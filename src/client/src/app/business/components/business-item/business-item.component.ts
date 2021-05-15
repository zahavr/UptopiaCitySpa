import {Component, Input, OnInit} from '@angular/core';
import {IBusiness} from '../../../shared/interfaces/business.interface';
import {BusinessService} from '../../business.service';
import {BsModalService} from 'ngx-bootstrap/modal';
import {VacancyFormComponent} from './vacancy-form/vacancy-form.component';
import {UserVacanciesRespondsComponent} from '../vacancy/user-vacancies-responds/user-vacancies-responds.component';
import {BusinessVacanciesRespondComponent} from './business-vacancies-respond/business-vacancies-respond.component';
import {WorkerListComponent} from './worker-list/worker-list.component';

@Component({
  selector: 'app-business-item',
  templateUrl: './business-item.component.html',
  styleUrls: ['./business-item.component.scss']
})
export class BusinessItemComponent implements OnInit {
  @Input('business') business: IBusiness;

  constructor(private businessService: BusinessService,
              private modalService: BsModalService) {
  }

  ngOnInit(): void {
  }

  createVacancy(): void {
    this.initializeModal(VacancyFormComponent);
  }

  showListVacancyResponse(): void {
    this.initializeModal(BusinessVacanciesRespondComponent);
  }

  showListWorkers(): void {
    this.initializeModal(WorkerListComponent);
  }

  private initializeModal(component: any): void {
    const initialState = {
      businessId: this.business.id
    };

    this.modalService.show(component, {
      class: 'modal-dialog-centered modal-lg',
      backdrop: true,
      initialState
    });
  }
}
