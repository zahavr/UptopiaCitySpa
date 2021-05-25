import { Component, OnInit } from '@angular/core';
import {DefaultParams} from '../../shared/params/defaultParams';
import {ICardBuilding} from '../../shared/interfaces/building.interface';
import {BuildingService} from '../building.service';
import {IPagination} from '../../shared/interfaces/pagination.interface';

@Component({
  selector: 'app-user-appartaments',
  templateUrl: './user-appartaments.component.html',
  styleUrls: ['./user-appartaments.component.scss']
})
export class UserAppartamentsComponent implements OnInit {
  appartamentParams = new DefaultParams();
  totalCount: number;
  appartaments: ICardBuilding[] = [];

  constructor(private buildingService: BuildingService) { }

  ngOnInit(): void {
    this.loadUserAppartaments();
  }

  loadUserAppartaments(): void {
    this.buildingService.getOwnUserAppartaments(this.appartamentParams).subscribe((res: IPagination) => {
      this.appartaments = res.data;
      this.totalCount = res.count;
    });
  }

  onPageChanged(event: number): void {
    if (this.appartamentParams.pageIndex !== event) {
      this.appartamentParams.pageIndex = event;
      this.loadUserAppartaments();
    }
  }

  deleteAppartament(event: ICardBuilding): void {
    this.appartaments = this.appartaments.filter(x => x.id !== event.id);
  }
}
