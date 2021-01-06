import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
  validationErrors: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error() {
    this.http.get(environment.apiUrl + 'products/42').subscribe(response => {
      console.log(response)
    }, error => {
      console.log(error)
    })
  }

  get500Error() {
    this.http.get(environment.apiUrl + 'buggy/servererror').subscribe(response => {
      console.log(response)
    }, error => {
      console.log(error)
    })
  }

  get400Error() {
    this.http.get(environment.apiUrl + 'buggy/badrequest').subscribe(response => {
      console.log(response)
    }, error => {
      console.log(error)
    })
  }

  get400ValidationError() {
    this.http.get(environment.apiUrl + 'products/fortytwo').subscribe(response => {
      console.log(response)
    }, error => {
      this.validationErrors = error.errors;
      console.log(error)
    })
  }

}
