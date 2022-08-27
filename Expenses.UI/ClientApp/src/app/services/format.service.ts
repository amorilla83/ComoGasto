import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Item } from '../models/item';

@Injectable({
  providedIn: 'root'
})
export class FormatService {

  private formatURL = 'https://localhost:5001/api/formats/';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient) { }

  getFormats(): Observable<Item[]> {
    return this.http.get<Item[]>(this.formatURL);
  }

  addFormat(format : any) : any {
    return this.http.post(this.formatURL, format);
  }
}
