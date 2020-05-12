import { Component, OnInit } from '@angular/core';
import { BaseService } from "src/app/shared/base.service";
//import { TheSpeakerService } from "src/app/admin-panel/the-speaker/the-speaker.service";
//import { MatTableDataSource } from "@angular/material/table";
import { SelectionModel } from "@angular/cdk/collections";
import { ViewChild, Injectable } from "@angular/core";
//import { MatPaginator } from "@angular/material/paginator";
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

//@Component({
//selector: '---',
//templateUrl: './heroes.component.html',
//styleUrls: ['./heroes.component.css']
//})

//@Injectable()
export class BaseComponent {
    dataList = new Array();
    dataSource = new MatTableDataSource();
    selection = new SelectionModel<any>(true, []);

  @ViewChild(MatPaginator, /* TODO: add static flag */ { static: false })
    paginator: MatPaginator;

  @ViewChild(MatSort, /* TODO: add static flag */ { static: false })
    sort: MatSort;

    constructor(private currentService: any) {
        //this.baseNgOnInit()
    }

    baseNgOnInit() {
        this.getDataTable();
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    getAll(): void {
        this.currentService.getAll()
            .subscribe(d => this.dataList = d);
    }

    getDataTable(): void {
        this.currentService.getDataTable()
            .subscribe(d => {
                this.dataSource.data = d;
                this.selection.clear();

            });
    }

    add(newRec: any): void {
        if (!newRec) { return; }

        this.currentService.add(newRec)
            .subscribe(d => {

                this.currentService.openSnackBar("با موفقیت انجام شد.", 'عملیات');

                //if (newRec.Id == 0) {
                //    //newRec.Id = d;
                //    //this.dataList.push(newRec);
                //    //this.dataSource.data.push(newRec);

                //    //this.dataSource._updateChangeSubscription();
                //}

                this.getDataTable();

            });
    }

    delete(delRec: any): void {

        //return this.currentService.confirm('رکوردی را از جدول انتخاب کنید!');

        this.currentService.delete(delRec)
            .subscribe(a => {
                this.currentService.openSnackBar("با موفقیت انجام شد.", 'عملیات');
                this.dataList = this.dataList.filter(h => h !== delRec);
                this.dataSource.data = this.dataSource.data.filter(h => h !== delRec);
                this.selection.clear();
            },
            error => this.currentService.openSnackBar(error.Message, 'خطا'));

    }

}
