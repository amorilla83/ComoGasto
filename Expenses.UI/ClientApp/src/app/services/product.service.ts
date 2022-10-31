import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { Item } from '../models/item';
import { Pagination } from '../models/pagination';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private productURL = 'https://localhost:5001/api/products/';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient) { }

  getProducts(page: number = 1, itemsPerPage: number = 0): Observable<Pagination<Product>> {

    let params = new HttpParams()
    .set("page", page?.toString())
    .set("itemsPerPage", itemsPerPage?.toString());

   return this.http.get<Pagination<Product>>(this.productURL, {params: params});
  }

  getProductDetails(id: number): Observable<Product> {
    return this.http.get<Product>(this.productURL + id)
  }

  getBrandsByProduct (id: number): Observable<any> {
    return this.http.get(this.productURL + id + '/brands/');
  }

  getPurchasesByProduct(id: number): Observable<any> {
    return this.http.get(this.productURL + id + '/purchases/');
  }

  addProduct(product : any) : any {
    return this.http.post(this.productURL, product);
  }

  getFormatsByBrand (id: number): Observable<Item[]> {
    return this.http.get<Item[]>('https://localhost:5001/api/details/formats/' + id);
  }
}
