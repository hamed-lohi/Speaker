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
import { RouterOutlet } from '@angular/router';
import { slideInAnimation } from './general/animations';
import { Location } from '@angular/common';

/** @title Fixed sidenav */
@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html', 
  styleUrls: ['app.component.css'],
  animations: [slideInAnimation]
})
export class AppComponent {
    options: FormGroup; 

  constructor(fb: FormBuilder, public location: Location) {
    this.options = fb.group({
      bottom: 0,
      fixed: false,
      top: 0
    });
  }

  getIsCurrentCategory(route: string) {
    //console.log(route);
    //console.log("------"+this.location.path());
    //console.log(this.location.path().startsWith(route));
    return (this.location.path().startsWith(route));
  }

  shouldRun = true; //[/(^|\.)plnkr\.co$/, /(^|\.)stackblitz\.io$/].some(h => h.test(window.location.host));

  getAnimationData(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData['animation'];
  }

}

