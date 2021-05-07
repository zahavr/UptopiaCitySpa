import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BuildingRoutingModule } from './building-routing.module';
import { BuildingsComponent } from './components/buildings/buildings.component';
import { BuildingItemComponent } from './components/building-item/building-item.component';
import { BuildingDetailComponent } from './components/building-detail/building-detail.component';
import {SharedModule} from '../shared/shared.module';


@NgModule({
  declarations: [BuildingsComponent, BuildingItemComponent, BuildingDetailComponent],
  imports: [
    CommonModule,
    BuildingRoutingModule,
    SharedModule
  ]
})
export class BuildingModule { }
