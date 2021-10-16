import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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

  getProducts(): Observable<any> {
    return this.http.get(this.productURL);
  }

  getBrandsByProduct (id: number): Observable<any> {
    return this.http.get(this.productURL + id + '/brands/');
  }

  addProduct(product : any) : any {
    return this.http.post(this.productURL, product);
  }
}
