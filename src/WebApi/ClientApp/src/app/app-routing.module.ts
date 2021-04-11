import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContainerComponent } from '@roomsight/container/container.component';
import { NotFoundComponent } from './shared/notfound/notfound.component';

const routes: Routes = [
  { path: '', component: ContainerComponent },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
