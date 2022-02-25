import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Output } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Angular';
  url: any;
  format: any;
  filename: any;
  url2: any;
  errorformat: boolean = false;
  video: any;
  busquedavalue: any;

  mensajerror: any

  constructor(private http: HttpClient) 
  {
    
  }

  // on init
  ngOnInit(): void 
  {

  }


 
      

  onSubmitSearch(data: any){
    // table filter the data with the value
    
  }
}
