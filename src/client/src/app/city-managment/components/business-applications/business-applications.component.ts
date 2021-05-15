import {Component, OnInit, TemplateRef} from '@angular/core';
import {IBusiness} from '../../../shared/interfaces/business.interface';
import {CityManagmentService} from '../../city-managment.service';
import {TableParams} from '../../../shared/params/tableParams';
import {ITableData} from '../../../shared/interfaces/tableData.interface';
import {ToastrService} from 'ngx-toastr';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {BsModalService} from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-business-applications',
  templateUrl: './business-applications.component.html',
  styleUrls: ['./business-applications.component.scss']
})
export class BusinessApplicationsComponent implements OnInit {
  businessApplications: IBusiness[];
  totalCount = 0;
  tableParams = new TableParams();
  loading: boolean;
  discardForm: FormGroup;
  minDate = new Date();

  constructor(private cityManagmentService: CityManagmentService,
              private toastrService: ToastrService,
              private fb: FormBuilder,
              private bsModalService: BsModalService) { }

  ngOnInit(): void {
  }

  uploadBusinessApplications(event): void {
    this.tableParams.tableSkip = event.first;
    this.tableParams.tableTake = event.rows;
    this.getBusinessApplications();
  }

  getBusinessApplications(): void {
    this.loading = true;
    this.cityManagmentService.getBusinessRequests(this.tableParams).subscribe((res: ITableData) => {
      this.totalCount = res.count;
      this.businessApplications = res.data;
      this.loading = false;
    });
  }

  acceptBusiness(id: number): void {
    this.cityManagmentService.acceptBusinessRequest(id).subscribe((res: boolean) => {
      if (res) {
        this.toastrService.success('Business was accepted');
        this.getBusinessApplications();
      }
    }, error => {
      this.toastrService.error(error);
    });
  }

  initializeDiscardForm(businessId): void {
    this.discardForm = this.fb.group({
      description: new FormControl('', [Validators.required, Validators.maxLength(400)]),
      expiredDate: new FormControl('', [Validators.required]),
      businessId: new FormControl(businessId)
    });
  }

  openDiscardForm(template: TemplateRef<any>, businessId): void {
    this.initializeDiscardForm(businessId);
    this.bsModalService.show(template);
  }

  closeDialog(): void {
    this.bsModalService.hide();
  }

  rejectBusiness(): void {
    this.cityManagmentService.rejectBusiness(this.discardForm.value).subscribe((res: boolean) => {
      if (res) {
        this.toastrService.success('Business was rejected');
        this.closeDialog();
        this.getBusinessApplications();
      }
    }, error => {
      this.toastrService.error(error);
    });
  }
}
