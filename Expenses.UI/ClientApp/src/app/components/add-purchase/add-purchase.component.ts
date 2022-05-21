import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbAccordion, NgbDateStruct, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Product } from 'src/app/models/product';
import { ProductPurchase } from 'src/app/models/productPurchase';
import { Purchase } from 'src/app/models/purchase';
import { Store } from 'src/app/models/store';
import { ProductService } from 'src/app/services/product.service';
import { PurchaseService } from 'src/app/services/purchase.service';
import { StoreService } from 'src/app/services/store.service';
import { AddItemComponent } from '../add-item/add-item.component';
import { AddProductComponent } from '../add-product/add-product.component';
import { AddStoreComponent } from '../add-store/add-store.component';

@Component({
  selector: 'app-add-purchase',
  templateUrl: './add-purchase.component.html',
  styleUrls: ['./add-purchase.component.css']
})
export class AddPurchaseComponent implements OnInit {
  @ViewChild('acc') accordionComponent: NgbAccordion;
  listStores: Store[] = [];
  listProducts: Product[] = [];
  listProductsPurchase: ProductPurchase[] = [];
  date: NgbDateStruct;
  title: boolean;
  searchText: string;
  selectedStore: number;
  idPurchase: number;

  constructor(private storeService: StoreService,
    private productService: ProductService,
    private purchaseService: PurchaseService,
    private aRoute: ActivatedRoute,
    private modalService: NgbModal) {

    this.idPurchase = +this.aRoute.snapshot.paramMap.get("id")!;
  }


  ngOnInit(): void {
    this.esEditar();
  }

  esEditar() {

    if (this.idPurchase != undefined && this.idPurchase != 0) {
      this.purchaseService.getPurchase(this.idPurchase).subscribe(
        data => {
          console.log(data);
          let date: Date = new Date(data.date);
          this.title = true;
          this.selectedStore = data.store.id;
          this.date = { day: date.getUTCDate(), month: date.getUTCMonth() + 1, year: date.getUTCFullYear() };
          this.getStores();
          this.getProducts();
          this.getProductsPurchase();
          this.accordionComponent.toggle("Product");
          console.log (date);
          console.log(this.date);
        },
        error => {
          console.log(error);
        }
      )
    }
  }

  getStores() {
    this.storeService.getStores().subscribe(
      data => {
        console.log(data);
        this.listStores = data;
      },
      error => {
        console.log(error);
      });
  }

  getProducts() {
    this.productService.getProducts().subscribe(
      data => {
        console.log(data);
        this.listProducts = data;
      },
      error => {
        console.log(error);
      });
  }

  getProductsPurchase() {
    this.purchaseService.getProductsByPurchase(this.idPurchase).subscribe(
      data => {
        console.log(data);
        this.listProductsPurchase = data;
      },
      error => {
        console.log(error);
      }
    )
  }

  onDateSelection(event) {
    console.log(this.date);
    this.title = true;
    this.getStores();
    this.accordionComponent.toggle("Store");
  }

  triggerAddModal(typeItem: string) {
    if (typeItem == 'tienda') {
      const modalRef = this.modalService.open(AddStoreComponent, { ariaLabelledBy: 'modal-basic-title' });
      modalRef.componentInstance.storeEdit = undefined;
      modalRef.componentInstance.newStore = true;
      modalRef.result.then(
        (res) => {
          this.getStores();
        }, (error) => {
          console.log(error);
        });
    }

    if (typeItem == 'producto') {
      const modalRef = this.modalService.open(AddItemComponent, { ariaLabelledBy: 'modal-basic-title' });
      modalRef.componentInstance.typeItem = typeItem;
      modalRef.result.then(
        (res) => {
          //Añadir producto a la lista
          console.log(res);
          if (!res.includes('click')) {

            let productItem: Product = { id: this.listProducts.length + 1, name: res };
            this.productService.addProduct(productItem).subscribe(
              (response) => {
                this.listProducts.push(response);
                console.log(response);
              },
              (error) => console.log(error));

            console.log("Nuevo producto añadido a la lista");
          }
        },
        (error) => {
          console.log(error);
        }
      )
    }
  }

