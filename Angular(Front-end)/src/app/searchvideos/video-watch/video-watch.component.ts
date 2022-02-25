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
  { 

  }
  // https://localhost:44375/api/Videos/1
  id: any
  video: any;

  ngOnInit(): void 
  {
     this.id = this.router.url.split('/')[2];

    var newRoute = 'https://localhost:44375/api/videos/' + this.id;
    this.http.get(newRoute)
      .subscribe(res => {
        if(res){ 
          this.video = res;
        }
    });
  }
}
