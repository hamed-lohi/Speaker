import { Component, Inject } from '@angular/core';
//import { ITheSpeaker } from '../the-speaker';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseComponent } from "src/app/shared/base.component";
import { OnInit, ViewChild } from "@angular/core";
import { WINDOW } from "src/app/shared/base.utility";
import { AuthenticationService } from "src/app/authentication/_services";
import { Location } from '@angular/common';
import { MainPageService } from "src/app/index-page/main-page/main-page.service";

declare var $;

@Component({
    selector: 'dialog-speaker',
    templateUrl: 'dialog.component.html',
    styleUrls: ['dialog.component.css']
})
export class DialogComponent implements OnInit {

    name = 'ng2-ckeditor';
    ckeConfig: any;
    mycontent: string;
    log: string = '';

    selectListCity: any[];
    selectListActivityTypes: any[];
    selectListSpeechFields: any[];

    //@ViewChild("myckeditor") ckeditor: any;
    //@ViewChild("educationDescription") educationDescription: any;
    //@ViewChild("teachingDescription") teachingDescription: any;
    //@ViewChild("recordsDescription") recordsDescription: any;
    //@ViewChild("researchDescription") researchDescription: any;
    //@ViewChild("masterDescription") masterDescription: any;

    constructor(
        public dialogRef: MatDialogRef<DialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any,
        private service: MainPageService,
        @Inject(WINDOW) protected window: Window,
        private _location: Location, ) {

    }

    ngOnInit() {
        this.ckeConfig = {
            allowedContent: false,
            //extraPlugins: 'divarea',
            forcePasteAsPlainText: true
        };

        this.getRecordInfo();
    }

    onChange($event: any): void {
        //console.log("onChange");
        //this.log += new Date() + "<br />";
    }

    apiUrl = this.window.location.origin + '/api/FileApi/UploadSingleFile';  // URL to web api

    onNoClick(): void {
        this.dialogRef.close();
    }

    fillDependSelect() {

        if (!this.data.SSActivityId)
            return;

        var pid;

        switch (this.data.SSActivityId) {

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

    getSelectOptions(): void {
        this.service.getSelectOptions("CityApi")
            .subscribe(d => this.selectListCity = d);

        this.service.getSelectOptions("ConstApi", "pId=300")
            .subscribe(d => {
                this.selectListActivityTypes = d;
                this.fillDependSelect();
            });

    }

    getRecordInfo() {

        if (this.data.Id != 0) {

            this.data.EducationDescription = "";
            this.data.ActivityDescription = "";
            this.data.TeachingDescription = "";
            this.data.RecordsDescription = "";
            this.data.ResearchDescription = "";
            this.data.MasterDescription = "";

            this.service.getForEdit(this.data.Id)
                .subscribe(a => {
                    this.data = a;
                    this.getSelectOptions();
                });
        } else {
          this.getSelectOptions();
        }
    }

    add(): void {
        if (!this.data) { return; }

        var msg = '';

        if (!this.data.FName) {
            msg = "a";
        } if (!this.data.LName) {
            msg = "a";
        } if (!this.data.MobileNumber) {
            msg = "a";
        } if (!this.data.Grade && this.data.SSActivityId != 306) {
            msg = "a";
        } if (!this.data.Major && this.data.SSActivityId != 306) {
            msg = "a";
        } if (!this.data.TblSpeechFieldIds) {
            msg = "a";
        } if (!this.data.CityId) {
            msg = "a";
        }

        if (msg) {
            this.service.openSnackBar("را به صورت کامل وارد نمایید", "اطلاعات پروفایل");
            return;
        }


        if (!this.data.EducationDescription && this.data.SSActivityId != 306) {
            msg += "[سوابق تحصیلی رسمی و غیر رسمی]";
            this.service.openSnackBar("را وارد نمایید", msg);
            return;
        }

        if (!this.data.TeachingDescription && this.data.SSActivityId == 301) {
            msg += "[سوابق و موضوعات تدریس]";
            this.service.openSnackBar("را وارد نمایید", msg);
            return;
        }

        if (!this.data.RecordsDescription && this.data.SSActivityId != 306) {
            msg += "[سوابق اجرایی]";
            this.service.openSnackBar("را وارد نمایید", msg);
            return;
        }

        //if (msg) {
        //  this.service.openSnackBar("اجباری می باشد", msg);
        //  return;
        //}

        this.service.setApiUrl('SpeakerApi');
        this.service.add(this.data)
            .subscribe(d => {

                this.onNoClick();
                this.service.openSnackBar("اطلاعات با موفقیت ذخیره شد.", 'پیام');

                //if (newRec.Id == 0) {
                //    //newRec.Id = d;
                //    //this.dataList.push(newRec);
                //    //this.dataSource.data.push(newRec);

                //    //this.dataSource._updateChangeSubscription();
                //}

            },
            e => {
                this.service.openSnackBar("اطلاعات را به درستی وارد نمایید", 'عملیات');

            }
            );
    }

    //onSelectFile(event) {

    //  if (event.target.files && event.target.files[0]) {
    //    var reader = new FileReader();

    //    reader.readAsDataURL(event.target.files[0]); // read file as data url

    //    reader.onload = (event) => { // called once readAsDataURL is completed
    //        this.data.ImageUrl = reader.result;
    //    }
    //  }
    //}

    deleteImage(id: number) {

        this.service.deleteFile(id)
            .subscribe(d => {
                // do something, if upload success
                //console.log(data);

                this.data.ImageUrl = '';
                this.data.ImageFileId = null;

            }, error => {
                console.log(error);
            });
    }

    deleteResume(id: number) {

        this.service.deleteFile(id)
            .subscribe(d => {
                // do something, if upload success
                //console.log(data);

                this.data.ResumeUrl = '';
                this.data.ResumeFileId = null;

            }, error => {
                console.log(error);
            });
    }

    //------------------
    fileToUpload: File = null;

    //handleFileInput(files: FileList) {

    //    this.fileToUpload = files.item(0);

    //  this.uploadFileToActivity();
    //}

    handleFileInput(input: HTMLInputElement, type: number) {

        this.fileToUpload = input.files[0];

        this.uploadFileToActivity(type);
    }

    uploadFileToActivity(type: number) {
        this.service.postFile(this.fileToUpload, type)
            .subscribe(d => {
                // do something, if upload success
                //console.log(data);
                if (type == 100) {
                    this.data.ImageUrl = d.FileUrl;
                    this.data.ImageFileId = d.Id;
                }
                else if (type == 101) {
                    this.data.ResumeUrl = d.FileUrl;
                    this.data.ResumeFileId = d.Id;
                }


            }, error => {
                console.log(error);
            });
    }

    backClicked() {
        this._location.back();
    }

    //afuConfig = {
    //  multiple: false,
    //  formatsAllowed: ".jpg,.png",
    //  maxSize: "1",
    //  uploadAPI: {
    //      url: this.apiUrl,
    //    headers: {
    //        //"Content-Type": "text/plain;charset=UTF-8",
    //        "Content-Type": "false",
    //        //"Content-Type": "multipart/form-data",
    //        //contentType: false,
    //        //processData: false,

    //      //"Authorization": `Bearer ${token}` 
    //    }
    //  },
    //  //theme: "attachPin",
    //  //theme: "dragNDrop",
    //  hideProgressBar: false,
    //  hideResetBtn: false,
    //  hideSelectBtn: false
    //};


}
