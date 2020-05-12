import { Component, HostBinding } from '@angular/core';
import { Router } from '@angular/router';
import { MainPageService } from "src/app/index-page/main-page/main-page.service";
import { FormGroup } from "@angular/forms";
import { interval, Subscription } from 'rxjs';

@Component({
    selector: 'app-slider',
    templateUrl: './slider.component.html',
    styleUrls: ['./slider.component.css']
})
export class SliderComponent {

    sliderList: any[];
    selectedIndex = 0;
    subscription: Subscription;
    //intervalId: number;

    constructor(private router: Router,
        private service: MainPageService,
        //private _formBuilder: FormBuilder,
    ) {

        this.getSliders();

    }



    getSliders() {

        this.service.setApiUrl('PostApi');
        this.service.getData('GetPostsForIndexPage', 'SSPostType=202')
            .subscribe(a => this.sliderList = a);
    }


  /**
 * IMPORTANT NODE
 * ONLY USE EITHER METHOD 1
 * OR METHOD 2
 * DON'T USE BOTH
 */

  ngOnInit() {

    // This is METHOD 1
    const source = interval(8000);
    this.subscription = source.subscribe(val => this.changeSlide());

    // This is METHOD 2
    //this.intervalId = setInterval(this.changeSlide(), 7000);

  }


  changeSlide() {

    if ((this.sliderList.length - 1) == this.selectedIndex) {
      this.selectedIndex = 0;
    } else {
      this.selectedIndex += 1; 
      }
  }

  ngOnDestroy() {
    // For method 1
    this.subscription && this.subscription.unsubscribe();

    // For method 2
    //clearInterval(this.intervalId);
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
