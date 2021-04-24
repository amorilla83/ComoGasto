import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Store } from '../models/store';

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  private storeURL = 'https://localhost:5001/api/stores/';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

 httpOptionsFile: any = {
    headers: new HttpHeaders({"Content-Type": "multipart/form-data"})         
  };

  constructor(
    private http: HttpClient) { }

  getStores(): Observable<any> {
    return this.http.get(this.storeURL);
  }

  addStore(store : any) : any {
    return this.http.post(this.storeURL, store, this.httpOptionsFile);
  }

  editStore(store: any, id: number): any {
    return this.http.put(this.storeURL + id, store, this.httpOptionsFile);
  }


  deleteStore(store: any) : any {
    return this.http.delete(this.storeURL + store.id);
  }
}
