import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Item } from '../models/item';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  private brandsURL = 'https://localhost:5001/api/brands/';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient) { }

  getBrands(): Observable<Item[]> {
    return this.http.get<Item[]>(this.brandsURL);
  }

  addBrand(brand : any) : any {
    return this.http.post(this.brandsURL, brand);
  }
}
