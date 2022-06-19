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
  errorMessage : string;
  successMessage: string;

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
        this.errorMessage ="Error al obtener el listado de compras";
      }
    );
  }

  deletePurchase (id : number) {
    this.purchaseService.deletePurchase(id).subscribe(
      data => {
        console.log(data);
        this.successMessage = "Compra eliminada";
        let index = this.listPurchases.findIndex(p => p.idPurchase == id);
        this.listPurchases.splice(index, 1);
      },
      error => {
        console.log(error);
        this.errorMessage = "Error al eliminar la compra";
      }
    );
  }

}
