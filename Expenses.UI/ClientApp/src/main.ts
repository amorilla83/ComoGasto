/***************************************************************************************************
 * Load `$localize` onto the global scope - used if i18n tags appear in Angular templates.
 */
import '@angular/localize/init';
import { enableProdMode, importProvidersFrom } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';


import { environment } from './environments/environment';
import { AppComponent } from './app/app.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { withInterceptorsFromDi, provideHttpClient } from '@angular/common/http';
import { AppRoutingModule } from './app/app-routing.module';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { DatePipe } from '@angular/common';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

bootstrapApplication(AppComponent, {
    providers: [
        importProvidersFrom(BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }), AppRoutingModule, FormsModule, ReactiveFormsModule, NgbModule, InfiniteScrollModule),
        DatePipe,
        provideHttpClient(withInterceptorsFromDi())
    ]
})
  .catch(err => console.log(err));