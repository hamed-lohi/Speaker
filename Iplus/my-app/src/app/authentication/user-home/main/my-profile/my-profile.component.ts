import { Component, OnInit, HostBinding } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { DialogService } from "src/app/general/dialog.service";
import { switchMap } from "rxjs/operators";
import { Location } from '@angular/common';
import { UserHomeService } from "src/app/authentication/user-home/user-home.service";

@Component({
    selector: 'app-my-profile',
    templateUrl: './my-profile.component.html',
    styleUrls: ['./my-profile.component.css']
})

export class MyProfileComponent implements OnInit {

    accountInfo: any = {};
    fileToUpload: File = null;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: UserHomeService,
        private _location: Location
    ) { }

    ngOnInit() {
        //this.route.paramMap.subscribe(params => {
        //    this.speakerId = +params.get("id");
        //    this.getMyProfile();
        //}); 
      this.getSelectOptions();
      this.getMyProfile();
    }

  selectListCity: any[];

  getSelectOptions(): void {
    this.service.getSelectOptions("CityApi")
      .subscribe(d => this.selectListCity = d);

  }

    getMyProfile() {

        this.service.getMyProfile()
            .subscribe(a => this.accountInfo = a);
    }

  save(): void {
        if (!this.accountInfo) { return; }

        this.service.changeMyProfile(this.accountInfo)
            .subscribe(d => {

                this.service.openSnackBar("با موفقیت انجام شد.",'');
                //this.backClicked();

            });
    }

    backClicked() {
        this._location.back();
    }

    showRequestForm() {
        //this.service.selectedSpeakerId = speakerId;
        //this.router.navigate(['/index-page/the-speaker/request', this.speakerId]);
        //this.router.navigate(['/superheroes', { id: heroId, foo: 'foo' }]);
    }

    //handleFileInput(input: HTMLInputElement, type: number) {

    //    this.fileToUpload = input.files[0];

    //    this.uploadFileToActivity(type);
    //}

    //uploadFileToActivity(type: number) {
    //    this.service.postFile(this.fileToUpload, type)
    //        .subscribe(d => {
    //            // do something, if upload success
    //            //console.log(data);
    //            if (type == 110) {
    //                this.accountInfo.ImageUrl = d.FileUrl;
    //                this.accountInfo.ImageFileId = d.Id;
    //            }
    //            //else if (type == 101) {
    //            //    this.data.ResumeUrl = d.FileUrl;
    //            //    this.data.ResumeFileId = d.Id;
    //            //}


    //        }, error => {
    //            console.log(error);
    //        });
    //}

    //deleteImage(id: number) {

    //    this.service.deleteFile(id)
    //        .subscribe(d => {
    //            // do something, if upload success
    //            //console.log(data);

    //            this.accountInfo.ImageUrl = '';
    //            this.accountInfo.ImageFileId = null;

    //        }, error => {
    //            console.log(error);
    //        });
    //}

}
