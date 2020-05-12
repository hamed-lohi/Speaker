import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { MaterialsModule } from "../../shared/materials.module";
import { DialogComponent } from "./main/dialog/dialog.component";
import { UserHomeRoutingModule } from "src/app/authentication/user-home/user-home-routing.module";
import { MainUserComponent } from "src/app/authentication/user-home/main/main-user.component";
import { MyProfileComponent } from "src/app/authentication/user-home/main/my-profile/my-profile.component";
import { MyAccountComponent } from "src/app/authentication/user-home/main/my-account/my-account.component";
import { ChangePasswordComponent } from "src/app/authentication/user-home/main/change-password/change-password.component";
import { TheSpeakerModule } from "src/app/admin-panel/the-speaker/the-speaker.module";
import { SpeakerRequestModule } from "src/app/admin-panel/speaker-request/speaker-request.module";
//import { TheSpeakerListComponent } from "src/app/admin-panel/the-speaker/the-speaker-list/the-speaker-list.component";


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        UserHomeRoutingModule,
        MaterialsModule,
        TheSpeakerModule,
        SpeakerRequestModule,
    ],

    exports: [MainUserComponent],

    entryComponents: [MainUserComponent, DialogComponent],
    declarations: [
        MainUserComponent,
        DialogComponent,
        MyProfileComponent,
        MyAccountComponent,
        ChangePasswordComponent,
        //TheSpeakerListComponent,
        //SpeakerRequestComponent,
    ]
})
export class UserHomeModule { }
