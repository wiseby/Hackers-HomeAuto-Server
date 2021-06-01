import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

export function getBaseUrl(): string {
  let baseUrl = document.getElementsByTagName('base')[0].href;

  if (environment.local) {
    baseUrl = 'http://localhost:3000';
  }
  baseUrl = baseUrl.replace(/(\/\/?)$/g, '');
  return baseUrl;
}

const providers = [{ provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }];

platformBrowserDynamic(providers)
  .bootstrapModule(AppModule)
  .catch((err) => console.error(err));
