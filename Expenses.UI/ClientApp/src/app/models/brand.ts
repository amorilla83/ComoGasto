import { Item } from "./item";

export interface Brand {
    id?: number;
    name: string;
    formatList: Item[];
    productId?: number;
  }