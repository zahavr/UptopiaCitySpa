import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams, HttpResponse} from '@angular/common/http';
import {BehaviorSubject, Observable, Subject} from 'rxjs';
import {IUserProfile} from '../interfaces/user-profile.interface';
import {ApiUrl} from '../constants/shared.url.constants';
import {map} from 'rxjs/operators';
import {DefaultParams} from '../params/defaultParams';
import {IPagination} from '../interfaces/pagination.interface';
import {IApiResponse} from '../interfaces/api-response.interface';

@Injectable({
  providedIn: 'root'
})
export class UserService{
  private photoUrl = new Subject<string>();


  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl) { }

  getUserProfile(): Observable<IUserProfile>{
    return this.http.get(this.baseUrl + ApiUrl.User.Info).pipe(
      map((userProfile: IUserProfile) => {
        return userProfile;
      })
    );
  }

  updateUserPhotoUrl(url: string): void {
    this.photoUrl.next(url);
  }

  getNewPhotoUrl(): Observable<any> {
    return this.photoUrl.asObservable();
  }

  updateUserProfile(value): Observable<IUserProfile> {
    return this.http.patch(this.baseUrl + ApiUrl.User.UpdateProfileInfo, value).pipe(
      map((res: IUserProfile) => {
        return res;
      })
    );
  }

  getUserViolations(defaultParams: DefaultParams): Observable<IPagination> {
    let params: HttpParams = new HttpParams();

    params = params.append('pageIndex', defaultParams.pageIndex.toString());
    params = params.append('pageSize', defaultParams.pageSize.toString());

    return this.http.get(this.baseUrl + ApiUrl.User.GetUserViolations, {observe: 'response', params}).pipe(
      map((res: HttpResponse<IPagination>) => res.body)
    );
  }

  payForViolation(id: number): Observable<IApiResponse> {
    return this.http.delete(this.baseUrl + ApiUrl.User.PayForViolation.replace(':id', id.toString())).pipe(
      map((res: IApiResponse) => res)
    );
  }
}
