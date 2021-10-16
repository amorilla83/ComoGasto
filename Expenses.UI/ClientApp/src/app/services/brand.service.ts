import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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

  getBrands(): Observable<any> {
    return this.http.get(this.brandsURL);
  }

  addBrand(brand : any) : any {
    return this.http.post(this.brandsURL, brand);
  }
}
