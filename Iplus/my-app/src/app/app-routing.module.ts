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

//import { ComposeMessageComponent } from './general/compose-message/compose-message.component';

//import { AuthGuard } from './auth/auth.guard';
import { SelectivePreloadingStrategyService } from './general/selective-preloading-strategy.service';
import { PageNotFoundComponent } from "src/app/general/page-not-found/page-not-found.component";
import { LoginComponent } from "src/app/authentication/login/login.component";
import { AuthGuard } from "src/app/authentication/_guards/auth.guard";
import { RegisterComponent } from "src/app/authentication/register/register.component";
import { Role } from "src/app/authentication/_models/role";
import AppAdmin = Role.AppAdmin;


const appRoutes: Routes = [
    //{
    //  path: 'compose',
    //  component: ComposeMessageComponent,
    //  outlet: 'popup'
    //},
    //{
    //  path: 'admin',
    //  loadChildren: './admin/admin.module#AdminModule',
    //  canLoad: [AuthGuard]
    //},
    //{
    //  path: 'crisis-center',
    //  loadChildren: './crisis-center/crisis-center.module#CrisisCenterModule',
    //  data: { preload: true }
    //},
    //{
    //  path: 'admin-panel',
    //  loadChildren: './admin-panel/admin-panel.module#AdminPanelModule',
    //  data: { preload: true }
    //},
    {
        path: 'index-page',
        redirectTo: '/index-page/main-page',
        pathMatch: 'full'
    },
    {
        path: 'admin-panel',
        redirectTo: '/admin-panel/the-speaker',
        pathMatch: 'full',
        canActivate: [AuthGuard],
        data: { roles: [Role.AppAdmin, Role.Admin] } 
        //canLoad: [AuthGuard]
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: '',
        redirectTo: '/index-page/main-page',
        pathMatch: 'full'
    },

    //{ path: 'Home/AdminPanel', redirectTo: '/superheroes', pathMatch: 'full' },
    { path: '*', component: PageNotFoundComponent }
    //{ path: '**', redirectTo: '' }
];

@NgModule({
    imports: [
        RouterModule.forRoot(
            appRoutes,
            {
                enableTracing: false, // <-- debugging purposes only
                preloadingStrategy: SelectivePreloadingStrategyService,
            }
        )
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }
