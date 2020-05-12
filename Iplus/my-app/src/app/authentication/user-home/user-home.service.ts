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
export class UserHomeService extends BaseService {
  //static nextCrisisId = 100;
  //private crises$: BehaviorSubject<ITheSpeaker[]> = new BehaviorSubject<ITheSpeaker[]>(CRISES);

  constructor(protected  messageService: MessageService,
      protected  http: HttpClient,
      protected snackBar: MatSnackBar,
    @Inject(WINDOW) protected window: Window
  ) {
      super(messageService, http, snackBar, window);

      this.setApiUrl('Account'); // URL to web api
    }

  changePassword(record: any) {
    return this.postData(record, 'ChangePassword');
  }

  resetPassword(record: any) {
      return this.postData(record, 'ResetPasswordByAdmin');
  }
  ResetPasswordByAdmin

  getMyProfile() {
      return this.getData('GetMyProfile');
  }

  changeMyProfile(record: any) {
      return this.postData(record, 'ChangeMyProfile');
  }

}
