import { BehaviorSubject, Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Injectable } from '@angular/core';
//import { ITheSpeaker } from './the-speaker-list/the-speaker';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject } from "@angular/core";
import { WINDOW } from "src/app/shared/base.utility";
import { BaseService } from "src/app/shared/base.service";
import { MessageService } from "src/app/general/message.service";

@Injectable({
    providedIn: 'root',
})
export class SpeakerRequestService extends BaseService {
    //static nextCrisisId = 100;
    //private crises$: BehaviorSubject<ITheSpeaker[]> = new BehaviorSubject<ITheSpeaker[]>(CRISES);

    selectedSpeakerId: number;

    constructor(protected messageService: MessageService,
        protected http: HttpClient,
        protected snackBar: MatSnackBar,
        @Inject(WINDOW) protected window: Window
    ) {
        super(messageService, http, snackBar, window);

        //this.apiUrl += '/api/SpeakerRequestApi/';  // URL to web api
        this.setApiUrl('SpeakerRequestApi');

    }

    getDefaultInfo() {
        return this.getData('GetDefaultInfo');
    }

}
