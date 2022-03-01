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
  descriptionLength: any;

  reload(): void
  {
    const actualroute = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
    this.router.navigate([actualroute]);
  });
  }
  // after view init event

  ngOnInit(): void
  {
    if (this.router.url.includes('/search'))
    {
      // split url to get search value
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
          this.videosOriginal.forEach(function(value: any){
            if (value.description == null) {
              value.description = '';
            }
            value.linksrc = 'https://localhost:44375/api/videos/watch/?id=' + value.id_video ;
          });
        });
  }

  OnPageChange(event: PageEvent){
    let startIndex = event.pageIndex * event.pageSize;
    let endIndex = startIndex + event.pageSize + 3;
    
    if(endIndex > this.length){
      endIndex = this.length;
    }
    this.videos = this.videosOriginal;
    this.videos = this.videos.slice(startIndex, endIndex);
  }
  
  onResize(event: any) { //to adjust to screen size
    this.breakpoint = (event.target.innerWidth <= 400) ? 1 : 6;
  }

  openVid(video: any)
  {
    // go to /watch page with video
    window.location.href = '/watch/' + video.id_video;
  }

}