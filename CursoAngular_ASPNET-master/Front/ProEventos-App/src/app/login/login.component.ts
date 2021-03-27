import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  private router: Router;

  constructor( private http: HttpClient, router: Router) {this.router = router; }

  ngOnInit(): void {
  }

  goToLogin() {
    this.router.navigate(['/login']);
}


}
