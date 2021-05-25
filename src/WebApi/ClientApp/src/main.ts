import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

export function getBaseUrl(): string {
  let baseUrl = document.getElementsByTagName('base')[0].href;

  console.log('BaseUrl: ', baseUrl);

  if (environment.local) {
    baseUrl = 'http://localhost:3000';
  }
  baseUrl = baseUrl.replace(/(\/\/?)$/g, '');
  return baseUrl;
}

platformBrowserDynamic()
  .bootstrapModule(AppModule)
  .catch((err) => console.error(err));
