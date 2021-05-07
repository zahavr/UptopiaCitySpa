import {Component, OnInit} from '@angular/core';
import {ICardBuilding} from '../../../shared/interfaces/building.interface';
import {BuildingParams} from '../../../shared/interfaces/buildingParams';
import {BuildingService} from '../../building.service';
import {IPagination} from '../../../shared/interfaces/pagination.interface';

@Component({
  selector: 'app-buildings',
  templateUrl: './buildings.component.html',
  styleUrls: ['./buildings.component.scss']
})
export class BuildingsComponent implements OnInit {
  buildings: ICardBuilding[];
  buildingParams = new BuildingParams();
  totalCount: number;

  constructor(private buildingService: BuildingService) {
  }

  ngOnInit(): void {
    this.getAppartaments();
  }

  getAppartaments(): void {
    this.buildingService.getAppartaments(this.buildingParams).subscribe((res: IPagination) => {
      this.buildings = res.data;
      this.buildingParams.pageIndex = res.pageIndex;
      this.buildingParams.pageSize = res.pageSize;
      this.totalCount = res.count;
    });
  }

  onPageChanged(event: any): void {
    if (this.buildingParams.pageIndex !== event) {
      this.buildingParams.pageIndex = event;
      this.getAppartaments();
    }
  }
}
