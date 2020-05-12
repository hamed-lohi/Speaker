import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { PostListComponent } from './post-list/post-list.component';
import { PostRoutingModule } from './post-routing.module';
import { MaterialsModule } from "../../shared/materials.module";
import { DialogComponent } from "./post-list/dialog/dialog.component";
import { CKEditorModule } from 'ng2-ckeditor';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        PostRoutingModule,
        MaterialsModule,
        CKEditorModule,

    ],
    entryComponents: [PostListComponent, DialogComponent],
    declarations: [
        PostListComponent,
        DialogComponent,
    ]
})
export class PostModule { }
