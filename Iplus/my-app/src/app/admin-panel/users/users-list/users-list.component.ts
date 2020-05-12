import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
//import { SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs';

import { ActivatedRoute } from '@angular/router';
import { UsersService } from '../users.service';
import { switchMap } from 'rxjs/operators';

import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogComponent } from "./dialog/dialog.component";
//import { ITheSpeaker, TheSpeakerData } from "./the-speaker";
import { BaseComponent } from "src/app/shared/base.component";
import { UserHomeService } from "src/app/authentication/user-home/user-home.service";
//import { DialogService } from "src/app/general/dialog.service";

/**
 * @title Table with pagination
 */
@Component({
    selector: 'app-users-list',
    templateUrl: 'users-list.component.html',
    styleUrls: ['users-list.component.css']
})
export class UsersListComponent extends BaseComponent implements OnInit {

  @Input() viewType: number = 0;// profile=1,adminPanel = 0

    constructor(
        private service: UsersService,
        private route: ActivatedRoute,
        //public dialogService: DialogService,
        public dialog: MatDialog,
        private userService: UserHomeService
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

        //if (this.viewType == 1) {
        //  return ['select', 'ImageUrl',
        //    'FName', 'LName', 'TblSpeechFieldNames', 'CityName',
        //  ];
        //}

      return ['select',
          'UserName', 'FullName', 'PhoneNumber', 'Email',
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

  openChangePasswordDialog() {

    var select = this.selection.selected[0];

    if (!select) {
      //return this.dialogService.confirm('رکوردی را از جدول انتخاب کنید!');
      return this.service.confirm('کاربر مورد نظر را انتخاب کنید!');
    }

    const dialogRef = this.dialog.open(DialogComponent,
      {
        width: '250px',
        height: '250px',
        //data: { name: this.name, animal: this.animal }
        data: { viewType: this.viewType, Id: select.Id, dialogType: 0, NewPassword: "", ConfirmPassword:""}
        });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        return;
        }

      var accountInfo = {
          Id: result.Id,
          NewPassword: result.NewPassword,
          ConfirmPassword: result.ConfirmPassword,
      };

      this.changePassword(accountInfo);
    });

    }

  changePassword(accountInfo) {
    this.userService.resetPassword(accountInfo)
      .subscribe(a => {
        this.service.openSnackBar("با موفقیت انجام شد.", 'عملیات');
      });
  }

}
