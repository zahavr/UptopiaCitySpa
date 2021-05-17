import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {BusinessService} from '../../../business/business.service';
import {ToastrService} from 'ngx-toastr';
import {BsModalService} from 'ngx-bootstrap/modal';
import {IApiResponse} from '../../../shared/interfaces/api-response.interface';
import {BuildingService} from '../../building.service';
import {typeAppartament} from '../../../shared/interfaces/building.interface';

@Component({
  selector: 'app-add-building',
  templateUrl: './add-building.component.html',
  styleUrls: ['./add-building.component.scss']
})
export class AddBuildingComponent implements OnInit {
  @ViewChild('countAppartamentsInput', {static: true}) countAppartamentsInput: ElementRef;
  createBusinessForm: FormGroup;
  categoryType = typeAppartament;

  constructor(private fb: FormBuilder,
              private buildingService: BuildingService,
              private toastrService: ToastrService,
              private modalService: BsModalService) {
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  get formControls(): any {
    return this.createBusinessForm.controls;
  }

  get appartamentsControl(): FormGroup[] {
    return this.formControls.appartaments.controls as FormGroup[];
  }

  initializeForm(): void {
    this.createBusinessForm = this.fb.group({
      countAppartaments: new FormControl('', [Validators.required, Validators.maxLength(50)]),
      countFloor: new FormControl('', [Validators.required, Validators.maxLength(1000)]),
      street: new FormControl('', [Validators.required]),
      appartaments: new FormArray([])
    });
  }

  sendRequest(): void {
    console.log(this.createBusinessForm.value);
    this.buildingService.creatNewBuilding(this.createBusinessForm.value).subscribe((res: IApiResponse) => {
          this.toastrService.success(res.message);
          this.modalService.hide();
        });
  }

  setCountOfAppartaments(countAppartaments): void {
    if (countAppartaments > 50) {
      this.countAppartamentsInput.nativeElement.value = 50;
      return;
    }
    if (countAppartaments < 0) {
      this.countAppartamentsInput.nativeElement.value = 0;
      return;
    }

    const count = countAppartaments || 0;

    if (this.formControls.appartaments.length < count) {
      for (let i = this.formControls.appartaments.length; i < count; i++) {
        this.formControls.appartaments.push(this.fb.group({
          title: ['', Validators.required],
          description: ['', Validators.required],
          countRooms: ['', Validators.required],
          floor: ['', Validators.required],
          typeAppartament: [typeAppartament.Econom, Validators.required],
          pictureUrl: new FormControl('', [Validators.required]),
          cost: ['', Validators.required]
        }));
      }
    } else {
      for (let i = this.formControls.appartaments.length; i >= count; i--) {
        console.log('w1');
        this.formControls.appartaments.removeAt(i);
      }
    }

  }
}
