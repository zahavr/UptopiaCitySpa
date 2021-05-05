import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, Observable, Subject} from 'rxjs';
import {IUserProfile} from '../interfaces/user-profile.interface';
import {ApiUrl} from '../constants/shared.url.constants';
import {map} from 'rxjs/operators';

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
}
