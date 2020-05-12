import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
//import { SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs';

import { ActivatedRoute } from '@angular/router';
import { ConstService } from '../const.service';
import { switchMap } from 'rxjs/operators';

import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogComponent } from "./dialog/dialog.component";
//import { ITheSpeaker, TheSpeakerData } from "./the-speaker";
import { BaseComponent } from "src/app/shared/base.component";
//import { DialogService } from "src/app/general/dialog.service";

/**
 * @title Table with pagination
 */
@Component({
    selector: 'app-const-list',
    templateUrl: 'const-list.component.html',
    styleUrls: ['const-list.component.css']
})
export class ConstListComponent extends BaseComponent implements OnInit {

    constructor(
        private service: ConstService,
        private route: ActivatedRoute,
        //public dialogService: DialogService,
        public dialog: MatDialog,
    ) {
        super(service);

    }

    ngOnInit() {
        this.baseNgOnInit();
        //this.dataSource = this.dataSource; //MatTableDataSource<any>(this.dataList);
        //this.dataSource.paginator = this.paginator;
    }

    //@ViewChild(MatPaginator)
    //paginator: MatPaginator;

    selectedRecord: any;
    pId: number = null;

    displayedColumns: string[] = ['select', 'ImageUrl', 'Id', 'ConstName', 'Priority', 'ConstNumber'];
    //dataSource = new MatTableDataSource<ITheSpeaker>(TheSpeakerData);
    //dataSource = new MatTableDataSource(); //= new MatTableDataSource<any>(this.dataList);

    categories: any;

    getChilds() {
        this.getSelectRecord();

        if (!this.selectedRecord) {
            return;
        }

        this.categories = this.dataSource.data;

        this.dataSource.data = this.selectedRecord.TblConsts;

        this.selection.clear();
    }

    getCategories() {
        this.selectedRecord = null;
        this.dataSource.data = this.categories;
        this.selection.clear();
    }

    /** Whether the number of selected elements matches the total number of rows. */
    isAllSelected() {
        const numSelected = this.selection.selected.length;
        const numRows = this.dataSource.data.length;
        return numSelected === numRows;
    }

    /** Selects all rows if they are not all selected; otherwise clear selection. */
    masterToggle() {

        this.isAllSelected() ? this.selection.clear() : this.dataSource.data.forEach(row => this.selection.select(row));
    }

    // MatPaginator Inputs

    // MatPaginator Output
    //pageEvent: PageEvent;

    //displayedColumns: string[] = ['Id', 'FName', 'LName', 'ImageUrl', 'Categories'];
    //dataSource = new MatTableDataSource<TheSpeaker>(CRISES);

    getSelectRecord() {
        var select = this.selection.selected[0];

        if (!select) {
            //return this.dialogService.confirm('رکوردی را از جدول انتخاب کنید!');
            return this.service.confirm('رکوردی را از جدول انتخاب کنید!');
        }
        this.selectedRecord = select;
    }

    openDialog(isNewMode) {

        var select = this.selection.selected[0];

        if (!isNewMode && !select) {
            //return this.dialogService.confirm('رکوردی را از جدول انتخاب کنید!');
            return this.service.confirm('رکوردی را از جدول انتخاب کنید!');
        }

        const dialogRef = this.dialog.open(DialogComponent,
            {
                width: '85%',
                //data: { name: this.name, animal: this.animal }
                data: isNewMode ? { Id: 0, ImageFileId: null, ImageUrl: null, PId: (this.selectedRecord ? this.selectedRecord.Id : null) } : select
            });

        dialogRef.afterClosed().subscribe(result => {
            if (!result) {
                return;
            }

            this.addConst(result);
            //this.getChilds();
            //this.selectedRecord = null;

        });
    }

    addConst(newRec: any): void {
        if (!newRec) {
            return;
        }
        this.service.add(newRec)
            .subscribe(d => {
                this.service.openSnackBar("با موفقیت انجام شد.", 'عملیات');
                this.getConstDataTable();
            });
    }

    getConstDataTable(): void {
        this.service.getDataTable()
            .subscribe(d => {
                this.dataSource.data = d;
                this.selection.clear();

                this.categories = this.dataSource.data;
                this.dataSource.data = d.find(x => x.Id == this.selectedRecord.Id).TblConsts;
            });
    }

    getDataTable() {
        super.getDataTable();
        this.selectedRecord = null;
    }

    deleteRecords() {

        this.delete(this.selection.selected[0]);
        //return this.dialogService.confirm('برای حذف اطمینان دارید ؟');
    }

}
