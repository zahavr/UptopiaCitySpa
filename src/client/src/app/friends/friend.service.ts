import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams, HttpResponse} from '@angular/common/http';
import {ApiUrl} from '../shared/constants/shared.url.constants';
import {map} from 'rxjs/operators';
import {Observable} from 'rxjs';
import {IFriend} from '../shared/interfaces/friend.interface';
import {ObjectAssignBuiltinFn} from '@angular/compiler-cli/src/ngtsc/partial_evaluator/src/builtin';
import {UserFriendParams} from '../shared/params/userFriendParams';
import {IPagination} from '../shared/interfaces/pagination.interface';
import {PaginationNumberLinkContext} from 'ngx-bootstrap/pagination';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl) { }


  public createFriendRequest(friend: IFriend): Observable<any> {
    return this.http.post(this.baseUrl + ApiUrl.User.CreateFriendRequest, friend).pipe(
      map(res => res)
    );
  }

  public findFriend(email: string): Observable<IFriend> {
    return this.http.get(this.baseUrl + ApiUrl.User.FindFriend.replace(':email', email)).pipe(
      map((res: IFriend) => res)
    );
  }

  public acceptFriend(id: number): Observable<boolean> {
    return this.http.get(this.baseUrl + ApiUrl.User.AcceptFriendRequest.replace(':id', `${id}`)).pipe(
      map((res: boolean) => res)
    );
  }

  public rejectFriend(id: number): Observable<boolean> {
    return this.http.get(this.baseUrl + ApiUrl.User.RejectFriendRequest.replace(':id', `${id}`)).pipe(
      map((res: boolean) => res)
    );
  }

  public deleteFriend(id: number): Observable<boolean> {
    return this.http.delete(this.baseUrl + ApiUrl.User.DeleteFriend.replace(':id', `${id}`)).pipe(
      map((res: boolean) => res)
    );
  }

  public getListFriends(userFriendParams: UserFriendParams): Observable<IPagination> {
    const params = this.setParams(userFriendParams);

    return this.http.get(this.baseUrl + ApiUrl.User.GetFriendList, {observe: 'response', params}).pipe(
      map((res: HttpResponse<IPagination>) => res.body)
    );
  }

  public getAllFriendRequests(userFriendParams: UserFriendParams): Observable<IPagination> {
    const params = this.setParams(userFriendParams);

    return this.http.get<IPagination>(this.baseUrl + ApiUrl.User.GetFriendsRequest, {observe: 'response', params}).pipe(
      map((res: HttpResponse<IPagination>) => res.body)
    );
  }

  private setParams(userFriendParams: UserFriendParams): HttpParams {
    let params: HttpParams = new HttpParams();

    if (userFriendParams.search) {
      params = params.append('search', userFriendParams.search);
    }

    params = params.append('userEmail', userFriendParams.userEmail);
    params = params.append('pageIndex', userFriendParams.pageIndex.toString());
    params = params.append('pageSize', userFriendParams.pageSize.toString());

    return  params;
  }
}
