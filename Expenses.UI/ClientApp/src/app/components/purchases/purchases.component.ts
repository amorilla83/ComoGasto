import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { FilterPurchase } from 'src/app/models/filterPurchase';
import { Purchase } from 'src/app/models/purchase';
import { Store } from 'src/app/models/store';
import { PurchaseService } from 'src/app/services/purchase.service';
import { StoreService } from 'src/app/services/store.service';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.css']
})
export class PurchasesComponent implements OnInit {

  filterPurchases: Purchase[] = [];
  totalPurchases: Purchase[] = [];
  listStores: Store[] = [];
  errorMessage : string;
  successMessage: string;
  filterData: UntypedFormGroup;

  constructor(private purchaseService: PurchaseService, private storeService: StoreService,
    private fb: UntypedFormBuilder) { 
    
  }

  ngOnInit(): void {
    this.filterData = this.fb.group(
      {
        dateStart:[],
        dateEnd: [],
        storeId: [''],
        maxPrice: ['0'],
        minPrice: ['0']
      });
    this.getStores();
    this.getPurchases ();
  }

  getStores() {
    this.storeService.getStores().subscribe(
      data => {
        console.log(data);
        this.listStores = data;
      },
      error => {
        console.log(error);
        this.errorMessage ="Error al obtener las tiendas";
      });
  }

  getPurchases() {
    this.purchaseService.getPurchases().subscribe(
      data => {
        console.log(data);
        this.filterPurchases = data;
        this.totalPurchases = data;
      },
      error => {
        console.log(error);
        this.errorMessage ="Error al obtener el listado de compras";
      }
    );
  }

  deletePurchase (id : number) {
    this.purchaseService.deletePurchase(id).subscribe(
      data => {
        console.log(data);
        this.successMessage = "Compra eliminada";
        let index = this.filterPurchases.findIndex(p => p.idPurchase == id);
        this.filterPurchases.splice(index, 1);
      },
      error => {
        console.log(error);
        this.errorMessage = "Error al eliminar la compra";
      }
    );
  }

  filter () {
    console.log(this.filterData);
    let storeId = this.filterData.get('storeId').value;
    if (storeId != "" && storeId != null)
    {
      console.log ("Se filtra por tienda");
      this.filterPurchases = this.totalPurchases.filter(p => p.store.id == storeId);
    }
    else 
    {
      //De momento solo aplico el filtro de tiendas, cuando haya más habrá que aplicar los que corresponda
      this.filterPurchases = this.totalPurchases;
    }
  }

  limpiar () {
    this.filterPurchases = this.totalPurchases;
    this.filterData.patchValue({storeId: ''});
    this.filterData.patchValue({dateStart: ''});
  }

}
