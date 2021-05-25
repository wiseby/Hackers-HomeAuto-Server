import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ContainerComponent } from './container/container.component';
import { NodeDetailsComponent } from './components/node-details/node-details.component';

const routes: Routes = [
  {
    path: 'configurations',
    component: ContainerComponent,
    children: [
      {
        path: ':guid',
        component: NodeDetailsComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ConfigurationRoutingModule {}
