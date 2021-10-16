import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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

  getFormats(): Observable<any> {
    return this.http.get(this.formatURL);
  }

  addFormat(format : any) : any {
    return this.http.post(this.formatURL, format);
  }
}
