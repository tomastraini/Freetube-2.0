import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit, } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from '../../app.component';
import { PageEvent} from '@angular/material/paginator';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
  providers: [
    AppComponent
  ]
})
export class MenuComponent implements OnInit {
  constructor(private http: HttpClient, private app: AppComponent, private router: Router, private appComponent: AppComponent)
  {
    if (sessionStorage.getItem('reload') !== null || sessionStorage.getItem('reload') !== undefined)
    {
        this.reload();
    }
  }

  videos: any;
  videosOriginal: any;
  comments: any;
  carpeta: any;
  obj: any;
  videoElement: any;
  mover = 0;
  @Input() busquedavalue: any;

  amountSliced = 8;

  reload(): void
  {
    const actualroute = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.router.navigateByUrl(actualroute);
    });
  }


  ngOnInit(): void
  {
    if (this.router.url.includes('/search'))
    {
      const searchvalue = this.router.url.split('/')[2];
      this.busquedavalue = searchvalue;
    }

    this.http.get(this.appComponent.apiUrl + 'videos',
      {
      headers: {
        Authorization: 'Bearer ' + sessionStorage.getItem('token')
        }
      }
        )
      .subscribe(
        (Response) =>
        {
          this.videosOriginal = Response;
          this.videos = Response;
          this.videos = this.videos.slice(0, this.amountSliced);
          console.log(this.videos);
          this.videosOriginal.forEach((value: any) =>
          {
            if (value.description == null)
            {
              value.description = '';
            }
            value.descriptionLength = value.description != null ? value.description.length : 0;

            value.linksrc = this.appComponent.apiUrl + 'videos/watch/?id=' + value.id_video;

            value.imglinksrc = this.appComponent.apiUrl + 'Users/imageID' + '/' + value.id_user + '/' + false;

          });
        });
  }

  openVid($event: any, video: any): void
  {
    console.log($event);

    if ($event.type === 'auxclick')
    {
      if ($event.srcElement.className.includes('vidUserIMG'))
      {
        window.open('/profile/' + video.id_user);
      }
      window.open('/watch/' + video.id_video);
    }
    else
    {
      if ($event.srcElement.className.includes('vidUserIMG'))
      {
        this.router.navigate(['/profile/' + video.id_user]);
      }
      if ($event.srcElement.className.includes('ml-2'))
      {
        window.location.href = '/watch/' + video.id_video;
      }
    }
  }

  openProfile(id: any): void
  {
    window.location.href = '/profile/' + id;
  }

  expandView(): void
  {
    if (this.amountSliced + 8 < this.videosOriginal.length)
    {
      this.amountSliced += 8;
      this.videos = this.videosOriginal.slice(0, this.amountSliced);
      this.reload();
    }
    else
    {
      this.amountSliced = this.videosOriginal.length;
      this.videos = this.videosOriginal;
      this.reload();
    }
  }
}
