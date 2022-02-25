import { Component, OnInit } from '@angular/core';
import { MenuComponent } from './menu/menu.component';
import { ActivatedRoute, Router } from '@angular/router';
import { Route } from '@angular/compiler/src/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-searchvideos',
  templateUrl: './searchvideos.component.html',
  styleUrls: ['./searchvideos.component.scss'],
  providers: [
  ]
})
export class SearchvideosComponent implements OnInit {

  constructor(public route: ActivatedRoute, public router: Router, private http: HttpClient)
  { 
    
  }

  title = 'Angular';
  url: any;
  format: any;
  filename: any;
  url2: any;
  errorformat: boolean = false;
  video: any;
  busquedavalue: any;

  mensajerror: any
  sub: any;
  id: any;
  view = '';

  ngOnInit(): void 
  {
    if(this.router.url.includes('/watch'))
    {
      this.view = 'watch';

      this.id = this.router.url.split('/')[2];
    }
    if(this.router.url.includes('/search'))
    {
      this.view = 'search';
    }
    
  
  }

  // make a function to reload component with router
  reload()
  {
    var actualroute = this.router.url;
    // get actual component and reload it
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.router.navigate([actualroute]);
    });
  }

  buscar(event: any)
  {
    console.log(event);
    if(!this.router.url.includes('?search='))
    {
      this.router.navigate(['/search/' + event]);
      setTimeout(() =>
      {
        window.location.reload();
      }, 50);
    }
  }

  onSelectFile(event: any) {
    //get the file
    this.url = event.target.files[0];
    console.log(this.url);

  }


    
  onSubmitVid(data: any){
    
    
    if(data.selectitle == ""){
      this.errorformat = true;
      this.mensajerror = "Rellenar título!"
    }else{
      const formData = new FormData();
      formData.append('files', this.url);

      const httpOptions = {
        headers: new HttpHeaders({ 
          'Access-Control-Allow-Origin':'*',
          'accept': '*/*'
        })
      };

      
      this.http.post('https://localhost:44375/api/Videos?title='+data.selectitle
      +"&description="+data.selectDesc, formData, httpOptions).subscribe(
        (data: any) => {
          window.location.reload();
        }
      );
    }
  }
}
