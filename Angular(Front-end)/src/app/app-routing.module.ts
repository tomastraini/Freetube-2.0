import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { PagnotfoundComponent } from './pagnotfound/pagnotfound.component';
import { MenuComponent } from './searchvideos/menu/menu.component';
import { SearchvideosComponent } from './searchvideos/searchvideos.component';
import { VideoWatchComponent } from './searchvideos/video-watch/video-watch.component';
import { RegisterComponent } from './register/register.component';
import { AboutComponent } from './searchvideos/about/about.component';
import { ProfileComponent } from './searchvideos/profile/profile.component';

const routes: Routes = [
  {
    // pass the MenuComponent and the SearchvideosComponent
    path: '',
    component: SearchvideosComponent,
    children:
    [
      {
        path: '',
        component: MenuComponent
      },
      {
        path: 'watch/:id',
        component: VideoWatchComponent
      },
      {
        path: 'search/:id',
        component: MenuComponent
      },
      {
        path: 'about',
        component: AboutComponent
      },
      {
        path: 'profile/:id',
        component: ProfileComponent
      },
    ]
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },

  {
    path: '**',
    component: PagnotfoundComponent
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

export const routingcomponents = [MenuComponent];
