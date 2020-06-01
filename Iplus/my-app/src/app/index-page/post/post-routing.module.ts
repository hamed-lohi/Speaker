import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostListComponent } from "src/app/index-page/post/post-list/post-list.component";
import { PostDetailComponent } from "src/app/index-page/post/post-list/post-detail/post-detail.component";


//import { CanDeactivateGuard } from '../can-deactivate.guard';

const postRoutes: Routes = [

  {
    path: ':id',
    component: PostListComponent,
    },
  {
      path: 'detail/:id',
      component: PostDetailComponent,
      //data: { animation: 'detail' }
  },
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
