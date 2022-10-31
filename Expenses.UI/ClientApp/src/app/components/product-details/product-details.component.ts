import { DatePipe } from '@angular/common';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/models/product';
import { ProductPurchase } from 'src/app/models/productPurchase';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnChanges {

  @Input() product : Product ;
  productPurchases: ProductPurchase[];
  errorMessage: string;

  constructor(private route: ActivatedRoute, private productService: ProductService, private datePipe: DatePipe) { }
  
  ngOnChanges(changes: SimpleChanges): void {
    console.log("ngOnChanges");
    console.log(changes);
    if (this.product != undefined)
    {
      console.log(this.product);
      this.getDetails ();
    }
  }

  getDetails () 
  {
    this.productService.getPurchasesByProduct(this.product.id).subscribe(
      data => {
        this.productPurchases = data;
        this.productPurchases.forEach(p => p.product = this.product);
        console.log(this.productPurchases);
      },
      error => {
        console.log(error);
        this.errorMessage ="Error al obtener las tiendas";
      });
  }
}
