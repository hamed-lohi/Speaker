
//import { BrowserModule } from '@angular/platform-browser';
import { MaterialsModule } from "../shared/materials.module";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NgModule } from '@angular/core';
import { AdminPanelComponent } from './admin-panel.component';
//import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ComposeMessageComponent } from './compose-message/compose-message.component';

import { AdminPanelRoutingModule } from './admin-panel-routing.module';
import { HeroesModule } from './heroes/heroes.module';
//import { AuthModule } from './auth/auth.module';
import { HttpClientModule } from "@angular/common/http";
import { WINDOW_PROVIDERS } from "src/app/shared/base.utility";
import { CommonModule } from "@angular/common";
import { TheSpeakerModule } from "src/app/admin-panel/the-speaker/the-speaker.module";
import { SpeakerRequestModule } from "src/app/admin-panel/speaker-request/speaker-request.module";
import { PageNotFoundComponent } from "src/app/general/page-not-found/page-not-found.component";

@NgModule({
    declarations: [
        AdminPanelComponent,
        ComposeMessageComponent,
        //PageNotFoundComponent
    ],
    imports: [
        //BrowserModule,
        CommonModule,
        //BrowserAnimationsModule,
        MaterialsModule,
        FormsModule,
        ReactiveFormsModule,
        HeroesModule,
        TheSpeakerModule,
        SpeakerRequestModule,
        //AuthModule,
        AdminPanelRoutingModule,
        HttpClientModule
    ],

    //providers: [WINDOW_PROVIDERS,],// <- add WINDOW_PROVIDERS here

})
export class AdminPanelModule {
    // Diagnostic only: inspect router configuration
    constructor(router: Router) {
        // Use a custom replacer to display function names in the route configs
        // const replacer = (key, value) => (typeof value === 'function') ? value.name : value;

        // console.log('Routes: ', JSON.stringify(router.config, replacer, 2));
    }
}
