import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
//import { SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs';

import { ActivatedRoute, Router } from '@angular/router';
import { PostService } from '../post.service';
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
    selector: 'app-post-list',
    templateUrl: 'post-list.component.html',
    styleUrls: ['post-list.component.css']
})
export class PostListComponent extends BaseComponent implements OnInit {

    postsData: any[];
    //selectListSpeechFields: any[];
    selectedId: number;
    //private _speechFieldIds: number[] = null;
    postTypeId: number;

    constructor(
        private service: PostService,
        private route: ActivatedRoute,
        public dialogService: DialogService,
        public dialog: MatDialog,
        private router: Router,
    ) {
        super(service);

        //this.getSelectOptions();
    }

    ngOnInit() {
        //this.baseNgOnInit();

        //this.dataSource = this.dataSource; //MatTableDataSource<any>(this.dataList);
        //this.dataSource.paginator = this.paginator;
      this.route.paramMap.subscribe(params => {
        this.postTypeId = +params.get("id");

        this.service.getData('GetPostsList', 'SSPostType=' + this.postTypeId) // 201
          .subscribe(a => this.postsData = a);
      });

    }

    //get speechFieldIds() {
    //    return this._speechFieldIds;
    //}

    //set speechFieldIds(val) {
    //    this._speechFieldIds = val;
    //    //this.search();
    //}

    //getSelectOptions(): void {
    //    this.service.getSelectOptions("ConstApi", "pId=1")
    //        .subscribe(d => this.selectListSpeechFields = d);

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

    showDetail(postId: number) {
        //this.service.selectedSpeakerId = speakerId;
        this.router.navigate(['/index-page/post/detail', postId]);
        //this.router.navigate(['/superheroes', { id: heroId, foo: 'foo' }]);
    }

    //search() {

    //    if (this.postsData == null) {
    //        this.postsData = this.dataSource.data;
    //    }

    //    if (this._speechFieldIds.length == 0) {

    //        this.dataSource.data = this.postsData;
    //    } else {

    //      this.dataSource.data =
    //          this.postsData.filter(a => a.TblSpeechFieldIds.some(b => this._speechFieldIds.some(c => c == b)));
    //    }
      
    //}

}
