import { ProductDetails } from "./productDetails";

export interface Product {
    id?: number;
    name: string;
    productDetails: ProductDetails[];
  }