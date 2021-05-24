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
    path: 'nodes/:clientId',
    pathMatch: 'full',
    loadChildren: () =>
      import('@node-details/node-details.module').then(
        (m) => m.NodeDetailsModule,
      ),
  },
  {
    path: 'nodes/:clientId/configure',
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
