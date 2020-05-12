//import { Component } from '@angular/core';

//@Component({
//  selector: 'app-root',
//  templateUrl: './app.component.html',
//  styleUrls: ['./app.component.css']
//})
//export class AppComponent {
//  title = 'my-project';
//}

import {Component} from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { RouterOutlet, Router } from '@angular/router';
import { Location } from '@angular/common';
import { slideInAnimation } from "src/app/general/animations";
import { AuthenticationService } from "src/app/authentication/_services";

/** @title Fixed sidenav */
@Component({
  selector: 'admin-panel-tag',
  templateUrl: 'admin-panel.component.html',
  styleUrls: ['admin-panel.component.css'],
  animations: [slideInAnimation]
})
export class AdminPanelComponent {
  options: FormGroup;

  constructor(fb: FormBuilder, public location: Location, public authenticationService: AuthenticationService, private router: Router) {
    this.options = fb.group({
      bottom: 0,
      fixed: false,
      top: 0
    });
  }

  getIsCurrentCategory(route: string) {
    //console.log(route);
    console.log("------"+this.location.path());
    //console.log(this.location.path().startsWith(route));
    return (this.location.path().startsWith(route));
  }

  shouldRun = true; //[/(^|\.)plnkr\.co$/, /(^|\.)stackblitz\.io$/].some(h => h.test(window.location.host));

  getAnimationData(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData['animation'];
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }

}

