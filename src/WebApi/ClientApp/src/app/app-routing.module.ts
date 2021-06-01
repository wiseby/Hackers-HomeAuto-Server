import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './shared/notfound/notfound.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('@home/home.module').then((m) => m.HomeModule),
  },
  {
    path: 'nodes',
    pathMatch: 'full',
    loadChildren: () => import('@home/home.module').then((m) => m.HomeModule),
  },
  {
    path: 'nodes/configurations',
    pathMatch: 'full',
    loadChildren: () =>
      import('@configurations/configuration.module').then(
        (m) => m.ConfigurationModule,
      ),
  },
  {
    path: 'roomsight',
    pathMatch: 'full',
    loadChildren: () =>
      import('@roomsight/roomsight.module').then((m) => m.RoomsightModule),
  },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
