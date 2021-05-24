import { Component, OnInit } from '@angular/core';
import {WorkService} from './work.service';
import {IWork} from '../shared/interfaces/work.interface';
import {ToastrService} from 'ngx-toastr';
import {IApiResponse} from '../shared/interfaces/api-response.interface';

@Component({
  selector: 'app-work',
  templateUrl: './work.component.html',
  styleUrls: ['./work.component.scss']
})
export class WorkComponent implements OnInit {
  work: IWork;
  shiftIsOpen: boolean;

  constructor(private workService: WorkService,
              private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.loadUserWork();
  }

  loadUserWork(): void {
    this.workService.getUserWork().subscribe((res: IWork) => {
      this.work = res;
    });
    this.workService.checkOpenShift().subscribe((res: boolean) => {
      this.shiftIsOpen = res;
    });
  }

  startShift(): void {
    this.workService.startShift().subscribe((res: boolean) => {
      if (res) {
        this.toastrService.success('Shift started');
        this.shiftIsOpen = true;
      }
    });
  }

  closeShift(): void {
    this.workService.closeShift().subscribe((res: IApiResponse) => {
      if (res.statusCode === 200) {
        this.toastrService.success(res.message);
      }
    });
  }
}
