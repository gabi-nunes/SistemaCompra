import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NavComponent } from './shared/nav/nav.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  /**
   *
   */
  constructor(private router: Router) {}
  title = 'ProEventos-App';
  public get ShowContainer(): boolean{
    return this.router.url !== '/user/login' && this.router.url !== '/user/recuperar';
  }

  @ViewChild(NavComponent) nav: NavComponent;

  ngAfterViewInit(): void {
    debugger;
    this.nav.ngOnInit();
  }
}
