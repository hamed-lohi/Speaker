import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { TheSpeakerHomeComponent } from './the-speaker-home/the-speaker-home.component';
import { TheSpeakerListComponent } from './the-speaker-list/the-speaker-list.component';
import { TheSpeakerComponent } from './the-speaker/the-speaker.component';
import { TheSpeakerRoutingModule } from './the-speaker-routing.module';
import { MaterialsModule } from "../../shared/materials.module";
import { DialogComponent } from "./the-speaker-list/dialog/dialog.component";
import { CKEditorModule } from 'ng2-ckeditor';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        TheSpeakerRoutingModule,
        MaterialsModule,
        CKEditorModule,

    ],

    exports: [TheSpeakerListComponent],

    entryComponents: [TheSpeakerListComponent, DialogComponent],

    declarations: [
        TheSpeakerComponent,
        TheSpeakerListComponent,
        TheSpeakerHomeComponent,
        DialogComponent,
    ]
})
export class TheSpeakerModule { }
