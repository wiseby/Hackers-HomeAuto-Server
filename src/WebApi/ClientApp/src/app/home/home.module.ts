import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ContainerComponent } from './container/container.component';
import { SharedModule } from '@shared/shared.module';

const routes: Routes = [{ path: '', component: ContainerComponent }];

@NgModule({
  declarations: [ContainerComponent],
  imports: [
    RouterModule.forChild(routes),
    FontAwesomeModule,
    CommonModule,
    SharedModule,
  ],
  providers: [],
})
export class HomeModule {}
