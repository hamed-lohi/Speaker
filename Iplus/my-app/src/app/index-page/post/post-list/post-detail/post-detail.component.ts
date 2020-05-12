import { Component, OnInit, HostBinding } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { DialogService } from "src/app/general/dialog.service";
import { switchMap } from "rxjs/operators";
import { Location } from '@angular/common';
import { PostService } from "src/app/index-page/post/post.service";

@Component({
    selector: 'app-post-detail',
    templateUrl: './post-detail.component.html',
    styleUrls: ['./post-detail.component.css']
})

export class PostDetailComponent implements OnInit {

    postInfo: any = {};
    postId: number;


    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private service: PostService,
        private _location: Location
    ) { }

    ngOnInit() {
        this.route.paramMap.subscribe(params => {
            this.postId = +params.get("id");
            this.getRecordInfo();
        });

    }

    getRecordInfo() {

        this.service.getById(this.postId)
            .subscribe(a => this.postInfo = a);
    }

    backClicked() {
        this._location.back();
    }

    //showRequestForm() {
    //  //this.service.selectedSpeakerId = speakerId;
    //    this.router.navigate(['/index-page/the-speaker/request', this.postId]);
    //  //this.router.navigate(['/superheroes', { id: heroId, foo: 'foo' }]);
    //}

}
