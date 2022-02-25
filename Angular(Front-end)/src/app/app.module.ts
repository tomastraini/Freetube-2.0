import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './searchvideos/menu/menu.component';
import { FormsModule } from '@angular/forms';
import { routingcomponents } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import {MatGridListModule} from '@angular/material/grid-list';
import { VideoWatchComponent } from './searchvideos/video-watch/video-watch.component';
import { TableFilterPipe } from './mypipe';
import { SearchvideosComponent } from './searchvideos/searchvideos.component';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; 

@NgModule({
  declarations: [
    AppComponent,
    routingcomponents,
    TableFilterPipe,
    MenuComponent,
    VideoWatchComponent,
    SearchvideosComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatPaginatorModule,
    HttpClientModule,
    FormsModule,
    MatGridListModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
