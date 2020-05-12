import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
//import { SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { MainPageService } from '../main-page.service';
import { switchMap } from 'rxjs/operators';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogComponent } from "./dialog/dialog.component";
//import { ITheSpeaker, TheSpeakerData } from "./the-speaker";
import { BaseComponent } from "src/app/shared/base.component";
import { DialogService } from "src/app/general/dialog.service";

import { Router } from "@angular/router";
import { InjectionToken } from "@angular/core";
import { ScrollStrategy, Overlay } from "@angular/cdk/overlay";
import { AuthenticationService } from "src/app/authentication/_services";

/**
 * @title Table with pagination
 */
@Component({
    selector: 'app-main-page-list',
    templateUrl: 'main-page-list.component.html',
    styleUrls: ['main-page-list.component.css']
})
export class MainPageListComponent extends BaseComponent implements OnInit {


    constructor(
        private service: MainPageService,
        private route: ActivatedRoute,
        private router: Router,
        public dialog: MatDialog,
        private overlay: Overlay,
        public authenticationService: AuthenticationService,
    ) {
        super(service);

    }

    //  ngOnInit() {
    //    this.baseNgOnInit();
    //    //this.dataSource = this.dataSource; //MatTableDataSource<any>(this.dataList);
    //      //this.dataSource.paginator = this.paginator;
    //}

    ngOnInit() {

    }

    showRequestForm() {
        //this.service.selectedSpeakerId = speakerId;
        this.router.navigate(['/index-page/the-speaker/request']);
      //, { msg: ' برای ثبت درخواست سخنران ابتدا باید وارد سایت شوید، و در صورت عدم عضویت، دکمه عضویت را کلیک نمایید ', f:'f' }
        //this.router.navigate(['/superheroes', { id: heroId, foo: 'foo' }]);
    }

    openDialog(isNewMode) {

        //var select = this.selection.selected[0];

        //if (!isNewMode && !select) {
        //  //return this.dialogService.confirm('رکوردی را از جدول انتخاب کنید!');
        //  return this.service.confirm('رکوردی را از جدول انتخاب کنید!');
        //}

        //const MAT_DIALOG_SCROLL_STRATEGY: InjectionToken<() => ScrollStrategy>;

        if (!this.authenticationService.currentUserValue) {

            this.router.navigate(['/login', { msg: ' برای ثبت سخنران ابتدا باید وارد سایت شوید، و در صورت عدم عضویت، دکمه عضویت را کلیک نمایید ', f: 'fo' }]);
            return;
        }

        const dialogRef = this.dialog.open(DialogComponent,
            {
                width: '95%',
                height: '95%',
                //scrollStrategy: this.overlay.scrollStrategies.noop(),// . scrollStrategies.noop(),
                //data: { name: this.name, animal: this.animal }
                data: { Id: 0, ImageFileId: null },
            });

        //dialogRef.afterClosed().subscribe(result => {
        //    if (!result) {
        //        return;
        //    }
        //    this.service.setApiUrl('SpeakerApi');
        //    this.add(result);

        //    if (isNewMode) {
        //        //TheSpeakerData.push(result);
        //        //this.dataList.push(result);

        //    } else {

        //        //this.selection.selected[0] = result;
        //    }

        //    //this.service.openSnackBar("با موفقیت انجام شد.", 'عملیات');

        //});
    }

  

    //tiles: any[] = [
    //  { text: 'One', cols: 3, rows: 1, color: 'lightblue' },
    //  { text: 'Two', cols: 1, rows: 2, color: 'lightgreen' },
    //  { text: 'Three', cols: 1, rows: 1, color: 'lightpink' },
    //  { text: 'Four', cols: 2, rows: 1, color: '#DDBDF1' },
    //];

}
