import {Component, Input, OnInit} from '@angular/core';
import {ICardBuilding, typeAppartament} from '../../../shared/interfaces/building.interface';
import {BuildingService} from '../../building.service';
import {ToastrService} from 'ngx-toastr';
import {AccountService} from '../../../account/account.service';
import {IUser} from '../../../shared/interfaces/user.interface';
import {IApiResponse} from '../../../shared/interfaces/api-response.interface';

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
              private accountService: AccountService) {
  }

  ngOnInit(): void {
  }

  buyAppartament(id: number): void {
    this.buildingService.buyAppartament(id).subscribe((res: IApiResponse) => {
      this.accountService.currentUser$.subscribe(result => {
        this.user = result;
      }).unsubscribe();
      this.toastService.success(res.message);
      this.user.money -= this.appartament.cost;
      this.accountService.updateUserState(this.user);
    }, error => {
      this.toastService.error(error.error);
    });
  }
}
