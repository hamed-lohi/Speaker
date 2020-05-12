import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { ConstListComponent } from './const-list/const-list.component';
import { ConstRoutingModule } from './const-routing.module';
import { MaterialsModule } from "../../shared/materials.module";
import { DialogComponent } from "./const-list/dialog/dialog.component";
//import { AngularFileUploaderModule } from "angular-file-uploader";
//import { CKEditorModule } from 'ng2-ckeditor';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ConstRoutingModule,
        MaterialsModule,
        //AngularFileUploaderModule,
        //CKEditorModule,

    ],
    entryComponents: [ConstListComponent, DialogComponent],
    declarations: [
        ConstListComponent,
        DialogComponent,
    ]
})
export class ConstModule { }
