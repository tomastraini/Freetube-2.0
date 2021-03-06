import { HttpClient, HttpErrorResponse, HttpHeaders, } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(public http: HttpClient, public router: Router, private appComponent: AppComponent) {  }

  user: any;
  password: any;


  ngOnInit(): void
  {
    if (sessionStorage.getItem('m') !== null && sessionStorage.getItem('m') !== undefined)
    {
      window.location.href = '/';
    }
    sessionStorage.removeItem('user');
    sessionStorage.removeItem('password');

  }

  login(event: any): void
  {

    this.http.post(this.appComponent.apiUrl + 'Users/login',
    {
      usern: this.user,
      passwordu: this.password
    },
    {
      observe: 'response',
      responseType: 'json',
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + sessionStorage.getItem('token')
      })
    }).subscribe(
      res =>
      {
        if (res.status === 200 && res.body !== null)
        {
          // get res.body.passwordu from json response
          const response = JSON.stringify(res.body);
          const json = JSON.parse(response);
          sessionStorage.setItem('x', json.usern);
          sessionStorage.setItem('y', json.passwordu);
          sessionStorage.setItem('m', json.id_user);
          window.location.href = '/';
        }
      },
      err =>
      {
        console.log(err);
      }
    );
  }
  register(): void
  {
    this.router.navigate(['/register']);
  }
  goAbout(): void
  {
    this.router.navigate(['/about']);
  }
}


