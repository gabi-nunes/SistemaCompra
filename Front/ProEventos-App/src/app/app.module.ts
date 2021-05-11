import { UserService } from 'src/app/services/user.service';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventosComponent } from './components/eventos/eventos.component';
import { EventoDetalheComponent } from './components/eventos/evento-detalhe/evento-detalhe.component';
import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';
import { NavComponent } from './shared/nav/nav.component';
import { TituloComponent } from './shared/titulo/titulo.component';
import { ContatosComponent } from './components/contatos/contatos.component';


import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxSpinnerModule } from 'ngx-spinner';

import { ToastrModule } from 'ngx-toastr';

import { EventoService } from './services/evento.service';
import { DateTimeFormatterPipe } from './helpers/DateTimeFormatter.pipe';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { UserCadastroComponent } from './components/user/user-cadastro/user-cadastro.component';
import { UserListaComponent } from './components/user/UserLista/UserLista.component';
import { UserRecuperarSenhaComponent } from './components/user/user-RecuperarSenha/user-RecuperarSenha.component';
import { UserDetalheComponent } from './components/user/User-detalhe/User-detalhe.component';
import { PerfilComponent } from './components/user/login/perfil/perfil.component';

import { FornecedorListaComponent } from './components/fornecedor/fornecedor-lista/fornecedor-lista.component';
import { FornecedorDetalheComponent } from './components/fornecedor/fornecedor-detalhe/fornecedor-detalhe.component';
import { FornecedorComponent } from './components/fornecedor/fornecedor.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { SolicitacoesComponent } from './components/solicitacoes/solicitacoes.component';
import { SolicitacaoListaComponent } from './components/solicitacoes/solicitacao-lista/solicitacao-lista.component';
// import { ProdutoComponent } from './components/produtos/produto/produto.component';
import { ProdutoListaComponent } from './components/produtos/produto-lista/produto-lista.component';
import { SolicitacaoService } from './services/solicitacao.service';
import { PedidosComponent } from './components/pedidos/pedidos.component';
import { PedidoListaComponent } from './components/pedidos/pedido-lista/pedido-lista.component';
import { PedidoDetalheComponent } from './components/pedidos/pedido-detalhe/pedido-detalhe.component';
import { PedidoService } from './services/pedido.service';



@NgModule({
  declarations: [
    AppComponent,
    EventosComponent,
    EventoDetalheComponent,
    EventoListaComponent,
    PalestrantesComponent,
    NavComponent,
    DateTimeFormatterPipe,
    TituloComponent,
    ContatosComponent,
    DashboardComponent,
    UserComponent,
    LoginComponent,
    UserCadastroComponent,
    UserListaComponent,
    UserRecuperarSenhaComponent,
    UserDetalheComponent,
    PerfilComponent,
    FornecedorComponent,
    FornecedorListaComponent,
    FornecedorDetalheComponent,
    SolicitacoesComponent,
    SolicitacaoListaComponent,
    ProdutoListaComponent,
    PedidosComponent,
    PedidoListaComponent,
    PedidoDetalheComponent
   ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    BsDatepickerModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({timeOut: 3000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    }),
    NgxSpinnerModule
  ],
  providers: [EventoService, UserService, SolicitacaoService, PedidoService],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
