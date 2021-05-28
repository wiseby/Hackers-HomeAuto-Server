import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NodeCardComponent } from './components/node-card/node-card.component';
import { NodeStatisticsComponent } from './components/node-statistics/node-statistics.component';

@NgModule({
  declarations: [NodeCardComponent, NodeStatisticsComponent],
  imports: [CommonModule, FontAwesomeModule, RouterModule],
  exports: [NodeCardComponent, NodeStatisticsComponent],
  providers: [],
})
export class SharedModule {}
