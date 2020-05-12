import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from "../_services/authentication.service";

@Component({
    templateUrl: 'login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';
    message = '';

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService
    ) {

        if (this.authenticationService.currentUserValue) {
            this.router.navigate(['/']);
        }

    }

    accountInfo = {
        Username: "",
        Password: "",
    };

    hide = true;

    ngOnInit() {
        //this.loginForm = this.formBuilder.group({
        //    username: ['', Validators.required],
        //    password: ['', Validators.required]
        //});

        // reset login status
        //this.authenticationService.logout();

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

        var isRequest = this.returnUrl.includes('request');

        if (isRequest) {
            this.message =
                ' برای ثبت درخواست سخنران ابتدا باید وارد سایت شوید، و در صورت عدم عضویت، دکمه عضویت را کلیک نمایید ';
        }
        else {
            this.route.paramMap.subscribe(params => {
                this.message = params.get("msg");
            });
        }

        //if (! this.message) {
        //  var parameters = new URLSearchParams(this.returnUrl);
        //  this.message = parameters.get("msg");
        //}

    }

    // convenience getter for easy access to form fields
    //get f() { return this.loginForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        //if (this.loginForm.invalid) {
        //    return;
        //}

        this.loading = true;
        this.authenticationService.login(this.accountInfo)
            .pipe(first())
            .subscribe(
            data => {
                //if (this.returnUrl.startsWith(route) && (data.Role == "AppAdmin" || data.Role == "Admin")) {
                //  this.router.navigate([this.returnUrl]);
                //}
                //alert("اطلاعات شما صحیح نمی باشد");
                this.router.navigate([this.returnUrl]);

            },
            error => {
                this.error = error;
                this.loading = false;
                alert("اطلاعات شما صحیح نمی باشد");
            });
    }
}
