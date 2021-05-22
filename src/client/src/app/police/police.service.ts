import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ITableData} from '../shared/interfaces/tableData.interface';
import {ApiUrl} from '../shared/constants/shared.url.constants';
import {map} from 'rxjs/operators';
import {IFullUserProfile} from '../shared/interfaces/full-profile-user.interface';
import {IBusiness} from '../shared/interfaces/business.interface';
import {IPoliceAppartament} from '../shared/interfaces/building.interface';
import {DefaultParams} from '../shared/params/defaultParams';
import {IPagination} from '../shared/interfaces/pagination.interface';
import {IFriend} from '../shared/interfaces/friend.interface';
import {PoliceParams} from '../shared/params/policeParams';
import {IViolation} from '../shared/interfaces/violation.interface';

@Injectable({
  providedIn: 'root'
})
export class PoliceService {

  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string) {
  }

  getUsers(values): Observable<ITableData> {
    return this.http.post(this.baseUrl + ApiUrl.Police.GetUsers, values).pipe(
      map((res: ITableData) => res)
    );
  }

  getUser(userId: string): Observable<IFullUserProfile> {
    return this.http.get(this.baseUrl + ApiUrl.Police.GetUser.replace(':id', userId)).pipe(
      map((res: IFullUserProfile) => res)
    );
  }

  getUserBusiness(userId: string): Observable<IBusiness[]> {
    return this.http.get(this.baseUrl + ApiUrl.Police.GetUserBusiness.replace(':id', userId)).pipe(
      map((res: IBusiness[]) => res)
    );
  }

  getUserAppartaments(userId: string): Observable<IPoliceAppartament[]> {
    return this.http.get(this.baseUrl + ApiUrl.Police.GetUserAppartaments.replace(':id', userId)).pipe(
      map((res: IPoliceAppartament[]) => res)
    );
  }

  getUserFriend(defaultParams: DefaultParams, userId: string): Observable<IFriend[]> {
    let params = new HttpParams();

    params = params.append('pageIndex', defaultParams.pageIndex.toString());
    params = params.append('pageSize', defaultParams.pageSize.toString());

    return this.http.get(this.baseUrl + ApiUrl.Police.GetUserFriends.replace(':id', `${userId}/`),
      {observe: 'response', params})
      .pipe(
        map((res: HttpResponse<IFriend[]>) => res.body)
      );
  }

  getUserViolations(policeParams: PoliceParams, userId: string): Observable<ITableData> {
    return this.http.post(this.baseUrl + ApiUrl.Police.GetUserViolations.replace(':id', userId), policeParams)
      .pipe(
        map((res: ITableData) => res)
      );
  }

  setUserViolation(violation): Observable<boolean> {
    return this.http.post(this.baseUrl + ApiUrl.Police.SetUserViolation, violation).pipe(
      map((res: boolean) => res)
    );
  }

  amnestyUser(amnestyId: number): Observable<boolean>{
    return this.http.delete(this.baseUrl + ApiUrl.Police.AmnestyUser.replace(':id', amnestyId.toString()), {})
      .pipe(
        map((res: boolean) => res)
      );
  }
}
