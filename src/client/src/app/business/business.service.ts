import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {IPagination} from '../shared/interfaces/pagination.interface';
import {ApiUrl} from '../shared/constants/shared.url.constants';
import {DefaultParams} from '../shared/params/defaultParams';
import {map} from 'rxjs/operators';
import {ITableData} from '../shared/interfaces/tableData.interface';
import {TableParams} from '../shared/params/tableParams';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {

  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string) {

  }

  getUserBusiness(paginationParams: DefaultParams): Observable<IPagination> {
    let params = new HttpParams();

    params = params.append('pageIndex', paginationParams.pageIndex.toString());
    params = params.append('pageSize', paginationParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + ApiUrl.Business.GetUserBusiness, {observe: 'response', params})
      .pipe(
        map((res: HttpResponse<any>) => res.body)
      );
  }

  getUserPendingBusiness(tableParams: TableParams): Observable<ITableData> {
    let params = new HttpParams();

    params = params.append('tableSkip', tableParams.tableSkip.toString());
    params = params.append('tableTake', tableParams.tableTake.toString());

    return this.http.get<ITableData>(this.baseUrl + ApiUrl.Business.GetUserApplications, {observe: 'response', params})
      .pipe(
        map((res: HttpResponse<any>) => res.body)
      );
  }
}