  selectStore(id: number) {
    console.log("Select Store" + id.toString());
    this.selectedStore = id;
    this.getProducts();
    this.accordionComponent.toggle("Product");
  }

  addProductToPurchase(id: number) {
    //Mostrar el modal con el formulario para las diferentes opciones de un producto
    const modalRef = this.modalService.open(AddProductComponent, { ariaLabelledBy: 'modal-basic-title' });

    modalRef.componentInstance.product = this.listProducts.find(p => p.id == id);
    modalRef.result.then(
      async (res) => {
        console.log(res);

        if (res != undefined) {
          //TODO: Añadir la compra y el producto a base de datos
          if (this.idPurchase == undefined || this.idPurchase == 0) {

            let purchase: Purchase =
            {
              date: new Date(this.date.year, this.date.month, this.date.day),
              store: { id: this.selectedStore, name: '', logo: '', image: '' },
              count: 0,
              total: 0,
              productList: undefined
            };
        
            await this.purchaseService.addPurchase(purchase).subscribe(
              data => {
                console.log(data);
        
                console.log("Compra guardada");
                this.idPurchase = data.idPurchase;
                console.log("AddedPurchase: " + this.idPurchase.toString());
              console.log("Añadimos un producto a la compra" + this.idPurchase.toString());
              res.product = this.listProducts.find(p => p.id == id).name;
              //Añadimos el producto a la compra
              this.purchaseService.addProductToPurchase(res, this.idPurchase).subscribe(
                dataProduct => {
                  console.log(dataProduct);
                  console.log("Producto añadido a la compra");
                  this.listProductsPurchase = (dataProduct.productList);
                  console.log(this.listProductsPurchase);
                },
                error => {
                  console.log(error);
                });
              },
              error => {
                console.log(error);
                this.idPurchase = 0;
              });
          }
          else {
            console.log("Añadimos un producto a la compra" + this.idPurchase.toString());
            res.product = this.listProducts.find(p => p.id == id).name;
            //Añadimos el producto a la compra
            this.purchaseService.addProductToPurchase(res, this.idPurchase).subscribe(
              data => {
                console.log(data);
                console.log("Producto añadido a la compra");
                this.listProductsPurchase.push(data);
                console.log(this.listProductsPurchase);
              },
              error => {
                console.log(error);
              });
          }
        }
      }, (error) => {
        console.log(error);
      });
  }

  deleteProductPurchase(idProduct : number) {
    this.purchaseService.deleteProductFromPurchase(this.idPurchase, idProduct).subscribe(
      data => {
        console.log(data);
        console.log("Producto eliminado");

        let indexDelete = this.listProductsPurchase.findIndex(p => p.productId == idProduct);
        this.listProductsPurchase.splice(indexDelete, 1);
      },
      error => {
        console.log(error);
      }
    )
  }

  editProductPurchase (idProduct: number) {
    
  }

  getTotalProduct(product: ProductPurchase): number {
    if (product.quantity > 0) {
      return product.price * product.quantity;
    }

    if (product.weight > 0) {
      return product.price;
    }

  }

  getTotal(): number {
    let total = 0.0;
    for (let i = 0; i < this.listProductsPurchase.length; i++) {
      total += this.listProductsPurchase[i].price * this.listProductsPurchase[i].quantity;
    }

    return total;
  }

  async addPurchase() {
    //Guarda la compra completa en base de datos
    //date
    //selectedStore
    //listProductsPurchase
    let purchase: Purchase =
    {
      date: new Date(this.date.year, this.date.month, this.date.day),
      store: { id: this.selectedStore, name: '', logo: '', image: '' },
      count: 0,
      total: 0,
      productList: undefined
    };

    await this.purchaseService.addPurchase(purchase).subscribe(
      data => {
        console.log(data);

        console.log("Compra guardada");
        this.idPurchase = data.idPurchase;
      },
      error => {
        console.log(error);
        this.idPurchase = 0;
      });
  }
}
