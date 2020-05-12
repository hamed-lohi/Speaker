import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//import { TheSpeakerHomeComponent } from './the-speaker-home/the-speaker-home.component';
import { UsersListComponent } from './users-list/users-list.component';
//import { TheSpeakerComponent } from './the-speaker/the-speaker.component';

//import { CanDeactivateGuard } from '../can-deactivate.guard';

const routes: Routes = [
  {
    path: '',
    component: UsersListComponent,
    //children: [
    //  {
    //    path: '',
    //    component: TheSpeakerListComponent,
    //    children: [
    //      {
    //        path: '',
    //        component: TheSpeakerHomeComponent
    //      }
    //    ]
    //  }
    //]
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
export class UsersRoutingModule { }
