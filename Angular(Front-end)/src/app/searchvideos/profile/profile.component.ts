import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor(private http: HttpClient, public router: Router) { }

  userImg: any;
  loggedIn: any;
  userInfo: any;
  userName = '';
  videosOriginal: any;
  videos: any;
  permissionToDelete: any;

  breakpoint = 6;
  length = 0;
  pageSize = 6;
  pageSizeOptions = [6];
  @Input() busquedavalue: any;

  ngOnInit(): void
  {
    const user = sessionStorage.getItem('x');
    const password = sessionStorage.getItem('y');
    this.loggedIn = user !== 'user' && password !== '123';
    if (this.router.url.includes('/profile'))
    {
      const id = this.router.url.split('/')[2];
      if (id === sessionStorage.getItem('m')) { this.permissionToDelete = true; }
      this.userImg = 'https://localhost:44375/api/Users/imageID/' + id + '/' + false;
      this.http.post('https://localhost:44375/api/Users/id', {usern: id},
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
          console.log(this.videos);
          const usernames = this.userName;
          this.videos = this.videos.filter((video: any) => video.usern === usernames);
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
      });
    }


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
  
}
