import { ProductPurchase } from "./productPurchase";
import { Store } from "./store";

export class Purchase {
    idPurchase?: number;
    date: Date;
    dateString: string;
    store: Store;
    count: number;
    total: number;
    productList : ProductPurchase[]
  }