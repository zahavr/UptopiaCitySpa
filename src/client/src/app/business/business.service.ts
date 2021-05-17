import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {IPagination} from '../shared/interfaces/pagination.interface';
import {ApiUrl} from '../shared/constants/shared.url.constants';
import {DefaultParams} from '../shared/params/defaultParams';
import {map} from 'rxjs/operators';
import {ITableData} from '../shared/interfaces/tableData.interface';
import {TableParams} from '../shared/params/tableParams';
import {IApiResponse} from '../shared/interfaces/api-response.interface';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {

  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string) {

  }

  getUserBusiness(paginationParams: DefaultParams): Observable<IPagination> {
    return this.http.get<IPagination>(this.baseUrl + ApiUrl.Business.GetUserBusiness, {observe: 'response', params: this.setParams(paginationParams)})
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

  getAllVacancy(defaultParams: DefaultParams): Observable<IPagination> {
    return this.http.get<IPagination>(this.baseUrl + ApiUrl.Business.GetAllVacancy,
      {observe: 'response', params: this.setParams(defaultParams)})
      .pipe(
        map((res: HttpResponse<IPagination>) => res.body)
      );
  }

  createVacancy(value: any): Observable<boolean> {
    return this.http.post(this.baseUrl + ApiUrl.Business.CreateVacancy, value).pipe(
      map((res: boolean) => res)
    );
  }

  respondVacancy(vacancyId: number): Observable<boolean> {
    return this.http.post(this.baseUrl + ApiUrl.Business.RespondVacancy.replace(':vacancyId', `${vacancyId}`), {})
      .pipe(
        map((res: boolean) => res)
      );
  }

  getUserRespondVacancies(tableParams: TableParams, businessId: number): Observable<ITableData> {
    return this.http.get(this.baseUrl + ApiUrl.Business.GetVacanciesRespond.replace(':businessId', `${businessId}/`),
      {observe: 'response', params: this.setTableParams(tableParams)})
      .pipe(
        map((res: HttpResponse<ITableData>) => res.body)
      );
  }

  getUserVacancies(tableParams: TableParams): Observable<ITableData> {
    return this.http.get(this.baseUrl + ApiUrl.Business.GetUserVacancies, {observe: 'response', params: this.setTableParams(tableParams)})
      .pipe(
        map((res: HttpResponse<ITableData>) => res.body)
      );
  }

  acceptWorker(vacancyApplicationId: number): Observable<boolean> {
    return this.http.patch(this.baseUrl + ApiUrl.Business.AcceptWorker.replace(':vacancyApplicationId', `${vacancyApplicationId}`), {})
      .pipe(
        map((res: boolean) => res)
      );
  }

  getWorkers(tableParams: TableParams, businessId: number): Observable<ITableData> {
    return this.http.get(this.baseUrl + ApiUrl.Business.GetWorkers.replace(':businessId', `${businessId}/`),
      {observe: 'response', params: this.setTableParams(tableParams)})
      .pipe(
        map((res: HttpResponse<ITableData>) => res.body)
      );
  }

  dismissWorker(id: number): Observable<boolean> {
    return this.http.delete(this.baseUrl + ApiUrl.Business.DismissWorker.replace(':id', `${id}`))
      .pipe(
        map((res: boolean) => res)
      );
  }

  private setParams(paginationParams: DefaultParams): HttpParams {
    let params = new HttpParams();

    params = params.append('pageIndex', paginationParams.pageIndex.toString());
    params = params.append('pageSize', paginationParams.pageSize.toString());

    return params;
  }

  private setTableParams(tableParams: TableParams): HttpParams {
    let params = new HttpParams();

    params = params.append('tableTake', tableParams.tableTake.toString());
    params = params.append('tableSkip', tableParams.tableSkip.toString());

    return params;
  }

  createBusinessRequest(values: any): Observable<IApiResponse> {
    return this.http.post(this.baseUrl + ApiUrl.Business.CreateBusinessRequest, values).pipe(
      map((res: IApiResponse) => res)
    );
  }
}
