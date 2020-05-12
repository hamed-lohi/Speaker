
//import { BrowserModule } from '@angular/platform-browser';
import { MaterialsModule } from "../shared/materials.module";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NgModule } from '@angular/core';
import { IndexPageComponent } from './index-page.component';
//import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { IndexPageRoutingModule } from './index-page-routing.module';
import { HeroesModule } from './heroes/heroes.module';
import { HttpClientModule } from "@angular/common/http";
import { WINDOW_PROVIDERS } from "src/app/shared/base.utility";
import { CommonModule } from "@angular/common";
import { TheSpeakerModule } from "src/app/index-page/the-speaker/the-speaker.module";
import { MainPageModule } from "src/app/index-page/main-page/main-page.module";
import { PageNotFoundComponent } from "src/app/general/page-not-found/page-not-found.component";
import { AboutComponent } from "src/app/index-page/about/about.component";
import { ContactUsComponent } from "src/app/index-page/contact-us/contact-us.component";
import { PostModule } from "src/app/index-page/post/post.module";
import { UserHomeModule } from "src/app/authentication/user-home/user-home.module";
import { NgMatSearchBarModule } from 'ng-mat-search-bar';
import { DialogComponent } from "src/app/index-page/search-dialog/dialog.component";

@NgModule({
    declarations: [
        IndexPageComponent,
        AboutComponent,
        ContactUsComponent,
        //PageNotFoundComponent
        DialogComponent,
    ],
    entryComponents: [DialogComponent],
    imports: [
        //BrowserModule,
        CommonModule,
        //BrowserAnimationsModule,
        MaterialsModule,
        FormsModule,
        ReactiveFormsModule,
        HeroesModule,
        TheSpeakerModule,
        MainPageModule,
        PostModule,
        IndexPageRoutingModule,
        HttpClientModule,
        UserHomeModule,
        NgMatSearchBarModule,


    ],

    //providers: [WINDOW_PROVIDERS,],// <- add WINDOW_PROVIDERS here

})
export class IndexPageModule {
    // Diagnostic only: inspect router configuration
    constructor(router: Router) {
        // Use a custom replacer to display function names in the route configs
        // const replacer = (key, value) => (typeof value === 'function') ? value.name : value;

        // console.log('Routes: ', JSON.stringify(router.config, replacer, 2));
    }
}
