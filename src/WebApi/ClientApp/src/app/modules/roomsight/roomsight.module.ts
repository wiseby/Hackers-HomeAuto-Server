import { NgModule } from "@angular/core";
import { RoomComponent } from './components/room/room.component';
import { RoomsightRoutingModule } from "./roomsight-routing.module";

@NgModule({
  declarations: [ RoomComponent ],
  imports: [ RoomsightRoutingModule ]
})
export class RoomsightModule {}