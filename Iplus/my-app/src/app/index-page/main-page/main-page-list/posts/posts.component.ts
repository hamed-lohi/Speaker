import { Component, HostBinding, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MainPageService } from "src/app/index-page/main-page/main-page.service";

@Component({
    selector: 'app-posts',
    templateUrl: './posts.component.html',
    styleUrls: ['./posts.component.css']
})
export class PostsComponent {

    postList: any;

    constructor(private router: Router,
        private service: MainPageService, ) {
        
        this.getPosts();
    }


    getPosts() {

      this.service.setApiUrl('PostApi');
      this.service.getData('GetPostsForIndexPage', 'SSPostType=201')
            .subscribe(a => this.postList = a);
    }

  showDetail(postId: number) {
    //this.service.selectedSpeakerId = speakerId;
    this.router.navigate(['/index-page/post/detail', postId]);
    //this.router.navigate(['/superheroes', { id: heroId, foo: 'foo' }]);
  }

    //send() {
    //    this.sending = true;
    //    this.details = 'Sending Message...';

    //    setTimeout(() => {
    //        this.sending = false;
    //        this.closePopup();
    //    }, 1000);
    //}

    cancel() {
        this.closePopup();
    }

    closePopup() {
        // Providing a `null` value to the named outlet
        // clears the contents of the named outlet
        this.router.navigate([{ outlets: { popup: null } }]);
    }
}
