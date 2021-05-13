import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams, HttpResponse} from '@angular/common/http';
import {TableParams} from '../shared/params/tableParams';
import {Observable} from 'rxjs';
import {ITableData} from '../shared/interfaces/tableData.interface';
import {ApiUrl} from '../shared/constants/shared.url.constants';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CityManagmentService {

  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string) {
  }

  getBusinessRequests(tableParams: TableParams): Observable<ITableData> {
    let params: HttpParams = new HttpParams();

    params = params.append('tableSkip', tableParams.tableSkip.toString());
    params = params.append('tableTake', tableParams.tableTake.toString());

    return this.http.get(this.baseUrl + ApiUrl.Business.GetBusinessApplications, {observe: 'response', params})
      .pipe(
        map((res: HttpResponse<ITableData>) => res.body)
      );
  }

  acceptBusinessRequest(businessId: number): Observable<boolean> {
    return this.http.patch(this.baseUrl + ApiUrl.Business.AcceptBusiness.replace(':businessId', `${businessId}`), {})
      .pipe(
        map((res: boolean) => res)
      );
  }


  rejectBusiness(values: any): Observable<boolean> {
    return this.http.post(this.baseUrl + ApiUrl.Business.RejectBusiness, values).pipe(
      map((res: boolean) => res)
    );
  }
}
