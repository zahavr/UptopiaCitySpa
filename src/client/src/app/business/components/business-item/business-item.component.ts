import {Component, Input, OnInit} from '@angular/core';
import {IBusiness} from '../../../shared/interfaces/business.interface';

@Component({
  selector: 'app-business-item',
  templateUrl: './business-item.component.html',
  styleUrls: ['./business-item.component.scss']
})
export class BusinessItemComponent implements OnInit {
  @Input('business') business: IBusiness;

  constructor() { }

  ngOnInit(): void {
  }
}
