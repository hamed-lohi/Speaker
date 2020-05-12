import { Component, Inject } from '@angular/core';
//import { ITheSpeaker } from '../the-speaker';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseComponent } from "src/app/shared/base.component";
import { OnInit } from "@angular/core";
import { WINDOW } from "src/app/shared/base.utility";
import { SpeakerRequestService } from "src/app/admin-panel/speaker-request/speaker-request.service";

@Component({
    selector: 'dialog-speaker',
    templateUrl: 'dialog.component.html',
    styleUrls: ['./dialog.component.css']
})
export class DialogComponent implements OnInit {

    selectListCity: any[];
    selectListSpeechFields: any[];
    selectListSpeakers: any[];
    selectListActivityTypes: any[];

    constructor(
        public dialogRef: MatDialogRef<DialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any,
        private service: SpeakerRequestService,
        @Inject(WINDOW) protected window: Window) {


    }

    ngOnInit() {

        this.getRecordInfo();
    }

    apiUrl = this.window.location.origin + '/api/FileApi/UploadSingleFile';  // URL to web api

    getRecordInfo() {

        if (this.data.Id != 0) {

            this.service.getForEdit(this.data.Id)
                .subscribe(a => {
                    this.data = a;
                    this.getSelectOptions();
                });

        } else {
            this.getSelectOptions();
        }
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

    getSelectOptions(): void {

        this.service.getSelectOptions("CityApi")
            .subscribe(d => this.selectListCity = d);

        this.service.getSelectOptions("ConstApi", "pId=300")
            .subscribe(d => {
                this.selectListActivityTypes = d;
                //this.data.SSActivityId = this.data.SSActivityId;
                this.fillDependSelect();
            });

        //this.service.getSelectOptions("ConstApi", "pId=1")
        //  .subscribe(d => this.selectListSpeechFields = d);

        //this.service.getSelectOptions("SpeakerApi", "speechFieldId=")
        //  .subscribe(d => this.selectListSpeakers = d);
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

      this.service.getSelectOptions("ConstApi", "pId="+pid)
        .subscribe(d => {
            this.selectListSpeechFields = d;
          this.onChange();
        });

    }

    onChange() {

        this.service.getSelectOptions("SpeakerApi", "speechFieldId=" + this.data.SSSubject)
            .subscribe(d => this.selectListSpeakers = d);
    }

}
