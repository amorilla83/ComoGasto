import { ProductDetails } from "./productDetails";

export interface Product {
    id?: number;
    name: string;
    productDetails: ProductDetails[];
  }

export interface ProductReview  {
  idProduct: number;
  nameProduct: string;
  purchaseCount: number;
  formatsCount: number;
  brandsCount:number;
  lastDate: Date;
  lastPrice: number;
}