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
import { AddItemComponent } from './components/add-item/add-item.component';
import { AddStoreComponent } from './components/add-store/add-store.component';
import { AppRoutingModule } from './app-routing.module';
import { SafeURLPipe } from './SafeURL.pipe';
import { DeleteModalComponent } from './components/delete-modal/delete-modal.component';
import { NameFilterPipe } from './Filter.pipe';
import { AddProductComponent } from './components/add-product/add-product.component';
import { PurchasesComponent } from './components/purchases/purchases.component';
import { AlertComponent } from './components/alert/alert.component';
import { ReversePipe } from './Reverse.pipe';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { DatePipe } from '@angular/common';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

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
    AddItemComponent,
    AddStoreComponent,
    SafeURLPipe,
    DeleteModalComponent,
    NameFilterPipe,
    AddProductComponent,
    PurchasesComponent,
    AlertComponent,
    ReversePipe,
    ProductDetailsComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    InfiniteScrollModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
