import {Component, OnInit} from '@angular/core';
import {ICardBuilding} from '../../../shared/interfaces/building.interface';
import {DefaultParams} from '../../../shared/params/defaultParams';
import {BuildingService} from '../../building.service';
import {IPagination} from '../../../shared/interfaces/pagination.interface';
import {BsModalService} from 'ngx-bootstrap/modal';
import {AddBuildingComponent} from '../add-building/add-building.component';

@Component({
  selector: 'app-buildings',
  templateUrl: './buildings.component.html',
  styleUrls: ['./buildings.component.scss']
})
export class BuildingsComponent implements OnInit {
  buildings: ICardBuilding[];
  buildingParams = new DefaultParams();
  totalCount: number;

  constructor(private buildingService: BuildingService,
              private modalService: BsModalService) {
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

  createNewBuilding(): void {
    this.modalService.show(AddBuildingComponent, {
      class: 'modal-lg',
      backdrop: true,
    })
  }
}
