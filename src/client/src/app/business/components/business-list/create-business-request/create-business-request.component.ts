import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {BusinessService} from '../../../business.service';
import {IApiResponse} from '../../../../shared/interfaces/api-response.interface';
import {BsModalService} from 'ngx-bootstrap/modal';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-create-business-request',
  templateUrl: './create-business-request.component.html',
  styleUrls: ['./create-business-request.component.scss']
})
export class CreateBusinessRequestComponent implements OnInit {
  createBusinessForm: FormGroup;

  constructor(private fb: FormBuilder,
              private businessService: BusinessService,
              private toastrService: ToastrService,
              private modalService: BsModalService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    this.createBusinessForm = this.fb.group({
      name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      description: new FormControl('', [Validators.required, Validators.maxLength(1000)]),
      address: new FormControl('', [Validators.required]),
      maxCountOfWorker: new FormControl('', [Validators.required]),
    });
  }

  sendRequest(): void {
    this.businessService.createBusinessRequest(this.createBusinessForm.value).subscribe((res: IApiResponse) => {
      this.toastrService.success(res.message);
      this.modalService.hide();
    });
  }
}
