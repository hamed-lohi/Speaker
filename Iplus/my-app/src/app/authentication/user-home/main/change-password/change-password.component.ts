import { Component, OnInit, HostBinding } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { DialogService } from "src/app/general/dialog.service";
import { switchMap } from "rxjs/operators";
import { Location } from '@angular/common';
import { AuthenticationService } from "src/app/authentication/_services";
import { UserHomeService } from "src/app/authentication/user-home/user-home.service";

@Component({
    selector: 'app-change-password',
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.css']
})

export class ChangePasswordComponent implements OnInit {

    hide = true;
    accountInfo = {
        //FullName: "",
        //Email: "",
        OldPassword: "",
        NewPassword: "",
        ConfirmPassword: "",
    };

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: UserHomeService,
        private _location: Location,
        private authenticationService: AuthenticationService
    ) { }

    ngOnInit() {
        this.route.paramMap.subscribe(params => {
            //this.getRecordInfo();
        });

    }

    changePassword() {
        this.service.changePassword(this.accountInfo)
            .subscribe(a => {
                this.service.openSnackBar("با موفقیت انجام شد.", 'عملیات');
                this.accountInfo = null;
                this.logout();
            });
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }

    //getRecordInfo() {

    //    this.service.getForEdit(this.speakerId)
    //        .subscribe(a => this.speakerInfo = a);
    //}

    //backClicked() {
    //    this._location.back();
    //}

}
