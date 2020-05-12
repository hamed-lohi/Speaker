import { Component, HostBinding, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MainPageService } from "src/app/index-page/main-page/main-page.service";

@Component({
    selector: 'app-top-speakers',
    templateUrl: './top-speakers.component.html',
    styleUrls: ['./top-speakers.component.css'] 
})
export class TopSpeakersComponent {

    topSpeakersList: any;

    constructor(private router: Router,
        private service: MainPageService, ) {
        
        this.getSpeakers();
    }


    getSpeakers() {

      this.service.setApiUrl('SpeakerApi');
      this.service.getData('GetTopSpeakers')
          .subscribe(a => this.topSpeakersList = a);
    }

  showDetail(speakerId: number) {
    //this.service.selectedSpeakerId = speakerId;
      this.router.navigate(['/index-page/the-speaker/detail', speakerId]);
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
