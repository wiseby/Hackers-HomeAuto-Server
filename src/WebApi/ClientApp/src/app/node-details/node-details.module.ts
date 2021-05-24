import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ContainerComponent } from './container/container.component';
import { SharedModule } from '@shared/shared.module';
import { ChartsModule } from 'ng2-charts';
import { NodeStatisticsComponent } from './components/node-statistics/node-statistics.component';
import { GeneralInfoComponent } from './components/general-info/general-info.component';

const routes: Routes = [{ path: '', component: ContainerComponent }];

@NgModule({
  declarations: [
    ContainerComponent,
    NodeStatisticsComponent,
    GeneralInfoComponent,
  ],
  imports: [
    RouterModule.forChild(routes),
    FontAwesomeModule,
    CommonModule,
    SharedModule,
    ChartsModule,
  ],
  providers: [],
})
export class NodeDetailsModule {}
