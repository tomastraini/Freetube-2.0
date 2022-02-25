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
    if(sessionStorage.getItem("reload") !== null)
    {
      setTimeout(() =>
      {
        this.reload();
        sessionStorage.removeItem("reload");
      }, 1000);

    }

  }


  reload()
  {
    var actualroute = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
    this.router.navigate([actualroute]);
  });
  }
  

  breakpoint: number = 3;
  length: number = 0;
  pageSize: number = 3;  
  pageSizeOptions: number[] = [3, 6, 9, 12];

  videos: any;
  videosOriginal: any;
  carpeta: any;
  obj:any;
  videoElement: any;
  mover = 0;
  @Input() busquedavalue: any;
  descriptionLength: any;

  // after view init event

  ngOnInit(): void 
  {
    if(this.router.url.includes('/search'))
    {
      // split url to get search value
      var searchvalue = this.router.url.split('/')[2];
      this.busquedavalue = searchvalue;
    }
    this.breakpoint = (window.innerWidth <= 400) ? 1 : 6;

    this.http.get('https://localhost:44375/api/videos')
      .subscribe(res => {
        if(res){ 
          this.videosOriginal = res;
          this.videos = res;
          this.videos = this.videos.slice(0, 6);
          this.length = this.videos.length;
          this.descriptionLength = this.videos.length;
          
        }
    })

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