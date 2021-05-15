import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {BsModalService} from 'ngx-bootstrap/modal';
import {ToastrService} from 'ngx-toastr';
import {BusinessService} from '../../../business.service';

@Component({
  selector: 'app-vacancy-form',
  templateUrl: './vacancy-form.component.html',
  styleUrls: ['./vacancy-form.component.scss']
})
export class VacancyFormComponent implements OnInit {
  vacancyForm: FormGroup;
  businessId: number;

  constructor(private fb: FormBuilder,
              private modalService: BsModalService,
              private toastrService: ToastrService,
              private businessService: BusinessService) { }

  ngOnInit(): void {
    this.initializeVacancyForm();
  }

  initializeVacancyForm(): void {
    this.vacancyForm = this.fb.group({
      title: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      description: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      salary: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      businessId: new FormControl(this.businessId, [Validators.required])
      });
  }

  createVacancy(): void {
    this.businessService.createVacancy(this.vacancyForm.value).subscribe((res: boolean) => {
      if (res) {
        this.modalService.hide();
        this.toastrService.success('Vacancy was created');
      }
    });
  }
}
