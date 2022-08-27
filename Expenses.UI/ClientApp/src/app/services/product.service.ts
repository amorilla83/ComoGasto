import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Item } from '../models/item';
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


    //getItems(): Observable<Item[]> {
   //   return of(mock_items);
   // }
  getProducts(): Observable<Product []> {
    return this.http.get<Product[]>(this.productURL);
  }

  getProductDetails(id: number): Observable<Product> {
    return this.http.get<Product>(this.productURL + id)
  }

  getBrandsByProduct (id: number): Observable<any> {
    return this.http.get(this.productURL + id + '/brands/');
  }

  addProduct(product : any) : any {
    return this.http.post(this.productURL, product);
  }

  getFormatsByBrand (id: number): Observable<Item[]> {
    return this.http.get<Item[]>('https://localhost:5001/api/details/formats/' + id);
  }
}
