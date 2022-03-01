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
    if (sessionStorage.getItem('reload') !== null)
    {
      setTimeout(() =>
      {
        this.reload();
        sessionStorage.removeItem('reload');
      }, 1000);
    }
  }
  breakpoint = 6;
  length = 0;
  pageSize = 6;
  pageSizeOptions: number[] = [6];

  videos: any;
  videosOriginal: any;
  comments: any;
  carpeta: any;
  obj: any;
  videoElement: any;
  mover = 0;
  @Input() busquedavalue: any;

  reload(): void
  {
    const actualroute = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
    this.router.navigate([actualroute]);
  });
  }

  ngOnInit(): void
  {
    if (this.router.url.includes('/search'))
    {
      const searchvalue = this.router.url.split('/')[2];
      this.busquedavalue = searchvalue;
    }
    this.breakpoint = (window.innerWidth <= 400) ? 1 : 6;



    this.http.get('https://localhost:44375/api/videos',{
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
          console.log(this.videos);
          
          this.videosOriginal.forEach(function(value: any){
            if (value.description == null)
            {
              value.description = '';
            }
            value.descriptionLength = value.description != null ? value.description.length : 0;

            value.linksrc = 'https://localhost:44375/api/videos/watch/?id=' + value.id_video ;
          });
        });
  }

  OnPageChange(event: PageEvent): void{
    const startIndex = event.pageIndex * event.pageSize;
    let endIndex = startIndex + event.pageSize + 3;
    if (endIndex > this.length){
      endIndex = this.length;
    }
    this.videos = this.videosOriginal;
    this.videos = this.videos.slice(startIndex, endIndex);
  }
  onResize(event: any): void
  {
    this.breakpoint = (event.target.innerWidth <= 400) ? 1 : 6;
  }

  openVid(video: any): void
  {
    window.location.href = '/watch/' + video.id_video;
  }
}
