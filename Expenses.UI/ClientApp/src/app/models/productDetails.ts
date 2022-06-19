import { Product } from "./product";
import { Item } from "./item";

export interface ProductDetails {
    id: number;
    productId: number;
    product: Product;
    brandId: number;
    brand:Item;
    formatId: number;
    format:Item;
    lastPrice: number;
  }