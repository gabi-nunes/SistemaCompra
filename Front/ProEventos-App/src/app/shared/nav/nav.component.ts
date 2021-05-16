import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { user } from 'src/app/models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent implements OnInit {
  isCollapsed = true;
  usuario: any;
  isUser: boolean = false;
  isFornecedor: boolean= false;
  constructor(private router: Router, private userService: UserService) { }


  ngOnInit(): void {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.usuario= JSON.parse(userJson);
    this.isUserByEmail();
  }

  isUserByEmail(){
    this.userService.getIsUserByEmail(this.usuario.email).subscribe(
      (result: any) => {
        this.isUser= result;
        this.isFornecedor= !result;
      }
    );
  }

  showMenu(): boolean{
    return this.router.url !== '/user/login';
  }

  showmeun2(): boolean{
    return this.router.url !== '/user/recuperar';
  }
}
