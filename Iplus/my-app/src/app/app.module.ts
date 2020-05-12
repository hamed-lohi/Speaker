
//import { BrowserModule } from '@angular/platform-browser';
import { MaterialsModule } from "./shared/materials.module";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
//import { ComposeMessageComponent } from './compose-message/compose-message.component';

import { AppRoutingModule } from './app-routing.module';
//import { HeroesModule } from './general/heroes/heroes.module';
//import { AuthModule } from './auth/auth.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { WINDOW_PROVIDERS } from "src/app/shared/base.utility";
import { CommonModule } from "@angular/common";
import { AdminPanelModule } from "src/app/admin-panel/admin-panel.module";
import { IndexPageModule } from "src/app/index-page/index-page.module";
import { PageNotFoundComponent } from "src/app/general/page-not-found/page-not-found.component";
import { LoginComponent } from "src/app/authentication/login/login.component";
import { RegisterComponent } from "src/app/authentication/register/register.component";
import { JwtInterceptor } from "src/app/authentication/_helpers/jwt.interceptor";
import { ErrorInterceptor } from "src/app/authentication/_helpers/error.interceptor";
import { fakeBackendProvider } from "src/app/authentication/_helpers/fake-backend";

import { LoaderInterceptorService } from './general/loader/loader-interceptor.service';
import { LoaderComponent } from "src/app/general/loader/loader.component";

@NgModule({
    declarations: [
        AppComponent,
        //ComposeMessageComponent,
        PageNotFoundComponent,
        LoginComponent,
        RegisterComponent,
        LoaderComponent

    ],
    imports: [
        //BrowserModule,
        CommonModule,
        BrowserAnimationsModule,
        MaterialsModule,
        FormsModule,
        ReactiveFormsModule,
        //HeroesModule,
        //AuthModule,
        AppRoutingModule,
        HttpClientModule,
        AdminPanelModule,
        IndexPageModule,

    ],

    providers: [WINDOW_PROVIDERS,
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptorService, multi: true },
        // provider used to create fake backend
        fakeBackendProvider

    ],// <- add WINDOW_PROVIDERS here

    bootstrap: [AppComponent]
})
export class AppModule {
    // Diagnostic only: inspect router configuration
    constructor(router: Router) {
        // Use a custom replacer to display function names in the route configs
        // const replacer = (key, value) => (typeof value === 'function') ? value.name : value;

        // console.log('Routes: ', JSON.stringify(router.config, replacer, 2));
    }
}
