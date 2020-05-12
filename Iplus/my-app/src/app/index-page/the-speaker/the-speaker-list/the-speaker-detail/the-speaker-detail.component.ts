import { Component, OnInit, HostBinding } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { DialogService } from "src/app/general/dialog.service";
import { TheSpeakerService } from "src/app/index-page/the-speaker/the-speaker.service";
import { switchMap } from "rxjs/operators";
import { Location } from '@angular/common';

@Component({
    selector: 'the-speaker-detail',
    templateUrl: './the-speaker-detail.component.html',
    styleUrls: ['./the-speaker-detail.component.css']
})

export class TheSpeakerDetailComponent implements OnInit {

    speakerInfo: any = {};
    speakerId: number;


    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: TheSpeakerService,
        private _location: Location
    ) { }

    ngOnInit() {
        this.route.paramMap.subscribe(params => {
            this.speakerId = +params.get("id");
            this.getRecordInfo();
        });

    }

    getRecordInfo() {

        this.service.getForEdit(this.speakerId)
            .subscribe(a => this.speakerInfo = a);
    }

    backClicked() {
        this._location.back();
    }

    showRequestForm() {
        //this.service.selectedSpeakerId = speakerId;
        this.router.navigate(['/index-page/the-speaker/request', this.speakerId]);
        //this.router.navigate(['/superheroes', { id: heroId, foo: 'foo' }]);
    }

}
