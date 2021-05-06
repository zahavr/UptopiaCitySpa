import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {AccountService} from '../../account/account.service';
import {Inject, Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {IUser} from '../../shared/interfaces/user.interface';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService,
              @Inject('BASE_URL') private baseUrl) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let token = localStorage.getItem('token') ??  this.accountService.currentUser$
      .subscribe(user => token = user.token)
      .unsubscribe();

    if (token) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }

    return next.handle(req);
  }
}
