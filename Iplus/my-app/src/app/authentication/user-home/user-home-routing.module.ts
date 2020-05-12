import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainUserComponent } from "src/app/authentication/user-home/main/main-user.component";
import { MyProfileComponent } from "src/app/authentication/user-home/main/my-profile/my-profile.component";
import { MyAccountComponent } from "src/app/authentication/user-home/main/my-account/my-account.component";

//import { CanDeactivateGuard } from '../can-deactivate.guard';

const routes: Routes = [

    {
        path: '',
        component: MainUserComponent,
        //children: [

        //    {
        //        path: '',
        //        component: MyProfileComponent,
        //        outlet: 'account-section'
        //        //children: []
        //    },

        //    {
        //        path: 'my-profile',
        //        component: MyProfileComponent,
        //        outlet: 'accountsection'
        //        //children: []
        //    },
        //    {
        //        path: 'my-account',
        //        component: MyAccountComponent,
        //        outlet: 'accountsection'
        //        //children: []
        //    }

        //]
    },

    //{
    //    path: 'detail/:id',
    //    component: TheSpeakerDetailComponent,
    //    //data: { animation: 'detail' }
    //},
    //{
    //    path: 'request',
    //    component: SpeakerRequestComponent,
    //    //data: { animation: 'detail' }
    //},
    //{
    //    path: 'request/:id',
    //    component: SpeakerRequestComponent,
    //    //data: { animation: 'detail' }
    //},
];

//const crisisCenterRoutes: Routes = [
//  {
//    path: '',
//    component: CrisisCenterComponent,
//    children: [
//      {
//        path: '',
//        component: CrisisListComponent,
//        children: [
//          {
//            path: ':id',
//            component: CrisisDetailComponent,
//            canDeactivate: [CanDeactivateGuard],
//            resolve: {
//              crisis: CrisisDetailResolverService
//            }
//          },
//          {
//            path: '',
//            component: CrisisCenterHomeComponent
//          }
//        ]
//      }
//    ]
//  }
//];


@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class UserHomeRoutingModule { }
