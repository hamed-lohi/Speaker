import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
//import { SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs';

import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';

import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogComponent } from "./dialog/dialog.component";
//import { ITheSpeaker, TheSpeakerData } from "./the-speaker";
import { BaseComponent } from "src/app/shared/base.component";
import { DialogService } from "src/app/general/dialog.service";
import { UserHomeService } from "src/app/authentication/user-home/user-home.service";
import { Location } from '@angular/common';

/**
 * @title Table with pagination
 */
@Component({
    selector: 'app-main-user',
    templateUrl: 'main-user.component.html',
    styleUrls: ['main-user.component.css']
})
export class MainUserComponent extends BaseComponent implements OnInit { 

    @Input() tabType: number = 3;// profile=1,account = 2

    speakersData: any[] = null;
    selectListSpeechFields: any[];
    selectedId: number;
    private _speechFieldIds: number[] = null;

    constructor(
        private service: UserHomeService,
        private route: ActivatedRoute,
        public dialogService: DialogService,
        public dialog: MatDialog,
        private router: Router,
        private _location: Location
    ) {
        super(service);

        //this.getSelectOptions();
    }

    ngOnInit() {
        //this.baseNgOnInit();

        //this.dataSource = this.dataSource; //MatTableDataSource<any>(this.dataList);
        //this.dataSource.paginator = this.paginator;

      this.route.paramMap.subscribe(params => {
          this.tabType = +params.get("id");
      });

    }

    //get speechFieldIds() {
    //    return this._speechFieldIds;
    //}

    set speechFieldIds(val) {
        this._speechFieldIds = val;
        this.search();
    }

    getSelectOptions(): void {
        this.service.getSelectOptions("ConstApi", "pId=1")
            .subscribe(d => this.selectListSpeechFields = d);

    }

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

        if (this._speechFieldIds.length == 0) {

            this.dataSource.data = this.speakersData;
        } else {

            this.dataSource.data =
                this.speakersData.filter(a => a.TblSpeechFieldIds.some(b => this._speechFieldIds.some(c => c == b)));
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

    backClicked() {
        this._location.back();
    }

}
