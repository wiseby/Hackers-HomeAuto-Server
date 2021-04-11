import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ConfigurationModule } from './configurations/configuration.module';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { RoomsightModule } from './roomsight/roomsight.module';

@NgModule({
  declarations: [AppComponent, NavbarComponent],
  imports: [
    ConfigurationModule,
    RoomsightModule,
    AppRoutingModule,
    BrowserModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
