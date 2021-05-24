import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfigurationRoutingModule } from './configuration-routing.module';
import { ContainerComponent } from './container/container.component';
import { SharedModule } from '@shared/shared.module';

@NgModule({
  declarations: [ContainerComponent],
  imports: [ConfigurationRoutingModule, CommonModule, SharedModule],
  providers: [],
})
export class ConfigurationModule {}
