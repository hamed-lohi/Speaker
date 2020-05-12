//import { Component } from '@angular/core';

//@Component({
//  selector: 'app-root',
//  templateUrl: './app.component.html',
//  styleUrls: ['./app.component.css']
//})
//export class AppComponent {
//  title = 'my-project';
//}

import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { RouterOutlet, Router } from '@angular/router';
import { Location } from '@angular/common';
import { slideInAnimation } from "src/app/general/animations";
import { AuthenticationService } from "src/app/authentication/_services";
import { TheSpeakerService } from "src/app/index-page/the-speaker/the-speaker.service";
import { DialogService } from "src/app/general/dialog.service";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogComponent } from "src/app/index-page/search-dialog/dialog.component";

/** @title Fixed sidenav */
@Component({
    selector: 'index-page-tag',
    templateUrl: 'index-page.component.html',
    styleUrls: ['index-page.component.css'],
    animations: [slideInAnimation]
})
export class IndexPageComponent {

    options: FormGroup;
    selectListActivityTypes: any[];

    constructor(fb: FormBuilder,
        public location: Location,
        public authenticationService: AuthenticationService,
        private router: Router,
        private service: TheSpeakerService,
        public dialogService: DialogService,
        public dialog: MatDialog, ) {

        this.options = fb.group({
            bottom: 0,
            fixed: false,
            top: 0
        });

        this.service.getSelectOptions("ConstApi", "pId=300")
            .subscribe(d => {
                //this.selectListActivityTypes = [];

                this.selectListActivityTypes = d;

            });

    }

    getIsCurrentCategory(route: string) {
        //console.log(route);
        //console.log("------"+this.location.path());
        //console.log(this.location.path().startsWith(route));
        var isRequest = this.location.path().endsWith('request');
        if (isRequest) {
            return (route == 'request' ? true : false);
        }

        return this.location.path().startsWith(route)
    }

    shouldRun = true; //[/(^|\.)plnkr\.co$/, /(^|\.)stackblitz\.io$/].some(h => h.test(window.location.host));

    getAnimationData(outlet: RouterOutlet) {
        return outlet && outlet.activatedRouteData && outlet.activatedRouteData['animation'];
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }

    showAdminPanelBtn() {
        return (this.authenticationService.currentUserValue &&
            (this.authenticationService.currentUserValue.Role == "AppAdmin" || this.authenticationService.currentUserValue.Role == "Admin"));
    }

    searchBoxOnEnter(value: string) {

        if (value) {
            this.openDialog(value);
        }
    }

    openDialog(searchText: string) {

        //var select = this.selection.selected[0];

        //if (!isNewMode && !select) {
        //    return this.dialogService.confirm('رکوردی را از جدول انتخاب کنید!');
        //}

        const dialogRef = this.dialog.open(DialogComponent,
            {
                width: '85%',
                height: '95%',
                //data: { name: this.name, animal: this.animal }
                data: searchText,
            });

        dialogRef.afterClosed().subscribe(result => {
            if (!result) {
                return;
            }

            if (true) {
                //TheSpeakerData.push(result);
                //this.dataList.push(result);

            } else {

                //this.selection.selected[0] = result;
            }

            this.service.openSnackBar("با موفقیت انجام شد.", 'عملیات');

        });
    }

}

