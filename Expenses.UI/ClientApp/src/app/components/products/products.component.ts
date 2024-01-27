import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/models/pagination';
import { Product, ProductReview } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  productList: Product[] = [];
  productReviewList: ProductReview[] = [];
  productsToShow: ProductReview[] = [];
  pagination: Pagination<Product>;
  show: boolean;
  selectedProduct: Product;
  idSelected: number;
  searchText: string;

  constructor(public productService: ProductService) { }

  ngOnInit(): void {
    this.pagination = {
      items: [],
      itemsPerPage: 20,
      pageNumber: 1,
      countItems: 0,
      countPage: 0
    };
    //this.getProducts();
    this.getProductsReview();
    
    //this.show = false;
    window.addEventListener('scroll', () => {
      this.show = (window.scrollY != 0);
    });
  }

  add20Items (){
    let count = this.productsToShow.length;
    if (this.productReviewList.length < count + 20)
    {
      this.productsToShow.push(...this.productReviewList.slice(count, this.productReviewList.length));
    }
    else
    {
        this.productsToShow.push(...this.productReviewList.slice(count, count + 20));
    }

    console.log (this.productsToShow);
  }

  getProductsReview (): void {
    this.productService.getProductReview().subscribe(data => {
      console.log(data);
      this.productReviewList = data;
      this.add20Items();
    },
    error => {
      console.log(error);
    });
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
    if (this.productList.find(p => p.id == idProduct) == null)
    {
      let product : Product = {
        id: idProduct,
        name: this.productReviewList.find(p => p.id == idProduct).name,
        productDetails: undefined
      };
      console.log("Añadimos un nuevo producto a la lista");
      this.productList.push(product)
    }

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

  // onSelect (product: Product):void{
  //   this.selectedProduct = product;
  // }

  onSelect (idProduct: number) :void {
    console.log(idProduct);
    this.idSelected = idProduct;
    this.productService.getProductDetails(idProduct).subscribe(data => {
      console.log(data);
      this.selectedProduct = data;
      console.log(this.selectedProduct);
    },
    error => {
      console.log(error);
    }
    );
  }

  closeDetails (): void {
    this.selectedProduct = null;
    this.idSelected = 0;
  }

  onScroll () {
    if (this.productsToShow.length < this.productReviewList.length)
    {
      this.add20Items();
    }  
  }

  scrollToTop () {
    window.scrollTo(0, 0);
  }

}
