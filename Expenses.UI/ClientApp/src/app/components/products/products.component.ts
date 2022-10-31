import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/models/pagination';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  productList: Product[] = [];
  pagination: Pagination<Product>;
  show: boolean;
  selectedProduct: Product;

  constructor(public productService: ProductService) { }

  ngOnInit(): void {
    this.pagination = {
      items: [],
      itemsPerPage: 20,
      pageNumber: 1,
      countItems: 0,
      countPage: 0
    };
    this.getProducts();
    this.show = false;
  }

  getProducts() : void {
    this.productService.getProducts(this.pagination.pageNumber, this.pagination.itemsPerPage)
    .subscribe (data => {
      console.log(data);
      this.pagination = data;
      this.productList = data.items;

      console.log(this.productList);
    },
    error => {
      console.log(error);
    });

    // this.productService.getProducts(this.productService.page, this.productService.pageSize)
    //   .subscribe(productPagination => {
    //     this.productService.items = productPagination.items;
    //     this.productService.count = productPagination.count; 
    //   });
  }

  onPageChange(newPage: number): void {
    //this.productService.page = newPage;
    this.pagination.pageNumber = newPage;
    this.getProducts();
    window.scrollTo(0, 0);
  }

  showDetails (idProduct: number) :void {
    //Hay que mirar cómo hacer para cerrar uno que ya esté abierto. Puede haber varios abiertos
    if (this.productList.find(p => p.id == idProduct).productDetails == null
      ||this.productList.find(p => p.id == idProduct).productDetails.length == 0 )
    {
      this.productService.getProductDetails(idProduct).subscribe(data => {
        console.log(data);
        //Se añaden los detalles del producto al productDetails del producto
        this.productList.find(p => p.id == idProduct).productDetails = data.productDetails;
      },
      error => {
        console.log(error);
      }
      );
    }
  }

  onSelect (product: Product):void{
    this.selectedProduct = product;
  }

  closeDetails (): void {
    this.selectedProduct = null;
  }

}
