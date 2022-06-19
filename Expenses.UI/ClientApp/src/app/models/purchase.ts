import { ProductPurchase } from "./productPurchase";
import { Store } from "./store";

export class Purchase {
    idPurchase?: number;
    date: Date;
    store: Store;
    count: number;
    total: number;
    productList : ProductPurchase[]
  }