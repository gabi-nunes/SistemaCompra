import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  txtemail: string;
  txtsenha: string;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
  }



  salvarlogin(){
    debugger
    this.userService.login(this.txtemail,this.txtsenha)

  }

}
