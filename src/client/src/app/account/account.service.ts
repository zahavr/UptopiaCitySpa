import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Router} from '@angular/router';
import {Observable, of, ReplaySubject} from 'rxjs';
import {ApiUrl} from '../shared/constants/shared.url.constants';
import {IUser} from '../shared/interfaces/user.interface';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource = new ReplaySubject<IUser>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient,
              private router: Router,
              @Inject('BASE_URL') private baseUrl: string) {
  }

  login(values: any): Observable<void> {
    return this.http.post(this.baseUrl + ApiUrl.Account.Login, values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  register(values: any): Observable<void> {
    return this.http.post(this.baseUrl + ApiUrl.Account.Register, values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  loadCurrentUser(token: string): Observable<any> {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return  this.http.get(this.baseUrl + ApiUrl.Account.CurrentUser, {headers}).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  checkEmailExists(email: string): Observable<any> {
    return this.http.get(this.baseUrl + ApiUrl.Account.EmailExists.replace(':email', email));
  }

  logout(): void {
    localStorage.removeItem('token');
    this.router.navigateByUrl('/account/login');
    this.currentUserSource.next(null);
  }
}
