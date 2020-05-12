import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { PostListComponent } from './post-list/post-list.component';
import { PostRoutingModule } from './post-routing.module';
import { MaterialsModule } from "../../shared/materials.module";
import { DialogComponent } from "./post-list/dialog/dialog.component";
import { PostDetailComponent } from "src/app/index-page/post/post-list/post-detail/post-detail.component";


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        PostRoutingModule,
        MaterialsModule,
    ],
    entryComponents: [PostListComponent, DialogComponent],
    declarations: [
        PostListComponent,
        DialogComponent,
        PostDetailComponent,
    ]
})
export class PostModule { }
