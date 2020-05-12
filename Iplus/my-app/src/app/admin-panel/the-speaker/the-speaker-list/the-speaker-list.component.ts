import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
//import { SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs';

import { ActivatedRoute } from '@angular/router';
import { TheSpeakerService } from '../the-speaker.service';
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
    selector: 'app-the-speaker-list',
    templateUrl: 'the-speaker-list.component.html',
    styleUrls: ['the-speaker-list.component.css']
})
export class TheSpeakerListComponent extends BaseComponent implements OnInit {

  @Input() viewType: number = 0;// profile=1,adminPanel = 0

    constructor(
        private service: TheSpeakerService,
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

    get displayedColumns(): string[]  {

        if (this.viewType == 1) {
          return ['select', 'ImageUrl',
              'FName', 'LName', 'Activity', 'CityName', //, 'TblSpeechFieldNames'
          ];
        }

      return ['select', 'ImageUrl',
          'FName', 'LName', 'Activity', 'IsApproved',
          'IsPublished', 'FullName', 'Priority', 'CityName',
        ];

    }

    //displayedColumns: string[] = ['select', 'ImageUrl',
    //    'FName', 'LName', 'TblSpeechFieldNames', 'IsApproved',
    //    'IsPublished', 'FullName', 'Priority', 'CityName',
    //  ];

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
              data: { viewType: this.viewType, Id: (isNewMode ? 0 : select.Id) , ImageFileId: null},
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

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

}
