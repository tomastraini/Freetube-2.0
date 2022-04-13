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
  constructor(private http: HttpClient, private app: AppComponent, private router: Router)
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

    this.http.get('https://localhost:44375/api/videos',
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

            value.linksrc = 'https://localhost:44375/api/videos/watch/?id=' + value.id_video;

            value.imglinksrc = 'https://localhost:44375/api/Users/imageID' + '/' + value.id_user + '/' + false;

          });
        });
  }

  openVid($event: any, video: any): void
  {
    if ($event.type === 'click')
    {
      window.location.href = '/watch/' + video.id_video;
    }
    else if ($event.type === 'auxclick')
    {
      window.open('/watch/' + video.id_video, '_blank');
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
