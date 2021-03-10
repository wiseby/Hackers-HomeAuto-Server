import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ConfLayoutComponent } from "./components/layout/conf-layout.component";
import { NodeDetailsComponent } from "./components/node-details/node-details.component";

// Register routes and there behaviours:
const routes: Routes = [
  { path: 'configurations', component: ConfLayoutComponent },
  { path: 'configurations/:guid', component: NodeDetailsComponent },
];

@NgModule({
  // Declare Components and Directives
  declarations: [ ],
  // Modules imported and consumed by app:
  imports: [ RouterModule.forChild(routes) ],
  // Dependencies to inject:
  providers: [],
})
export class ConfigurationRoutingModule {}