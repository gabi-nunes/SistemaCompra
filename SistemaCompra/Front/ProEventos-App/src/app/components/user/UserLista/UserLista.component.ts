import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { user } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-UserLista',
  templateUrl: './UserLista.component.html',
  styleUrls: ['./UserLista.component.scss']
})
export class UserListaComponent implements OnInit {

  modalRef = {} as BsModalRef;
  constructor(
    private userService: UserService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public user: user[] = [];
  public UserFiltrados: user[] = [];
  private gridFilter = '';
  public userId = 0;
  public isvalid: boolean= false;

  public get GridFilter(): string{
    return this.gridFilter;
  }
  public set GridFilter(value: string){
    this.gridFilter = value;
    this.UserFiltrados = this.gridFilter ? this.Filtrar(this.gridFilter) : this.user;
  }

  public Filtrar(filter: string): user[]{
    filter = filter.toLocaleLowerCase();
    return this.user.filter(
      (user: any) => user.name.toLocaleLowerCase().indexOf(filter) !== -1
      );
  }

  public ngOnInit(): void {
    this.spinner.show();
    this.Carregar();
  }

  public Carregar(): void{
    // tslint:disable-next-line: deprecation
    this.userService.getUser().subscribe({
      next: (userResponse: user[]) => {
        this.user = userResponse,
        this.UserFiltrados = userResponse;
      },
      error: () => {
        this.spinner.hide(),
        this.toastr.error('Erro ao carregar os Usuario', 'Erro');
      },
      complete: () => this.spinner.hide()
    });
  }


  openModal(user: any, template: TemplateRef<any>, id: number): void {
    user.stopPropagation();
    this.userId= id;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});

  }

  confirm(): void {
    this.modalRef.hide();
    this.spinner.show();

    this.userService.deleteUser(this.userId).subscribe(
      (result: any)=>{
          console.log(result);
          this.toastr.success('User deletado com Sucesso', 'Deletado');
          this.spinner.hide();
          this.Carregar();

      },
      (error: any)=>{
        console.error(error);
        this.toastr.error('Erro ao tentar deletar o usuatio', 'Erro');
      },
      ()=>   this.spinner.hide(),
    );

  }

  decline(): void {
    this.modalRef.hide();
    this.isvalid= false;
  }

  DetalharEvento(id: number): void{
    this.router.navigate([`User/detalhe/${id}`]);
  }



}
