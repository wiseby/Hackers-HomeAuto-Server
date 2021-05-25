import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ConfigurationModule } from './configurations/configuration.module';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { RoomsightModule } from './roomsight/roomsight.module';
import { getBaseUrl } from 'src/main';
import { SharedModule } from '@shared/shared.module';

@NgModule({
  declarations: [AppComponent, NavbarComponent],
  imports: [
    ConfigurationModule,
    RoomsightModule,
    SharedModule,
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
  ],
  providers: [{ provide: 'BASE_URL', useFactory: getBaseUrl }],
  bootstrap: [AppComponent],
})
export class AppModule {}
