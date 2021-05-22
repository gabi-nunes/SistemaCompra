import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FamiliaProduto } from 'src/app/models/FamiliaProduto';
import { Fornecedor } from 'src/app/models/Fornecedor';
import { FamiliaProdutoService } from 'src/app/services/familiaProduto.service';
import { FornecedorService } from 'src/app/services/fornecedor.service';

@Component({
  selector: 'app-area-fornecedor',
  templateUrl: './area-fornecedor.component.html',
  styleUrls: ['./area-fornecedor.component.scss']
})
export class AreaFornecedorComponent implements OnInit {
  constructor() { }

  ngOnInit(): void {
  }

}
