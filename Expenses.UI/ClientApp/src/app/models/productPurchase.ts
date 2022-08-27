import { Product } from "./product";
import { ProductDetails } from "./productDetails";

export interface ProductPurchase {
    id: number;
    purchaseId: number;
    productDetail: ProductDetails;
    productId: number;
    product: Product;
    price: number;
    quantity: number;
    weight: number;
    details: string;
  }