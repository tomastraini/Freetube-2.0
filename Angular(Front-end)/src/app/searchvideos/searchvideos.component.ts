import { Component, OnInit } from '@angular/core';
import { MenuComponent } from './menu/menu.component';
import { ActivatedRoute, Router } from '@angular/router';
import { Route } from '@angular/compiler/src/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-searchvideos',
  templateUrl: './searchvideos.component.html',
  styleUrls: ['./searchvideos.component.scss'],
  providers: [
  ]
})
export class SearchvideosComponent implements OnInit {

  constructor(public route: ActivatedRoute, public router: Router, private http: HttpClient, private appComponent: AppComponent)
  {}

  title = 'Angular';
  url: any;
  format: any;
  filename: any;
  url2: any;
  errorformat = false;
  video: any;
  busquedavalue: any;

  mensajerror: any;
  sub: any;
  id: any;
  view = '';

  userImg = '';
  loggedIn = false;

  ngOnInit(): void
  {
    const user = sessionStorage.getItem('x');
    const password = sessionStorage.getItem('y');
    this.loggedIn = user !== 'user' && password !== '123';

    if (sessionStorage.getItem('m') !== undefined && sessionStorage.getItem('m') !== null)
    {
      this.userImg = this.appComponent.apiUrl + 'Users/imageID/' + sessionStorage.getItem('m') + '/' + false;
    }
    if (this.router.url.includes('/watch'))
    {
      this.view = 'watch';

      this.id = this.router.url.split('/')[2];
    }
    if (this.router.url.includes('/search'))
    {
      this.view = 'search';
    }

    if (this.router.url.includes('/about'))
    {
      this.view = 'about';
    }

    if (this.router.url.includes('/profile'))
    {
      this.view = 'profile';
    }

  }

  reload(): void
  {
    const actualroute = this.router.url;
    this.router.navigateByUrl(actualroute, {skipLocationChange: true}).then(() => {
      this.router.navigate([actualroute]);
    });
  }

  buscar(event: any): void
  {
    if (!this.router.url.includes('?search=') && event !== undefined && event !== null && event !== '')
    {
      this.router.navigate(['/search/' + event]);
      setTimeout(() =>
      {
        window.location.reload();
      }, 50);
    }
  }

  onSelectFile(event: any): void {
    this.url = event.target.files[0];
  }

  onSubmitVid(data: any): void
  {
    if (data.selectitle === ''){
      this.errorformat = true;
      this.mensajerror = 'Rellenar t??tulo!';
    }else{
      const formData = new FormData();
      formData.append('files', this.url);

      const httpOptions = {
        headers: new HttpHeaders({
            Authorization: 'Bearer ' + sessionStorage.getItem('token'),
        })
      };
      const username = sessionStorage.getItem('m');
      this.http.post(this.appComponent.apiUrl + 'Videos?title=' + data.selectitle
      + '&description=' + data.selectDesc + '&id_user=' + username, formData, httpOptions).subscribe(
        (Response) => {
          window.location.reload();
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }
  login(): void
  {
    this.router.navigate(['/login']);
  }

  register(): void
  {
    this.router.navigate(['/register']);
  }

  goAbout(): void
  {
    window.location.href = '/about';
  }

  logout(): void
  {
    sessionStorage.setItem('x', 'user');
    sessionStorage.setItem('y', '123');
    sessionStorage.removeItem('m');
    window.location.reload();
  }

  goToProfile(): void
  {
    window.location.href = '/profile/' + sessionStorage.getItem('m');
  }
}
