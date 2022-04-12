import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnDestroy, OnInit, Output } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnDestroy, OnInit {
  title = 'Angular';
  url: any;
  format: any;
  filename: any;
  url2: any;
  errorformat = false;
  video: any;
  busquedavalue: any;
  subscription: Subscription;

  mensajerror: any;

  constructor(private http: HttpClient, private router: Router) {
    this.subscription = this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        sessionStorage.removeItem("reload")
      }
    });
  }

  ngOnDestroy(): void {
    
  }

  ngOnInit()
  {
    if ((sessionStorage.getItem('x') === null && sessionStorage.getItem('y') === null)
    || (sessionStorage.getItem('x') === 'user' && sessionStorage.getItem('y') === '123'))
    {
      this.http.post('https://localhost:44375/api/Authentication/authentication', {
            name: 'user',
            password: '123',
            userEnc: false,
            passEnc: true
          }, {observe: 'response', responseType: 'text'})
          .subscribe(
            res =>
            {
              if (res.status === 200)
              {
                const token = res.body != null ? res.body : '';
                if (token === ''){ return; }
                sessionStorage.setItem('x', 'user');
                sessionStorage.setItem('y', '123');

                sessionStorage.setItem('token', token.toString());
              }
            }
          );
    }
    else
    {
      this.http.post('https://localhost:44375/api/Authentication/authentication', {
            name: sessionStorage.getItem('x'),
            password: sessionStorage.getItem('y'),
            userEnc: true,
            passEnc: false
          }, {observe: 'response', responseType: 'text'})
          .subscribe(
            res =>
            {
              if (res.status === 200)
              {
                const token = res.body != null ? res.body : '';
                if (token === ''){ return; }
                sessionStorage.setItem('token', token.toString());
              }
            }
          );
    }
  }
}
