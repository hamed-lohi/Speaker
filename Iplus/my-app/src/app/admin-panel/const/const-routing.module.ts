import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ConstListComponent } from './const-list/const-list.component';

//import { CanDeactivateGuard } from '../can-deactivate.guard';

const routes: Routes = [
  {
    path: '',
    component: ConstListComponent,
  }
];


@NgModule({
  imports: [
      RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ConstRoutingModule { }
