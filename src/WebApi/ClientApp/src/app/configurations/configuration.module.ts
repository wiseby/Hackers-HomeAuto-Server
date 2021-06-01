import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfigurationRoutingModule } from './configuration-routing.module';
import { ContainerComponent } from './container/container.component';
import { SharedModule } from '@shared/shared.module';
import { NodeConfigComponent } from './components/node-config/node-config.component';
import { NodeConfigSelectionComponent } from './components/node-config-selection/node-config-selection.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DefinitionsFormComponent } from './components/definitions-form/definitions-form.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [
    ContainerComponent,
    NodeConfigComponent,
    NodeConfigSelectionComponent,
    DefinitionsFormComponent,
  ],
  imports: [
    ConfigurationRoutingModule,
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule,
  ],
  providers: [],
})
export class ConfigurationModule {}
