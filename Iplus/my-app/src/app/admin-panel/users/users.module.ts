import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { UsersListComponent } from './users-list/users-list.component';
import { UsersRoutingModule } from './users-routing.module';
import { MaterialsModule } from "../../shared/materials.module";
import { DialogComponent } from "./users-list/dialog/dialog.component";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
      UsersRoutingModule,
        MaterialsModule,

    ],

    //exports: [UsersListComponent],

    entryComponents: [UsersListComponent, DialogComponent],

    declarations: [
      UsersListComponent,
        DialogComponent,
    ]
})
export class UsersModule { }
