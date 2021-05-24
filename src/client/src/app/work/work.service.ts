import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {IWork} from '../shared/interfaces/work.interface';
import {ApiUrl} from '../shared/constants/shared.url.constants';
import {map} from 'rxjs/operators';
import {IApiResponse} from '../shared/interfaces/api-response.interface';

@Injectable({
  providedIn: 'root'
})
export class WorkService {

  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string) { }

  getUserWork(): Observable<IWork> {
    return this.http.get(this.baseUrl + ApiUrl.Work.GetUserWork).pipe(
      map((res: IWork) => res)
    );
  }

  startShift(): Observable<boolean> {
    return this.http.post(this.baseUrl + ApiUrl.Work.StartShift, {}).pipe(
      map((res: boolean) => res)
    );
  }

  closeShift(): Observable<IApiResponse> {
    return this.http.post(this.baseUrl + ApiUrl.Work.EndShift, {}).pipe(
      map((res: IApiResponse) => res)
    );
  }

  checkOpenShift(): Observable<boolean> {
    return this.http.get(this.baseUrl + ApiUrl.Work.CheckOpenShift).pipe(
      map((res: boolean) => res)
    );
  }
}
