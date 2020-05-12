import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageListComponent } from './main-page-list/main-page-list.component';

//import { CanDeactivateGuard } from '../can-deactivate.guard';

const theSpeakerRoutes: Routes = [

  {
    path: '',
    component: MainPageListComponent,
  },
];


@NgModule({
  imports: [
      RouterModule.forChild(theSpeakerRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class MainPageRoutingModule { }
