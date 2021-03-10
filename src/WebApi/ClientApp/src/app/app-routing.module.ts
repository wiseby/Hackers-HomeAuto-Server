import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeLayoutComponent } from './modules/common/main/layout/home-layout.component';
import { NotFoundComponent } from './modules/common/notfound/notfound.component';

const routes: Routes = [
  { path: '', component: HomeLayoutComponent },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
