import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { isThisTypeNode, textChangeRangeIsUnchanged } from 'typescript';
import { ProductPurchase } from '../models/productPurchase';
import { Purchase } from '../models/purchase';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {

  private purchaseListSubject: BehaviorSubject<Purchase[]> = new BehaviorSubject({} as Purchase[]);
  public purchaseList: Observable<Purchase[]> = this.purchaseListSubject.asObservable();

  public nextId: number;
  public previousId: number;

  private purchaseURL = 'https://localhost:5001/api/purchase/';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient) { }

  changePurchase (currentId: number)
  {
    if (this.purchaseListSubject.getValue().length > 0)
    {
      let index = this.purchaseListSubject.getValue().findIndex(p => p.idPurchase == currentId);
      if (this.purchaseListSubject.getValue().length > index)
      {
        this.nextId = this.purchaseListSubject.getValue()[index+1].idPurchase;
      }
      else
      {
        this.nextId = 0;
      }

      if (index > 0)
      {
        this.previousId = this.purchaseListSubject.getValue()[index-1].idPurchase;
      }
      else
      {
        this.previousId = 0;
      }
    }
  }

  setPurchaseList(purchases: Purchase[])
  {
    this.purchaseListSubject.next(purchases);
  }

  getPurchases(): Observable<Purchase[]> {
    this.http.get<Purchase[]>(this.purchaseURL).subscribe(purchases => 
      {
        this.setPurchaseList(purchases);
      });
      return this.purchaseList;
  }

  getProductsByPurchase (idPurchase: number) : Observable<ProductPurchase[]> {
    return this.http.get<ProductPurchase[]>(this.purchaseURL + idPurchase + '/product');
  }

  getPurchase (idPurchase: number) : Observable<Purchase> {
    return this.http.get<Purchase>(this.purchaseURL + idPurchase);
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
