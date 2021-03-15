import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LayoutComponent } from "./components/layout/layout.component";
import { RoomComponent } from "./components/room/room.component";

const routes: Routes = [
  { path: 'roomsight', component: LayoutComponent, pathMatch: 'full' },
  { path: 'roomsight/:room', component: RoomComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [
    RouterModule
  ]
})
export class RoomsightRoutingModule {}