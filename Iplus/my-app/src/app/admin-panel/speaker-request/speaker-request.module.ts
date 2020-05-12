import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { SpeakerRequestHomeComponent } from './speaker-request-home/speaker-request-home.component';
import { SpeakerRequestListComponent } from './speaker-request-list/speaker-request-list.component';
import { SpeakerRequestComponent } from './speaker-request/speaker-request.component';
import { SpeakerRequestRoutingModule } from './speaker-request-routing.module';
import { MaterialsModule } from "../../shared/materials.module";
import { DialogComponent } from "./speaker-request-list/dialog/dialog.component";


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        SpeakerRequestRoutingModule,
        MaterialsModule,
    ],

    exports: [SpeakerRequestListComponent],

    entryComponents: [SpeakerRequestListComponent, DialogComponent],

    declarations: [
        SpeakerRequestComponent,
        SpeakerRequestListComponent,
        SpeakerRequestHomeComponent,
        DialogComponent,
    ]
})
export class SpeakerRequestModule { }
