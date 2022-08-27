import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/models/product';
import { ProductDetails } from 'src/app/models/productDetails';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  productDetails : ProductDetails [];
  product : Product;

  constructor(private route: ActivatedRoute, private productService: ProductService) { }

  ngOnInit(): void {
    this.getProductDetails ();
  }

  getProductDetails () : void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.productService.getProductDetails(id)
    .subscribe(data => {
      console.log(data);
      this.product = data;
      this.productDetails = data.productDetails;
    });
  }

}
