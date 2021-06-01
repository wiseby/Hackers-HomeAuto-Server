import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NodeConfigSelectionComponent } from './components/node-config-selection/node-config-selection.component';
import { NodeConfigComponent } from './components/node-config/node-config.component';
import { ContainerComponent } from './container/container.component';

const routes: Routes = [
  {
    path: 'nodes/configurations',
    component: ContainerComponent,
    children: [
      {
        path: '',
        component: NodeConfigSelectionComponent,
      },
      {
        path: ':clientId',
        component: NodeConfigComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ConfigurationRoutingModule {}
