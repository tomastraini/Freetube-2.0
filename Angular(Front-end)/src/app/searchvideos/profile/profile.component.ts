import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor(private http: HttpClient) { }

  userImg: any;
  loggedIn: any;
  userInfo: any;
  userName = '';

  ngOnInit(): void
  {
    const user = sessionStorage.getItem('x');
    const password = sessionStorage.getItem('y');
    this.loggedIn = user !== 'user' && password !== '123';
    if (sessionStorage.getItem('m') !== undefined && sessionStorage.getItem('m') !== null)
    {
      this.userImg = 'https://localhost:44375/api/Users/imageID/' + sessionStorage.getItem('m') + '/' + false;
      this.http.post('https://localhost:44375/api/Users/id', {usern: sessionStorage.getItem('m')},
      {
        observe: 'response',
        responseType: 'json',
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + sessionStorage.getItem('token')
        })
      }).subscribe(data => {
        this.userInfo = data.body;
        console.log(this.userInfo);
        
        this.userName = this.userInfo.usern;
      });
    }
  }

}
