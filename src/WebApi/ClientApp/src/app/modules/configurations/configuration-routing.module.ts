import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ConfLayoutComponent } from "./components/layout/conf-layout.component";
import { NodeDetailsComponent } from "./components/node-details/node-details.component";

// Register routes and there behaviours:
const routes: Routes = [
  { path: 'configurations', component: ConfLayoutComponent, pathMatch: 'full' },
  { path: 'configurations/:guid', component: NodeDetailsComponent, pathMatch: 'full' },
];

@NgModule({
  // Declare Components and Directives
  declarations: [ ],
  // Modules imported and consumed by app:
  imports: [ RouterModule.forChild(routes) ],
  exports: [
    RouterModule
  ],
})
export class ConfigurationRoutingModule {}