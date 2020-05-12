import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
//import { SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs';

import { ActivatedRoute } from '@angular/router';
import { PostService } from '../post.service';
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
    selector: 'app-post-list',
    templateUrl: 'post-list.component.html',
    styleUrls: ['post-list.component.css']
})
export class PostListComponent extends BaseComponent implements OnInit {

    constructor(
        private service: PostService,
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

    selectedId: number;

    displayedColumns: string[] = ['select', 'ImageUrl',
        'Title', 'PostType', 'IsPublished', 'FullName', 'ViewCount', 'Priority',
      ];
    //dataSource = new MatTableDataSource<ITheSpeaker>(TheSpeakerData);
    //dataSource = new MatTableDataSource(); //= new MatTableDataSource<any>(this.dataList);

    

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

    openDialog(isNewMode) {

        var select = this.selection.selected[0];

        if (!isNewMode && !select) {
            //return this.dialogService.confirm('رکوردی را از جدول انتخاب کنید!');
            return this.service.confirm('رکوردی را از جدول انتخاب کنید!');
        }

        const dialogRef = this.dialog.open(DialogComponent,
            {
                width: '90%',
                height: '95%',
                //data: { name: this.name, animal: this.animal }
                data: { Id: (isNewMode ? 0 : select.Id) , ImageFileId: null},
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

            //this.service.openSnackBar("با موفقیت انجام شد.", 'عملیات');

        });
    }

    deleteRecords() {

        this.delete(this.selection.selected[0]);
        //return this.dialogService.confirm('برای حذف اطمینان دارید ؟');
    }

}
