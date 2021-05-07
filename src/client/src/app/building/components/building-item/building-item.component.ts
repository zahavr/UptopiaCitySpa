import {Component, Input, OnInit} from '@angular/core';
import {ICardBuilding, typeAppartament} from '../../../shared/interfaces/building.interface';
import {BuildingService} from '../../building.service';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {AccountService} from '../../../account/account.service';
import {IUser} from '../../../shared/interfaces/user.interface';

@Component({
  selector: 'app-building-item',
  templateUrl: './building-item.component.html',
  styleUrls: ['./building-item.component.scss']
})
export class BuildingItemComponent implements OnInit {
  @Input('appartament') appartament: ICardBuilding;
  user: IUser;
  typeAppartament = typeAppartament;

  constructor(private buildingService: BuildingService,
              private toastService: ToastrService,
              private accountService: AccountService,
              private routerService: Router) {
  }

  ngOnInit(): void {
  }

  buyAppartament(id: number): void {
    this.buildingService.buyAppartament(id).subscribe(res => {
      this.accountService.currentUser$.subscribe(result => {
        this.user = result;
      }).unsubscribe();
      this.toastService.success('You`ve bought a new appartament');
      this.user.money -= this.appartament.cost;
      this.accountService.updateUserState(this.user);
    }, error => {
      this.toastService.error(error.error);
    });
  }
}
