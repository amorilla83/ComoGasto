import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {

  private purchaseURL = 'https://localhost:5001/api/purchase/';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient) { }

  getPurchases(): Observable<any> {
    return this.http.get(this.purchaseURL);
  }

  getProductsByPurchase (idPurchase: number) : Observable<any> {
    return this.http.get(this.purchaseURL + idPurchase + '/product');
  }

  getPurchase (idPurchase: number) : Observable<any> {
    return this.http.get(this.purchaseURL + idPurchase);
  }

  addPurchase(purchase : any) : any {
    return this.http.post(this.purchaseURL, purchase);
  }

  addProductToPurchase (productPurchase: any, idPurchase: number) : any {
    return this.http.put(this.purchaseURL + idPurchase + '/product', productPurchase);
  }
}
