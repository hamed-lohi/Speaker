import { Component, Inject} from '@angular/core';
//import { ITheSpeaker } from '../the-speaker';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseComponent } from "src/app/shared/base.component";
import { OnInit } from "@angular/core";
import { WINDOW } from "src/app/shared/base.utility";
import { TheSpeakerService } from "src/app/index-page/the-speaker/the-speaker.service";

@Component({
  selector: 'dialog-speaker',
  templateUrl: 'dialog.component.html',
})
export class DialogComponent {

  constructor(
    public dialogRef: MatDialogRef<DialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private service: TheSpeakerService,
    @Inject(WINDOW) protected window: Window) {

      this.getSelectOptions();
    }

  apiUrl = this.window.location.origin + '/api/FileApi/UploadSingleFile';  // URL to web api

  onNoClick(): void {
    this.dialogRef.close();
  }

  selectListCity: any[];
  selectListSpeechFields: any[];

  getSelectOptions(): void {
      this.service.getSelectOptions("CityApi")
          .subscribe(d => this.selectListCity = d);

      this.service.getSelectOptions("ConstApi","pId=1")
        .subscribe(d => this.selectListSpeechFields = d);

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

    //------------------
  fileToUpload : File = null;

  handleFileInput(input: HTMLInputElement) {

      this.fileToUpload = input.files[0];

    this.uploadFileToActivity();
  }

  uploadFileToActivity() {
      this.service.postFile(this.fileToUpload, 100)
        .subscribe(d => {
      // do something, if upload success
        //console.log(data);
            this.data.ImageUrl = d.FileUrl;
            this.data.ImageFileId = d.Id;

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
