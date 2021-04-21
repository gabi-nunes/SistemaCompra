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
  user: user;
  constructor(private router: Router) { }


  ngOnInit(): void {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.user = JSON.parse(userJson);

  }

  showMenu(): boolean{
    return this.router.url !== '/user/login';
  }

  showmeun2(): boolean{
    return this.router.url !== '/user/recuperar';
  }
}
