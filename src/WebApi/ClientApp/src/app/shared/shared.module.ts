import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NodeCardComponent } from './components/node-card/node-card.component';

@NgModule({
  declarations: [NodeCardComponent],
  imports: [CommonModule, FontAwesomeModule, RouterModule],
  exports: [NodeCardComponent],
  providers: [],
})
export class SharedModule {}
