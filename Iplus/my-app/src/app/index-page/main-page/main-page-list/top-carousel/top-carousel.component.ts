import { Component, HostBinding, OnInit, ViewChild, ChangeDetectorRef, ChangeDetectionStrategy, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { MainPageService } from "src/app/index-page/main-page/main-page.service";

import { NguCarousel, NguCarouselStore, NguCarouselConfig } from '@ngu/carousel';
//import { slider } from "src/app/index-page/main-page/main-page-list/top-carousel/slide.animation";

@Component({
  selector: 'app-top-carousel',
  templateUrl: './top-carousel.component.html',
  styleUrls: ['./top-carousel.component.css'],
  //animations: [slider],
  //changeDetection: ChangeDetectionStrategy.OnPush,

})
export class TopCarouselComponent implements OnInit, AfterViewInit {

  //name = 'Angular';
  //slideNo = 0;
  //withAnim = true;
  //resetAnim = true;

  //@ViewChild('myCarousel') myCarousel: NguCarousel<any>;
  //public carouselConfig: NguCarouselConfig = {
  //    grid: { xs: 1, sm: 1, md: 1, lg: 1, all: 0 },
  //    load: 3,
  //    //interval: { timing: 4000, initialDelay: 1000 },
  //    loop: true,
  //    touch: true,
  //    velocity: 0.2,
  //    RTL: true,
  //}

  public carouselTileConfig: NguCarouselConfig =
    {
      grid: { xs: 1, sm: 2, md: 3, lg: 4, all: 0 },
      //load: 1,
      slide: 1,
      speed: 500,
      point: {
        visible: true
      },
      touch: true,
      //easing: 'cubic-bezier(0, 0, 0.2, 1)',
      //easing: 'easing',
      loop: true,
      interval: { timing: 3000 },
      animation: 'lazy',
      RTL: true,
      //custom: 'banner',
      //velocity: 0.2,
    };

  public carouselTileConfig1: NguCarouselConfig =
    {
      grid: { xs: 1, sm: 2, md: 3, lg: 4, all: 0 },
      speed: 500,
      point: {
        visible: true
      },
      touch: true,
      loop: true,
      RTL: true,
    };

  public carouselTileConfig2: NguCarouselConfig =
    {
      grid: { xs: 1, sm: 2, md: 3, lg: 4, all: 0 },
      speed: 500,
      point: {
        visible: true
      },
      touch: true,
      loop: true,
      RTL: true,
    };

  public carouselTileConfig3: NguCarouselConfig =
    {
      grid: { xs: 1, sm: 2, md: 3, lg: 4, all: 0 },
      speed: 500,
      point: {
        visible: true
      },
      touch: true,
      loop: true,
      RTL: true,
    };

  public carouselTileConfig4: NguCarouselConfig =
    {
      grid: { xs: 1, sm: 2, md: 3, lg: 4, all: 0 },
      speed: 500,
      point: {
        visible: true
      },
      touch: true,
      loop: true,
      RTL: true,
    };

  public carouselTileConfig5: NguCarouselConfig =
    {
      grid: { xs: 1, sm: 2, md: 3, lg: 4, all: 0 },
      speed: 500,
      point: {
        visible: true
      },
      touch: true,
      loop: true,
      RTL: true,
    };

  //public carouselTileConfigPost: NguCarouselConfig =
  //  {
  //    grid: { xs: 1, sm: 2, md: 3, lg: 4, all: 0 },
  //    speed: 500,
  //    point: {
  //      visible: true
  //    },
  //    touch: true,
  //    loop: true,
  //    RTL: true,
  //  };

  //public carouselTileConfiga: NguCarouselConfig = {
  //  grid: { xs: 1, sm: 2, md: 3, lg: 4, all: 0 },
  //  speed: 500,
  //  point: {
  //    visible: true
  //  },
  //  touch: true,
  //  loop: true,
  //  interval: { timing: 3000 },
  //  //animation: 'lazy',
  //  RTL: true,
  //};

  postList: any = [];
  topSpeakersList: any = [];
  topSpeakersList1: any = [];
  topSpeakersList2: any = [];
  topSpeakersList3: any = [];
  topSpeakersList4: any = [];
  topSpeakersList5: any = [];


  constructor(private router: Router,
    private service: MainPageService,
    private cdr: ChangeDetectorRef) {

    this.getPosts();
  }

  getPosts() {

    this.service.setApiUrl('PostApi');
    this.service.getData('GetPostsForIndexPage', 'SSPostType=201')
      .subscribe(a => this.postList = a);
  }

  ngAfterViewInit() {
    this.cdr.detectChanges();
  }

  //reset() {
  //    this.myCarousel.reset(!this.resetAnim);
  //}

  //moveTo(slide) {
  //    this.myCarousel.moveTo(slide, !this.withAnim);
  //}

  ngOnInit() {

    //this.cdr.detectChanges();
    this.getSpeakers();
  }


  getSpeakers() {

    this.service.setApiUrl('SpeakerApi');
    this.service.getData('GetTopSpeakers')
      .subscribe(a => {
        this.topSpeakersList = a.filter(a => a.SSActivityId == 301);
        this.topSpeakersList1 = a.filter(a => a.SSActivityId == 302);
        this.topSpeakersList2 = a.filter(a => a.SSActivityId == 303);
        this.topSpeakersList3 = a.filter(a => a.SSActivityId == 304);
        this.topSpeakersList4 = a.filter(a => a.SSActivityId == 305);
        this.topSpeakersList5 = a.filter(a => a.SSActivityId == 306);
      });
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
