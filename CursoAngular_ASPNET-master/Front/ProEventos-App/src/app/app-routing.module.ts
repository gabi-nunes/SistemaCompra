import { EventosComponent } from './eventos/eventos.component';
import { NgModule } from '@angular/core';
import { Router, RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './User/login/login.component';


const routes: Routes = [
  {
      path:'login',
      component: LoginComponent
  },
  {
    path:'eventos',
    component: EventosComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

 }
