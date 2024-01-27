import { ProductDetails } from "./productDetails";

export interface Product {
    id?: number;
    name: string;
    productDetails: ProductDetails[];
  }

export interface ProductReview  {
  id: number;
  name: string;
  purchaseCount: number;
  formatsCount: number;
  brandsCount:number;
  lastDate: Date;
  lastPrice: number;
}