//import { NgModule } from '@angular/core';
//import { RouterModule, Routes } from '@angular/router';

//import { DashboardComponent } from './dashboard/dashboard.component';
//import { HeroesComponent } from './heroes/heroes.component';
//import { HeroDetailComponent } from './hero-detail/hero-detail.component';

//const routes: Routes = [
//  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
//  { path: 'dashboard', component: DashboardComponent },
//  { path: 'detail/:id', component: HeroDetailComponent },
//  { path: 'heroes', component: HeroesComponent }
//];

//@NgModule({
//  imports: [RouterModule.forRoot(routes)],
//  exports: [RouterModule]
//})
//export class AppRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ComposeMessageComponent } from './compose-message/compose-message.component';

//import { AuthGuard } from './auth/auth.guard';
//import { SelectivePreloadingStrategyService } from '../selective-preloading-strategy.service';
import { AdminPanelComponent } from "src/app/admin-panel/admin-panel.component";
//import { TheSpeakerComponent } from "src/app/admin-panel/the-speaker/the-speaker/the-speaker.component";
//import { SpeakerRequestComponent } from "src/app/admin-panel/speaker-request/speaker-request/speaker-request.component";
import { PageNotFoundComponent } from "src/app/general/page-not-found/page-not-found.component";
import { AuthGuard } from "src/app/authentication/_guards/auth.guard";
import { Role } from "src/app/authentication/_models/role";


const appRoutes: Routes = [
    {

        //path: 'admin-panel',
        //component: AdminPanelComponent 

        path: 'admin-panel',
        component: AdminPanelComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.AppAdmin, Role.Admin] },
        //canLoad: [AuthGuard],
        children: [
            {
                path: 'the-speaker',
            //loadChildren: './the-speaker/the-speaker.module#TheSpeakerModule',
            loadChildren: () => import('./the-speaker/the-speaker.module').then(m=> m.TheSpeakerModule)
                //component: TheSpeakerComponent,
            },
            {
                path: 'speaker-request',
                //loadChildren: './speaker-request/speaker-request.module#SpeakerRequestModule',
                loadChildren: () => import('./speaker-request/speaker-request.module').then(m => m.SpeakerRequestModule)
                //component: TheSpeakerComponent,
            },
            {
                path: 'const',
                //loadChildren: './const/const.module#ConstModule',
                loadChildren: () => import('./const/const.module').then(m => m.ConstModule)
                //component: TheSpeakerComponent,
            },
            {
                path: 'post',
                //loadChildren: './post/post.module#PostModule',
                loadChildren: () => import('./post/post.module').then(m => m.PostModule)
                //component: TheSpeakerComponent,
            },
            {
                path: 'users',
                //loadChildren: './users/users.module#UsersModule',
                loadChildren: () => import('./users/users.module').then(m => m.UsersModule)
                //component: TheSpeakerComponent,
            },

            {
                path: 'crisis-center',
                //loadChildren: './crisis-center/crisis-center.module#CrisisCenterModule',
                loadChildren: () => import('./crisis-center/crisis-center.module').then(m => m.CrisisCenterModule),
                data: { preload: true }
            },
            {
                path: '*',
                component: PageNotFoundComponent,
            },
            //{
            //  path: '',
            //  //loadChildren: './the-speaker/the-speaker.module#TheSpeakerModule',
            //  redirectTo: 'the-speaker',
            //  //pathMatch: 'full'
            //  //children: [
            //  //    {
            //  //        path: ':id',
            //  //        component: CrisisDetailComponent,
            //  //        canDeactivate: [CanDeactivateGuard],
            //  //        resolve: {
            //  //            crisis: CrisisDetailResolverService
            //  //        }
            //  //    },
            //  //    {
            //  //        path: '',
            //  //        component: CrisisCenterHomeComponent
            //  //    }
            //  //]
            //},

        ]
    }
];

//const appRoutes: Routes = [
//  {
//    path: 'admin-panel',
//    component: AdminPanelComponent
//  },
//  {
//    path: 'compose',
//    component: ComposeMessageComponent,
//    outlet: 'popup'
//  },
//  {
//    path: 'admin',
//    loadChildren: './admin/admin.module#AdminModule',
//    canLoad: [AuthGuard]
//  },
//  {
//    path: 'crisis-center',
//    loadChildren: './crisis-center/crisis-center.module#CrisisCenterModule',
//    data: { preload: true }
//  },
//  {
//    path: 'the-speaker',
//    loadChildren: './the-speaker/the-speaker.module#TheSpeakerModule',
//    data: { preload: true }
//  },
//  { path: '', redirectTo: '/superheroes', pathMatch: 'full' },
//  { path: 'Home/AdminPanel', redirectTo: '/superheroes', pathMatch: 'full' },
//  { path: '**', component: PageNotFoundComponent }
//];

@NgModule({
    //imports: [
    //  RouterModule.forRoot(
    //    appRoutes,
    //    {
    //      enableTracing: false, // <-- debugging purposes only
    //      preloadingStrategy: SelectivePreloadingStrategyService,
    //    }
    //  )
    //],
    //exports: [
    //  RouterModule
    //]

    imports: [
        RouterModule.forChild(appRoutes)
    ],
    exports: [
        RouterModule
    ]

})
export class AdminPanelRoutingModule { }
