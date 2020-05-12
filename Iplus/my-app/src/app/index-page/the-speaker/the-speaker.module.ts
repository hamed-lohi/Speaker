import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { TheSpeakerListComponent } from './the-speaker-list/the-speaker-list.component';
import { TheSpeakerRoutingModule } from './the-speaker-routing.module';
import { MaterialsModule } from "../../shared/materials.module";
import { DialogComponent } from "./the-speaker-list/dialog/dialog.component";
import { TheSpeakerDetailComponent } from "src/app/index-page/the-speaker/the-speaker-list/the-speaker-detail/the-speaker-detail.component";
import { SpeakerRequestComponent } from "src/app/index-page/the-speaker/the-speaker-list/speaker-request/speaker-request.component";


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        TheSpeakerRoutingModule,
        MaterialsModule,
    ],
    entryComponents: [TheSpeakerListComponent, DialogComponent],
    declarations: [
        TheSpeakerListComponent,
        DialogComponent,
        TheSpeakerDetailComponent,
        SpeakerRequestComponent,
    ]
})
export class TheSpeakerModule { }
