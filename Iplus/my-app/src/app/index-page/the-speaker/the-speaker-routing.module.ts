import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TheSpeakerListComponent } from './the-speaker-list/the-speaker-list.component';
import { TheSpeakerDetailComponent } from "src/app/index-page/the-speaker/the-speaker-list/the-speaker-detail/the-speaker-detail.component";
import { SpeakerRequestComponent } from "src/app/index-page/the-speaker/the-speaker-list/speaker-request/speaker-request.component";
import { AuthGuard } from "src/app/authentication/_guards/auth.guard";

//import { CanDeactivateGuard } from '../can-deactivate.guard';

const theSpeakerRoutes: Routes = [

    //{
    //    path: '',
    //    component: TheSpeakerListComponent,
    //    //children : []
    //},

    {
        path: 'detail/:id',
        component: TheSpeakerDetailComponent,
        //data: { animation: 'detail' }
    },
    {
        path: 'request',
        component: SpeakerRequestComponent,
        canActivate: [AuthGuard],
        //data: { animation: 'detail' }
    },
    {
        path: 'request/:id',
        component: SpeakerRequestComponent,
        canActivate: [AuthGuard],
        //data: { animation: 'detail' }
    },
    {
        path: ':id',
        component: TheSpeakerListComponent,
        //children : []
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
export class TheSpeakerRoutingModule { }
