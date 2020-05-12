import { NgModule, Injectable } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { MainPageListComponent } from './main-page-list/main-page-list.component';
//import { MainPageComponent } from './main-page/main-page.component';
import { MainPageRoutingModule } from './main-page-routing.module';
import { MaterialsModule } from "../../shared/materials.module";
import { DialogComponent } from "./main-page-list/dialog/dialog.component";
import { CKEditorModule } from 'ng2-ckeditor';
import { PostsComponent } from "src/app/index-page/main-page/main-page-list/posts/posts.component";
import { SliderComponent } from "src/app/index-page/main-page/main-page-list/slider/slider.component";
import { TopSpeakersComponent } from "src/app/index-page/main-page/main-page-list/top-speakers/top-speakers.component";
import { NguCarouselModule } from '@ngu/carousel';
import { TopCarouselComponent } from "src/app/index-page/main-page/main-page-list/top-carousel/top-carousel.component";

import { HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { HammerConfig } from "src/app/index-page/main-page/HammerConfig";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MainPageRoutingModule,
        MaterialsModule,
        CKEditorModule,
        NguCarouselModule,

    ],
    entryComponents: [MainPageListComponent, DialogComponent],
    declarations: [
        //MainPageComponent,
        MainPageListComponent,
        DialogComponent,
        PostsComponent,
        SliderComponent,
        TopSpeakersComponent,
        TopCarouselComponent,
    ],

    providers: [
      {
        provide: HAMMER_GESTURE_CONFIG,
        useClass: HammerConfig
      }
    ],

})
export class MainPageModule { }
