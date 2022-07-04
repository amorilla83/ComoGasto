import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Purchase } from '../models/purchase';

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

  deletePurchase (id: number) : any
  {
    return this.http.delete(this.purchaseURL + id);
  }

  addProductToPurchase (productPurchase: any, idPurchase: number) : any {
    return this.http.put(this.purchaseURL + idPurchase + '/product', productPurchase);
  }

  deleteProductFromPurchase (idPurchase: number, idProduct: number): any{
    return this.http.delete(this.purchaseURL + idPurchase + '/product/' + idProduct);
  }

  updateProductFromPurchase (productPurchase: any, idPurchase: number) : any {
    return this.http.put(this.purchaseURL + idPurchase + '/product/' + productPurchase.id, productPurchase);
  }

  updatePurchase (purchase: Purchase) : any {
    return this.http.put(this.purchaseURL + purchase.idPurchase, purchase);
  }
}
