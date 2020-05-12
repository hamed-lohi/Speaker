import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SpeakerRequestHomeComponent } from './speaker-request-home/speaker-request-home.component';
import { SpeakerRequestListComponent } from './speaker-request-list/speaker-request-list.component';
import { SpeakerRequestComponent } from './speaker-request/speaker-request.component';

//import { CanDeactivateGuard } from '../can-deactivate.guard';

const theSpeakerRoutes: Routes = [
  {
    path: '',
    component: SpeakerRequestComponent,
    children: [
      {
        path: '',
        component: SpeakerRequestListComponent,
        children: [
          {
            path: '',
            component: SpeakerRequestHomeComponent
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
export class SpeakerRequestRoutingModule { }
