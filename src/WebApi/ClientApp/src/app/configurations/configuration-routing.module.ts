import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ContainerComponent } from '@configurations/container/container.component';
import { NodeDetailsComponent } from './components/node-details/node-details.component';

const routes: Routes = [
  { path: 'configurations', component: ContainerComponent, pathMatch: 'full' },
  {
    path: 'configurations/:guid',
    component: NodeDetailsComponent,
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ConfigurationRoutingModule {}
