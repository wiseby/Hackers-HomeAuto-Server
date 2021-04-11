import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ContainerComponent } from "@roomsight/container/container.component";
import { RoomComponent } from "@roomsight//components/room/room.component";

const routes: Routes = [
  { path: 'roomsight', component: ContainerComponent, pathMatch: 'full' },
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