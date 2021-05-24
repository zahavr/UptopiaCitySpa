import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Router} from '@angular/router';
import {Observable, of, ReplaySubject, timer} from 'rxjs';
import {ApiUrl} from '../shared/constants/shared.url.constants';
import {IUser} from '../shared/interfaces/user.interface';
import {map, switchMap} from 'rxjs/operators';
import {IUserProfile} from '../shared/interfaces/user-profile.interface';
import {AsyncValidatorFn} from '@angular/forms';
import {ArrayType} from '@angular/compiler';

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
          this.setCurrentUser(user);
        }
      })
    );
  }

  register(values: any): Observable<void> {
    return this.http.post(this.baseUrl + ApiUrl.Account.Register, values).pipe(
      map((user: IUser) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  loadCurrentUser(): Observable<any> {
    return this.http.get(this.baseUrl + ApiUrl.Account.CurrentUser).pipe(
      map((user: IUser) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  checkEmailExists(email: string): Observable<any> {
    return this.http.get(this.baseUrl + ApiUrl.Account.EmailExists.replace(':email', email));
  }

  getToken(): string {
    let token = '';
    this.currentUser$.subscribe(res => {
      token = res.token;
    }).unsubscribe();

    return token;
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.checkEmailExists(control.value).pipe(
            map(res => {
              return res ? {emailExists: true} : null;
            })
          );
        })
      );
    };
  }

  setCurrentUser(user: IUser): void {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('token', user.token);
    localStorage.setItem('roles', JSON.stringify(user.roles));
    this.currentUserSource.next(user);
  }

  updateUserState(user: IUser): void {
    this.currentUserSource.next(user);
  }

  logout(): void {
    this.router.navigateByUrl('/account/login');
    localStorage.removeItem('token');
    localStorage.removeItem('roles');
    this.currentUserSource.next(null);
  }

  private getDecodedToken(token: string): any {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
