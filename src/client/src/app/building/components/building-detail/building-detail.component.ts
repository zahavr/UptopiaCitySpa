import { Component, OnInit } from '@angular/core';
import {ICardBuilding, typeAppartament} from '../../../shared/interfaces/building.interface';
import {BuildingService} from '../../building.service';
import {ActivatedRoute, Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {AccountService} from '../../../account/account.service';
import {IUser} from '../../../shared/interfaces/user.interface';

@Component({
  selector: 'app-building-detail',
  templateUrl: './building-detail.component.html',
  styleUrls: ['./building-detail.component.scss']
})
export class BuildingDetailComponent implements OnInit {
  appartament: ICardBuilding;
  suggestionAppartaments: ICardBuilding[];
  typeAppartament = typeAppartament;
  user: IUser;

  constructor(private buildingService: BuildingService,
              private activatedRoute: ActivatedRoute,
              private toastService: ToastrService,
              private router: Router,
              private accountService: AccountService) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => {
      return false;
    };
  }

  ngOnInit(): void {
    this.getSelectedAppartament();
    this.getRandomAppartament();
  }

  getSelectedAppartament(): void {
    this.buildingService.getAppartament(+this.activatedRoute.snapshot.paramMap.get('id'))
      .subscribe((appartament: ICardBuilding) => {
        this.appartament = appartament;
      });
  }

  getRandomAppartament(): void {
    this.buildingService.getRandomApprtaments().subscribe((res: ICardBuilding[]) => {
      this.suggestionAppartaments = res;
    });
  }

  buyAppartament(): void {
    this.buildingService.buyAppartament(this.appartament.id).subscribe(res => {
      this.accountService.currentUser$.subscribe(result => {
        this.user = result;
      }).unsubscribe();
      this.toastService.success('You`ve bought a new appartament');
      this.user.money -= this.appartament.cost;
      this.accountService.updateUserState(this.user);
    }, error => {
      this.toastService.error(error);
    });
  }
}
