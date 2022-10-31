import { Injectable } from '@angular/core';
import { HighlightSpanKind } from 'typescript';

@Injectable({
  providedIn: 'root'
})
export class DetailService {
  private listItems : any[];
  private nextItem: number;
  private previousItem: number;

  constructor () {}

  setAllValues(list: any[], next: number, previous: number) 
  { 
    this.listItems = list;
    this.nextItem = next;
    this.previousItem = previous;
  }

  setValues (next: number, previous: number)
  {
    this.nextItem = next;
    this.previousItem = previous;
  }

  setList (list: any[])
  {
    this.listItems = list;
  }

  setNext ()
  {
    
  }

  setPrevious (currentIndex: number)
  {
    
  }

  getSize () : number
  {
    return this.listItems.length;
  }

}
