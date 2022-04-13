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
  liked: any;
  users: any;

  reload(): void
  {
    const actualroute = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.router.navigateByUrl(actualroute);
    });
  }

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
            if (sessionStorage.getItem('m') !== undefined && sessionStorage.getItem('m') !== null)
            {
              this.http.post('https://localhost:44375/api/Videos/getIfLiked',
              {
                id_video: this.video.id_video,
                id_user: sessionStorage.getItem('m')
              },
              {
                headers:
                {
                  Authorization: 'Bearer ' + sessionStorage.getItem('token')
                }
              }).subscribe(res => {
                  this.liked = res;
              });
            }
            else
            {
              this.liked = 3;
            }


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
                      this.http.post('https://localhost:44375/api/Users/id', {
                        usern: this.comments[i].usern
                        }, {
                          headers: {
                            Authorization: 'Bearer ' + sessionStorage.getItem('token')
                          }
                        }).subscribe(res2 => {
                            this.users = res2;
                            this.comments[i].id_user = this.users.id_user;
                        });

                      const date = new Date(this.comments[i].fecha_carga);
                      const now = new Date();
                      const diff = now.getTime() - date.getTime();
                      const days = Math.floor(diff / (1000 * 60 * 60 * 24));
                      const months = Math.floor(diff / (1000 * 60 * 60 * 24 * 30));
                      const years = Math.floor(diff / (1000 * 60 * 60 * 24 * 30 * 12));
                      if (years > 0){
                        this.comments[i].fecha_diff = years + ' years ago';
                      }
                      else if (months > 0){
                        this.comments[i].fecha_diff = months + ' months ago';
                      }
                      else if (days > 0){
                        this.comments[i].fecha_diff = days + ' days ago';
                      }
                      else{
                        this.comments[i].fecha_diff = 'today';
                      }
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

  like(): void
  {
    if (sessionStorage.getItem('m') !== null && sessionStorage.getItem('m') !== undefined)
    {
      if (this.liked !== 1)
      {
        this.http.post('https://localhost:44375/api/Videos/like',
        {
          id_video: this.id,
          id_user: sessionStorage.getItem('m'),
          liked: true
        },
        {
          headers:
          {
            Authorization: 'Bearer ' + sessionStorage.getItem('token')
          }
        }).subscribe(res => {
              this.video.likes += 1;
              if (this.liked === 0) { this.video.dislikes -= 1; }
              this.liked = 1;
              this.reload();
        });
      }
      else
      {
        this.http.delete('https://localhost:44375/api/Videos/like?id_video=' + this.id + '&id_user=' + sessionStorage.getItem('m'),
        {
          headers:
          {
            Authorization: 'Bearer ' + sessionStorage.getItem('token')
          }
        }).subscribe(res => {
              this.liked = 3;
              this.video.likes -= 1;
              this.reload();
        });
      }
    }
    else
    {
      this.router.navigate(['/login']);
    }
  }

  dislike(): void
  {
    if (sessionStorage.getItem('m') !== null && sessionStorage.getItem('m') !== undefined)
    {
      if (this.liked !== 0)
      {
        this.http.post('https://localhost:44375/api/Videos/like',
        {
          id_video: this.id,
          id_user: sessionStorage.getItem('m'),
          liked: false
        },
        {
          headers:
          {
            Authorization: 'Bearer ' + sessionStorage.getItem('token')
          }
        }).subscribe(res => {
              this.video.dislikes += 1;
              if (this.liked === 1) { this.video.likes -= 1; }
              this.liked = 0;
              this.reload();
        });
      }
      else
      {
        this.http.delete('https://localhost:44375/api/Videos/like?id_video=' + this.id + '&id_user=' + sessionStorage.getItem('m'),
        {
          headers:
          {
            Authorization: 'Bearer ' + sessionStorage.getItem('token')
          }
        }).subscribe(res => {
              this.liked = 3;
              this.video.dislikes -= 1;
              this.reload();
        });
      }

    }
    else
    {
      this.router.navigate(['/login']);
    }
  }

  showProfile(id: any): void
  {
    window.location.href = '/profile/' + id;
  }
}
