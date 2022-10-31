import { Product } from "./product";
import { ProductDetails } from "./productDetails";
import { Purchase } from "./purchase";

export interface ProductPurchase {
    id: number;
    purchaseId: number;
    purchase: Purchase;
    productDetail: ProductDetails;
    productId: number;
    product: Product;
    price: number;
    quantity: number;
    weight: number;
    details: string;
  }