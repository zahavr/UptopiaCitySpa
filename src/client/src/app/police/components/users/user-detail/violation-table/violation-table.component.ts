import {Component, Input, OnInit} from '@angular/core';
import {PoliceService} from '../../../../police.service';
import {IViolation, typeViolation} from '../../../../../shared/interfaces/violation.interface';
import {ITableData} from '../../../../../shared/interfaces/tableData.interface';
import {PoliceParams} from '../../../../../shared/params/policeParams';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-violation-table',
  templateUrl: './violation-table.component.html',
  styleUrls: ['./violation-table.component.scss']
})
export class ViolationTableComponent implements OnInit {
  @Input('userId') userId: string;
  loading: boolean;
  violations: IViolation[] = [];
  totalCount: number;
  typeViolation = typeViolation;

  constructor(private policeService: PoliceService,
              private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  loadViolations(event): void{
    const params = new PoliceParams(event);
    this.loading = true;
    this.policeService.getUserViolations(params, this.userId).subscribe((res: ITableData) => {
      this.loading = false;
      this.violations = res.data;
      this.totalCount = res.count;
    });
  }

  amnestyUser(amnestyId: number): void {
    this.policeService.amnestyUser(amnestyId).subscribe((res: boolean) => {
      if (res) {
        this.toastrService.success('Amnesty for user');
        this.violations = this.violations.filter(v => v.id !== amnestyId);
      }
    });
  }
}
