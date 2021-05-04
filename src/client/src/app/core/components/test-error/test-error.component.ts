import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ApiUrl} from '../../../shared/constants/shared.url.constants';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
  validationError: any;

  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit(): void {
  }

  get404Error(): void {
    this.http.get(this.baseUrl + ApiUrl.Errors.NotFound).subscribe((response) => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  get500Error(): void {
    this.http.get(this.baseUrl + ApiUrl.Errors.ServerError).subscribe((response) => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  get400Error(): void {
    this.http.get(this.baseUrl + ApiUrl.Errors.BadRequest).subscribe((response) => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  get400ValidationError(): void {
    this.http.get(this.baseUrl + ApiUrl.Errors.ValidationError).subscribe((response) => {
      console.log(response);
    }, error => {
      console.log(error);
      this.validationError = error.errors;
    });
  }

  get401Unauthorize(): void {
    this.http.get(this.baseUrl + ApiUrl.Errors.UnAuth).subscribe((response) => {
      console.log(response);
    }, error => {
      console.log(error);
    });
  }
}
