import { ListagemPedidoComponent } from './components/area-fornecedor/listagem-pedido/listagem-pedido.component';

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventosComponent } from './components/eventos/eventos.component';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { EventoDetalheComponent } from './components/eventos/evento-detalhe/evento-detalhe.component';

import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { UserCadastroComponent } from './components/user/user-cadastro/user-cadastro.component';
import { UserListaComponent } from './components/user/UserLista/UserLista.component';
import { UserRecuperarSenhaComponent } from './components/user/user-RecuperarSenha/user-RecuperarSenha.component';
import { PerfilComponent } from './components/user/login/perfil/perfil.component';

import { FornecedorComponent } from './components/fornecedor/fornecedor.component';
import { FornecedorDetalheComponent } from './components/fornecedor/fornecedor-detalhe/fornecedor-detalhe.component';
import { FornecedorListaComponent } from './components/fornecedor/fornecedor-lista/fornecedor-lista.component';
import { SolicitacoesComponent } from './components/solicitacoes/solicitacoes.component';
import { SolicitacaoListaComponent } from './components/solicitacoes/solicitacao-lista/solicitacao-lista.component';
import { ProdutoListaComponent } from './components/produtos/produto-lista/produto-lista.component';
import { PedidosComponent } from './components/pedidos/pedidos.component';
import { PedidoDetalheComponent } from './components/pedidos/pedido-detalhe/pedido-detalhe.component';
import { PedidoListaComponent } from './components/pedidos/pedido-lista/pedido-lista.component';
import { SolicitacoesDetalheComponent } from './components/solicitacoes/solicitacoes-detalhe/solicitacoes-detalhe.component';
import { CotacoesListaComponent } from './components/cotacoes/cotacoes-lista/cotacoes-lista.component';
import { CotacoesComponent } from './components/cotacoes/cotacoes.component';
import { CotacoesDetalheComponent } from './components/cotacoes/cotacoes-detalhe/cotacoes-detalhe.component';
import { AreaFornecedorComponent } from './components/area-fornecedor/area-fornecedor.component';
import { DetalheCotacaoComponent } from './components/area-fornecedor/detalhe-cotacao/detalhe-cotacao.component';
import { ListagemCotacaoComponent } from './components/area-fornecedor/listagem-cotacao/listagem-cotacao.component';
import { AlterarSenhaComponent } from './components/area-fornecedor/alterar-senha/alterar-senha.component';

const routes: Routes = [
  {path: 'user', redirectTo: 'user/lista'},
  {path: 'user', component: UserComponent,
    children: [
      {path: 'login', component: LoginComponent},
      {path: 'cadastro', component: UserCadastroComponent},
      {path: 'lista', component: UserListaComponent},
      {path: 'recuperar', component: UserRecuperarSenhaComponent},
      {path: 'perfil', component: PerfilComponent},
      {path: 'perfil/:id', component: PerfilComponent},
    ]
  },

  {path: 'fornecedor', redirectTo: 'fornecedor/lista'},
  {path: 'fornecedor', component: FornecedorComponent,
    children: [
      {path: 'detalhe', component: FornecedorDetalheComponent},
      {path: 'detalhe/:id', component: FornecedorDetalheComponent},
      {path: 'lista', component: FornecedorListaComponent}
    ]
  },

  {path: 'pedidos', redirectTo: 'pedidos/lista'},
  {path: 'pedidos', component: PedidosComponent,
    children: [
      {path: 'detalhe/:id', component: PedidoDetalheComponent},
      {path: 'lista', component: PedidoListaComponent}
    ]
  },

  {path: 'solicitações', redirectTo: 'solicitações/lista'},
  {path: 'solicitações', component: SolicitacoesComponent,
    children: [
      {path: 'lista', component: SolicitacaoListaComponent},
      {path: 'detalhe', component: SolicitacoesDetalheComponent},
      {path: 'detalhe/:id', component: SolicitacoesDetalheComponent}
    ]
  },

  {path: 'cotacoes', redirectTo: 'cotacoes/lista'},
  {path: 'cotacoes', component: CotacoesComponent,
    children: [
      {path: 'lista', component: CotacoesListaComponent},
      {path: 'detalhe/:id', component: CotacoesDetalheComponent}
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
  {path: 'areaFornecedor', redirectTo: 'areaFornecedor/listaCotacao'},
{path: 'areaFornecedor', component: AreaFornecedorComponent,
  children: [
    {path: 'listaCotacao', component: ListagemCotacaoComponent},
    {path: 'detalhe/:id', component: DetalheCotacaoComponent},
    {path: 'listaPedido', component: ListagemPedidoComponent },
    {path: 'pedidos/detalhe/:id', component: PedidoDetalheComponent},
    {path: 'alterarSenha/:id', component: AlterarSenhaComponent},

  ]
},

  {path: 'produtos', component: ProdutoListaComponent },

  {path: 'palestrantes', component: PalestrantesComponent},
  {path: 'contatos', component: ContatosComponent},
  {path: 'dashboard', component: DashboardComponent},
  {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
  {path: '**', redirectTo: '/user/login', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
