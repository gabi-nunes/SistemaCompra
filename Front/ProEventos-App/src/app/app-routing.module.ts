import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventosComponent } from './components/eventos/eventos.component';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { EventoDetalheComponent } from './components/eventos/evento-detalhe/evento-detalhe.component';

import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { UserCadastroComponent } from './components/user/user-cadastro/user-cadastro.component';

const routes: Routes = [
  {path: 'user', component: UserComponent,
    children:[
      {path: 'login', component: LoginComponent},
      {path: 'cadastro', component: UserCadastroComponent}
    ]
  },
  {path: 'eventos', redirectTo: 'eventos/lista'},
  {path: 'eventos', component: EventosComponent,
    children: [
      {path: 'detalhe', component: EventoDetalheComponent},
      {path: 'detalhe/:id', component: EventoDetalheComponent},
      {path: 'lista', component: EventoListaComponent}
    ]
  },
  {path: 'palestrantes', component: PalestrantesComponent},
  {path: 'contatos', component: ContatosComponent},
  {path: 'perfil', component: PerfilComponent},
  {path: 'dashboard', component: DashboardComponent},
  {path: '', redirectTo: 'user/login', pathMatch: 'full'},
  {path: '**', redirectTo: 'dashboard', pathMatch: 'full'}
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
