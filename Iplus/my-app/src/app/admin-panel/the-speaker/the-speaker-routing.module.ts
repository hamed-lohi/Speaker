import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TheSpeakerHomeComponent } from './the-speaker-home/the-speaker-home.component';
import { TheSpeakerListComponent } from './the-speaker-list/the-speaker-list.component';
import { TheSpeakerComponent } from './the-speaker/the-speaker.component';

//import { CanDeactivateGuard } from '../can-deactivate.guard';

const theSpeakerRoutes: Routes = [
  {
    path: '',
    component: TheSpeakerComponent,
    children: [
      {
        path: '',
        component: TheSpeakerListComponent,
        children: [
          {
            path: '',
            component: TheSpeakerHomeComponent
          }
        ]
      }
    ]
  }
];


@NgModule({
  imports: [
      RouterModule.forChild(theSpeakerRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class TheSpeakerRoutingModule { }
