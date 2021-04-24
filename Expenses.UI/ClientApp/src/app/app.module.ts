import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { StoresComponent } from './components/stores/stores.component';
import { AddPurchaseComponent } from './components/add-purchase/add-purchase.component';
import { ProductsComponent } from './components/products/products.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { AddStoreComponent } from './components/add-store/add-store.component';
import { AppRoutingModule } from './app-routing.module';
import { SafeURLPipe } from './SafeURL.pipe';
import { DeleteModalComponent } from './components/delete-modal/delete-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    StoresComponent,
    AddPurchaseComponent,
    ProductsComponent,
    AddProductComponent,
    AddStoreComponent,
    SafeURLPipe,
    DeleteModalComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
