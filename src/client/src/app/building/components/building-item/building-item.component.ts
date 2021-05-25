import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ICardBuilding, typeAppartament} from '../../../shared/interfaces/building.interface';
import {BuildingService} from '../../building.service';
import {ToastrService} from 'ngx-toastr';
import {AccountService} from '../../../account/account.service';
import {IUser} from '../../../shared/interfaces/user.interface';
import {IApiResponse} from '../../../shared/interfaces/api-response.interface';
import {Router} from '@angular/router';

@Component({
  selector: 'app-building-item',
  templateUrl: './building-item.component.html',
  styleUrls: ['./building-item.component.scss']
})
export class BuildingItemComponent implements OnInit {
  @Input('appartament') appartament: ICardBuilding;
  @Input('isSale') isSale = false;
  @Output('deleteAppartament') deleteAppartament = new EventEmitter<ICardBuilding>();
  user: IUser;
  typeAppartament = typeAppartament;
  constructor(private buildingService: BuildingService,
              private toastService: ToastrService,
              private accountService: AccountService,
              private router: Router) {
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
      this.router.navigate(['/buildings/appartaments']);
    }, error => {
      this.toastService.error(error.error);
    });
  }

  sellAppartament(): void {
    this.buildingService.sellAppartaments(this.appartament.id).subscribe((res: boolean) => {
      if (res){
        this.accountService.currentUser$.subscribe(result => {
          this.user = result;
        }).unsubscribe();
        this.toastService.success('You sold your appartament');
        this.user.money += this.appartament.cost;
        this.accountService.updateUserState(this.user);
        this.deleteAppartament.emit(this.appartament);
      }
    });
  }
}
