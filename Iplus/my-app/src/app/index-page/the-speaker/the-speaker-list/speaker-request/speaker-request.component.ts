import { Component, OnInit, HostBinding } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { DialogService } from "src/app/general/dialog.service";
import { switchMap } from "rxjs/operators";
import { Location } from '@angular/common';
import { SpeakerRequestService } from "src/app/index-page/the-speaker/the-speaker-list/speaker-request/speaker-request.service";
import { AuthenticationService } from "src/app/authentication/_services";

@Component({
    selector: 'speaker-request',
    templateUrl: './speaker-request.component.html',
    styleUrls: ['./speaker-request.component.css']
})

export class SpeakerRequestComponent implements OnInit {

    speakerInfo: any = {};
    //speakerId: number;
    selectListCity: any[];
    selectListSpeechFields: any[];
    selectListSpeakers: any[];
    selectListActivityTypes: any[];

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: SpeakerRequestService,
        private _location: Location,

    ) { }

    ngOnInit() {
        this.route.paramMap.subscribe(params => {
            this.speakerInfo.SpeakerId = +params.get("id");
            //this.speakerId = +params.get("id");
            this.getSelectOptions();
            this.getDefaultInfo();
        });

    }

    getSelectOptions(): void {

        this.service.getSelectOptions("CityApi")
            .subscribe(d => this.selectListCity = d);

      this.service.getSelectOptions("ConstApi", "pId=300")
        .subscribe(d => this.selectListActivityTypes = d);

        //this.service.getSelectOptions("ConstApi", "pId=1")
        //    .subscribe(d => this.selectListSpeechFields = d);

        //this.service.getSelectOptions("SpeakerApi", "speechFieldId=")
        //    .subscribe(d => this.selectListSpeakers = d);
    }

    fillDependSelect() {

        switch (this.speakerInfo.SSActivityId) {

            case 301: // استاد و سخنران
                this.service.getSelectOptions("ConstApi", "pId=1")
                    .subscribe(d => this.selectListSpeechFields = d);
                break;

            case 302: // نیروی متخصص
                this.service.getSelectOptions("ConstApi", "pId=350")
                    .subscribe(d => this.selectListSpeechFields = d);
                break;

            case 303: // راوی دفاع مقدس
                this.service.getSelectOptions("ConstApi", "pId=400")
                    .subscribe(d => this.selectListSpeechFields = d);
                break;

            case 304: // قاری قرآن
                this.service.getSelectOptions("ConstApi", "pId=450")
                    .subscribe(d => this.selectListSpeechFields = d);
                break;

            case 305: // مداح و ستایشگر
                this.service.getSelectOptions("ConstApi", "pId=500")
                    .subscribe(d => this.selectListSpeechFields = d);
                break;

            case 306: // گروه سرود و تواشیح
                this.service.getSelectOptions("ConstApi", "pId=550")
                    .subscribe(d => this.selectListSpeechFields = d);
                break;

        }
    }

    onChange(newValue) {

        this.service.getSelectOptions("SpeakerApi", "speechFieldId=" + this.speakerInfo.SSSubject)
            .subscribe(d => this.selectListSpeakers = d);
    }

    add(): void {
        if (!this.speakerInfo) { return; }

        this.service.add(this.speakerInfo)
            .subscribe(d => {

                this.service.openSnackBar("با موفقیت انجام شد.", 'عملیات');
                this.backClicked();

            });
    }

    getDefaultInfo() {

        this.service.getDefaultInfo()
            .subscribe(a => {
                var speakerId = this.speakerInfo.SpeakerId;
                this.speakerInfo = a;
                this.speakerInfo.SpeakerId = speakerId;
            },
            b => { }
            );
    }

    backClicked() {

        this._location.back();
    }

}
