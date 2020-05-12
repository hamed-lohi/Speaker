import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostListComponent } from './post-list/post-list.component';

//import { CanDeactivateGuard } from '../can-deactivate.guard';

const postRoutes: Routes = [
  {
    path: '',
    component: PostListComponent,
  }
];


@NgModule({
  imports: [
      RouterModule.forChild(postRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class PostRoutingModule { }
