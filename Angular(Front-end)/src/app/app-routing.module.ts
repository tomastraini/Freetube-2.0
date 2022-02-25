import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MenuComponent } from './searchvideos/menu/menu.component';
import { SearchvideosComponent } from './searchvideos/searchvideos.component';
import { VideoWatchComponent } from './searchvideos/video-watch/video-watch.component';

const routes: Routes = [
  {
    // pass the MenuComponent and the SearchvideosComponent
    path: "",
    component: SearchvideosComponent,
    children: 
    [
      {
        path: "",
        component: MenuComponent
      },
      {
        path: "watch/:id",
        component: VideoWatchComponent
      },
      {
        path: "search/:id",
        component: MenuComponent
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule 
{ 
  constructor()
  {
   
  } 
}

export const routingcomponents = [MenuComponent]