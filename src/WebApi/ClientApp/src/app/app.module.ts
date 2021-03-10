import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ConfigurationModule } from './modules/configurations/configuration.module';
import { NavbarComponent } from './modules/common/navbar/navbar.component';
import { RoomsightModule } from './modules/roomsight/roomsight.module';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent
  ],
  imports: [
    ConfigurationModule,
    RoomsightModule,
    AppRoutingModule,
    BrowserModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
