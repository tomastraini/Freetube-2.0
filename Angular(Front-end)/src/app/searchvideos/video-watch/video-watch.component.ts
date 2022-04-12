import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-video-watch',
  templateUrl: './video-watch.component.html',
  styleUrls: ['./video-watch.component.scss']
})
export class VideoWatchComponent implements OnInit {

  constructor(public route: ActivatedRoute, private http: HttpClient, public router: Router)
  {}
  id: any;
  video: any;
  src: any;
  comments: any = [];

  ngOnInit(): void
  {
      this.id = this.router.url.split('/')[2];
      const newRoute = 'https://localhost:44375/api/videos/' + this.id;
      this.http.get(newRoute,
      {
        headers:
        {
          Authorization: 'Bearer ' + sessionStorage.getItem('token')
        }
      }).subscribe(res => {
          if (res){
            this.video = res;
            this.src = 'https://localhost:44375/api/videos/watch/?id=' + this.video.id_video;


            this.http.get('https://localhost:44375/api/Comments/'  + this.id, {
              headers: {
                Authorization: 'Bearer ' + sessionStorage.getItem('token')
              }
            })
              .subscribe(res => {
                if (res){
                    this.comments = res;
                    for(let i = 0; i < this.comments.length; i++){
                      this.comments[i].srcImage = 'https://localhost:44375/api/Users/imageID/' + this.comments[i].usern + '/' + false;
                    }
                  }
            });
          }
      });
  }

  submitComment(event: any): void
  {
    if (sessionStorage.getItem('m') !== null && sessionStorage.getItem('m') !== undefined)
    {
      const newRoute = 'https://localhost:44375/api/Comments';
      this.http.post(newRoute,
      {
        id_video: this.id,
        comment: event.target.commentText.value,
        id_user: sessionStorage.getItem('m')
      },
      {
        headers:
        {
          Authorization: 'Bearer ' + sessionStorage.getItem('token')
        }
      }).subscribe(res => {
          if (res){
            this.comments.push(res);
          }
      });
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate(['/watch/' + this.id]);
    }
    else
    {
      this.router.navigate(['/login']);
    }
  }
}
