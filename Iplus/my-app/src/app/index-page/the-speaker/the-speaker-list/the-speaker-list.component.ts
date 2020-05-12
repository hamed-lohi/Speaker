import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
//import { SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs';

import { ActivatedRoute, Router } from '@angular/router';
import { TheSpeakerService } from '../the-speaker.service';
import { switchMap } from 'rxjs/operators';

import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogComponent } from "./dialog/dialog.component";
//import { ITheSpeaker, TheSpeakerData } from "./the-speaker";
import { BaseComponent } from "src/app/shared/base.component";
import { DialogService } from "src/app/general/dialog.service";

/**
 * @title Table with pagination
 */
@Component({
    selector: 'app-the-speaker-list',
    templateUrl: 'the-speaker-list.component.html',
    styleUrls: ['the-speaker-list.component.css']
})
export class TheSpeakerListComponent extends BaseComponent implements OnInit {

    speakersData: any[] = null;
    selectListSpeechFields: any[];
    selectedId: number;
    private _speechFieldIds: number[] = null;
    activityId: number;
    selectListActivityTypes: any[];

    constructor(
        private service: TheSpeakerService,
        private route: ActivatedRoute,
        public dialogService: DialogService,
        public dialog: MatDialog,
        private router: Router,
    ) {
        super(service);

    }

    ngOnInit() {
        this.baseNgOnInit();

        //this.route.queryParams
        //  .filter(params => params.order)
        //  .subscribe(params => {
        //    console.log(params); // {order: "popular"}
        //    this.order = params.order;
        //    console.log(this.order); // popular
        //      });

        //this.route.queryParams.subscribe(params => {
        //  debugger;
        //  this.activityId = +params['activityTypeId'];
        //});

       

        //this.dataSource = this.dataSource; //MatTableDataSource<any>(this.dataList);
        //this.dataSource.paginator = this.paginator;
    }

    fillDependSelect() {

        this.selectListSpeechFields = [];
        this._speechFieldIds = [];

        this.search();

        if (!this.activityId) {
            return;
        }

        var pid;

        switch (this.activityId) {

            case 301: // استاد و سخنران
                pid = 1;
                break;
            case 302: // نیروی متخصص
                pid = 350;
                break;
            case 303: // راوی دفاع مقدس
                pid = 400;
                break;
            case 304: // قاری قرآن
                pid = 450;
                break;
            case 305: // مداح و ستایشگر
                pid = 500;
                break;
            case 306: // گروه سرود و تواشیح
                pid = 550;
                break;
        }

        this.service.getSelectOptions("ConstApi", "pId=" + pid)
            .subscribe(d => {
                this.selectListSpeechFields = d;
                //this.onChange();
            });

    }

    //baseNgOnInit() {
    //  this.getDataTable();
    //  this.dataSource.paginator = this.paginator;
    //  }

    getDataTable(): void {
        this.service.getData('GetPublishedList')
            .subscribe(d => {
                this.dataSource.data = d;

            this.route.paramMap.subscribe(params => {
              debugger;
              this.activityId = +params.get("id");
              this.fillDependSelect();
            });
                //this.selection.clear();
                //this.getSelectOptions();
            });
    }

    //get speechFieldIds() {
    //    return this._speechFieldIds;
    //}

    set speechFieldIds(val) {
        this._speechFieldIds = val;
        this.search();
    }

    //getSelectOptions(): void {
    //    //this.service.getSelectOptions("ConstApi", "pId=1")
    //    //    .subscribe(d => this.selectListSpeechFields = d);

    //    this.service.getSelectOptions("ConstApi", "pId=300")
    //        .subscribe(d => {
    //            this.selectListActivityTypes = [];

    //            this.selectListActivityTypes.push({
    //                Id: null,
    //                Text: '--- انتخاب کنید ---',
    //                ImageUrl: ''
    //            })

    //            this.selectListActivityTypes = this.selectListActivityTypes.concat(d);

    //            this.fillDependSelect();
    //        });

    //}

    openDialog(isNewMode) {

        var select = this.selection.selected[0];

        if (!isNewMode && !select) {
            return this.dialogService.confirm('رکوردی را از جدول انتخاب کنید!');
        }

        const dialogRef = this.dialog.open(DialogComponent,
            {
                width: '60%',
                //data: { name: this.name, animal: this.animal }
                data: isNewMode ? { Id: 0, ImageFileId: null } : select,
            });

        dialogRef.afterClosed().subscribe(result => {
            if (!result) {
                return;
            }

            this.add(result);

            if (isNewMode) {
                //TheSpeakerData.push(result);
                //this.dataList.push(result);

            } else {

                //this.selection.selected[0] = result;
            }

            this.service.openSnackBar("با موفقیت انجام شد.", 'عملیات');

        });
    }

    deleteRecords() {

        this.delete(this.selection.selected[0]);
        //return this.dialogService.confirm('برای حذف اطمینان دارید ؟');
    }

    showDetail(speakerId: number) {
        //this.service.selectedSpeakerId = speakerId;
        this.router.navigate(['/index-page/the-speaker/detail', speakerId]);
        //this.router.navigate(['/superheroes', { id: heroId, foo: 'foo' }]);
    }

    search() {

        if (this.speakersData == null) {
            this.speakersData = this.dataSource.data;
        }

        if (this.activityId) {
            this.dataSource.data =
                this.speakersData.filter(a => a.SSActivityId == this.activityId);

            if (this._speechFieldIds && this._speechFieldIds.length != 0) {
                this.dataSource.data =
                    this.speakersData.filter(a => a.SSActivityId == this.activityId && a.TblSpeechFieldIds.some(b => this._speechFieldIds.some(c => c == b)));
            }

        }
        else {
            this.dataSource.data = this.speakersData;
        }


        //var qparam = `speechFieldIds=${this._speechFieldIds.toString()}`;
        //  this.service.getDataTable(qparam)
        //.subscribe(d => {
        //  this.dataSource.data = d;
        //  this.selection.clear();

        //  this.categories = this.dataSource.data;
        //  this.dataSource.data = d.find(x => x.Id == this.selectedRecord.Id).TblConsts;
        //});
    }

}
