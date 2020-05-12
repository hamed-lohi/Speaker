import { Component, Inject } from '@angular/core';
//import { ITheSpeaker } from '../the-speaker';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseComponent } from "src/app/shared/base.component";
import { OnInit, ViewChild } from "@angular/core";
import { WINDOW } from "src/app/shared/base.utility";
import { TheSpeakerService } from "src/app/admin-panel/the-speaker/the-speaker.service";

@Component({
    selector: 'dialog-speaker',
    templateUrl: 'dialog.component.html',
    styleUrls: ['dialog.component.css']
})
export class DialogComponent implements OnInit {

    hide = true;
    name = 'ng2-ckeditor';
    ckeConfig: any;
    mycontent: string;
    log: string = '';
    viewType: number;

    //@ViewChild("myckeditor") ckeditor: any;

    constructor(
        public dialogRef: MatDialogRef<DialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any,
        private service: TheSpeakerService,
        @Inject(WINDOW) protected window: Window) {

        //this.getSelectOptions();
        this.viewType = data.viewType;

    }

    ngOnInit() {

        if (this.data.dialogType == 1) {

            this.ckeConfig = {
                allowedContent: false,
                //extraPlugins: 'divarea',
                forcePasteAsPlainText: true
            };

            this.getRecordInfo();
        }

    }

    onChange($event: any): void {
        //console.log("onChange");
        //this.log += new Date() + "<br />";
    }

    apiUrl = this.window.location.origin + '/api/FileApi/UploadSingleFile';  // URL to web api

    onNoClick(): void {
        this.dialogRef.close();
    }

    selectListCity: any[];
    selectListSpeechFields: any[];

    //getSelectOptions(): void {
    //    this.service.getSelectOptions("CityApi")
    //        .subscribe(d => this.selectListCity = d);

    //    this.service.getSelectOptions("ConstApi", "pId=1")
    //        .subscribe(d => this.selectListSpeechFields = d);

    //}

    getRecordInfo() {

        if (this.data.Id != 0) {

            //this.data.EducationDescription = "";

            this.service.getForEdit(this.data.Id)
                .subscribe(a => this.data = a);
        }
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
