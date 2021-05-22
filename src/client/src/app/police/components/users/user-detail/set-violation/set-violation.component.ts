import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {typeViolation} from '../../../../../shared/interfaces/violation.interface';
import {PoliceService} from '../../../../police.service';
import {BsModalService} from 'ngx-bootstrap/modal';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-set-violation',
  templateUrl: './set-violation.component.html',
  styleUrls: ['./set-violation.component.scss']
})
export class SetViolationComponent implements OnInit {
  @Input('userId') userId: string;
  violationForm: FormGroup;
  violationType = typeViolation;
  constructor(private fb: FormBuilder,
              private policeService: PoliceService,
              private modalService: BsModalService,
              private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    this.violationForm = this.fb.group({
      description: new FormControl('', Validators.required),
      typeViolation: new FormControl(this.violationType.Low, Validators.required),
      penalty: new FormControl('', Validators.required),
      dateExpired: new FormControl('', Validators.required),
      setDate: new FormControl('', Validators.required),
      citizenId: new FormControl(this.userId, Validators.required)
    });
  }

  sendViolation(): void{
    this.policeService.setUserViolation(this.violationForm.value).subscribe((res: boolean) => {
      if (res) {
        this.modalService.hide();
        this.toastrService.success('Set violation was success');
      }
    });
  }
}
