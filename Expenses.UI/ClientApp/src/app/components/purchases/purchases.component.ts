import { Component, OnInit } from '@angular/core';
import { Purchase } from 'src/app/models/purchase';
import { PurchaseService } from 'src/app/services/purchase.service';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.css']
})
export class PurchasesComponent implements OnInit {

  listPurchases: Purchase[] = [];

  constructor(private purchaseService: PurchaseService) { 
    //Tenemos que hacer el constructor añadiendo el servicio de compras
  }

  ngOnInit(): void {
    this.getPurchases ();
  }

  getPurchases() {
    this.purchaseService.getPurchases().subscribe(
      data => {
        console.log(data);
        this.listPurchases = data;
      },
      error => {
        console.log(error);
      }
    );
  }

  editPurchase (purchase : Purchase) {
    //TODO: Hay que ir a la pagina de addPurchase cargando todos los datos de la compra, incluída la lista de productos
  }

}
