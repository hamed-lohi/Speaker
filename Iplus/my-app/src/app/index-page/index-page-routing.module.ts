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
//import { SelectivePreloadingStrategyService } from '../selective-preloading-strategy.service';
//import { TheSpeakerComponent } from "src/app/index-page/the-speaker/the-speaker/the-speaker.component";
import { IndexPageComponent } from "src/app/index-page/index-page.component";
import { PageNotFoundComponent } from "src/app/general/page-not-found/page-not-found.component";
import { AboutComponent } from "src/app/index-page/about/about.component";
import { ContactUsComponent } from "src/app/index-page/contact-us/contact-us.component";
import { MainUserComponent } from "src/app/authentication/user-home/main/main-user.component";
import { AuthGuard } from "src/app/authentication/_guards/auth.guard";


const appRoutes: Routes = [
    {

        //path: 'admin-panel',
        //component: AdminPanelComponent 
        path: 'index-page',
        component: IndexPageComponent,
        children: [
            {
                path: 'main-page',
            //loadChildren: './main-page/main-page.module#MainPageModule',
            loadChildren: () => import('./main-page/main-page.module').then(m => m.MainPageModule)
                //data: { preload: true }
            },
            {
                path: 'the-speaker',
                //loadChildren: 'src/app/index-page/the-speaker/the-speaker.module#TheSpeakerModule',
              loadChildren: () => import('src/app/index-page/the-speaker/the-speaker.module').then(m => m.TheSpeakerModule)
            },
            {
                path: 'post',
                //loadChildren: 'src/app/index-page/post/post.module#PostModule',
              loadChildren: () => import('src/app/index-page/post/post.module').then(m => m.PostModule)
            },
          
            {
                path: 'account/:id',
                component: MainUserComponent,
                canActivate: [AuthGuard],
                //loadChildren: 'src/app/authentication/user-home/user-home.module#UserHomeModule',
                //pathMatch: 'full'
            },
            //{
            //    path: 'speaker-request',
            //    loadChildren: 'src/app/index-page/speaker-request/the-speaker.module#SpeakerRequestModule',
            //  },

            {
                path: 'about',
                //loadChildren: 'src/app/index-page/the-speaker/the-speaker.module#TheSpeakerModule',
                component: AboutComponent,
            },
            {
                path: 'contact-us',
                //loadChildren: 'src/app/index-page/the-speaker/the-speaker.module#TheSpeakerModule',
                component: ContactUsComponent,
            },

            //{
            //    path: 'crisis-center',
            //    loadChildren: 'src/app/index-page/crisis-center/crisis-center.module#CrisisCenterModule',
            //    data: { preload: true }
            //},

            {
                path: '*',
                component: PageNotFoundComponent,
            },
            //{
            //    path: '**',
            //    loadChildren: './the-speaker/the-speaker.module#TheSpeakerModule',
            //    //redirectTo: '/index-page/the-speaker',
            //    //pathMatch: 'full'

            //    //children: [
            //    //    {
            //    //        path: ':id',
            //    //        component: CrisisDetailComponent,
            //    //        canDeactivate: [CanDeactivateGuard],
            //    //        resolve: {
            //    //            crisis: CrisisDetailResolverService
            //    //        }
            //    //    },
            //    //    {
            //    //        path: '',
            //    //        component: CrisisCenterHomeComponent
            //    //    }
            //    //]
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
export class IndexPageRoutingModule { }
