import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams, HttpResponse} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {ApiUrl} from '../shared/constants/shared.url.constants';
import {DefaultParams} from '../shared/params/defaultParams';
import {IPagination} from '../shared/interfaces/pagination.interface';
import {map} from 'rxjs/operators';
import {ICardBuilding} from '../shared/interfaces/building.interface';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl) { }

  getAppartaments(buildingParams: DefaultParams): Observable<IPagination> {
    let params: HttpParams = new HttpParams();

    params = params.append('pageIndex', buildingParams.pageIndex.toString());
    params = params.append('pageSize', buildingParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + ApiUrl.Building.GetAppartaments, {observe: 'response', params})
      .pipe(
        map((res: HttpResponse<IPagination>) => res.body)
      );
  }

  getAppartament(id: number): Observable<ICardBuilding> {
    return this.http.get(this.baseUrl + ApiUrl.Building.GetAppartament.replace(':id', `${id}`)).pipe(
      map((res: ICardBuilding) => res)
    );
  }

  getRandomApprtaments(): Observable<ICardBuilding[]> {
    return this.http.get(this.baseUrl + ApiUrl.Building.GetRandomAppartaments).pipe(
      map((res: ICardBuilding[]) => res)
    );
  }

  buyAppartament(id: number): Observable<object>{
    return this.http.get(this.baseUrl + ApiUrl.Building.BuyAppartament.replace(':id', `${id}`)).pipe(
      map(res => res)
    );
  }
}
