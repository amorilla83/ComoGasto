import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { StoresComponent } from './components/stores/stores.component';
import { AddPurchaseComponent } from './components/add-purchase/add-purchase.component';
import { PurchasesComponent } from './components/purchases/purchases.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'stores', component: StoresComponent },
  { path: 'purchase', component: AddPurchaseComponent },
  { path: 'purchase/:id', component: AddPurchaseComponent},
  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  { path: 'purchase-list', component: PurchasesComponent},
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }