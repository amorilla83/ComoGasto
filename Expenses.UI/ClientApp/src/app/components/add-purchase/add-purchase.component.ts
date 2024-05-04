import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NgbAccordionDirective, NgbDate, NgbDateAdapter, NgbDateParserFormatter, NgbDateStruct, NgbDatepickerModule, NgbModal, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAccordionModule } from '@ng-bootstrap/ng-bootstrap';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { Item } from 'src/app/models/item';
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
import { AlertComponent } from '../alert/alert.component';
import { NgIf, NgFor, CurrencyPipe, DecimalPipe, NgClass } from '@angular/common';
import { CustomAdapter, CustomDateParserFormatter } from 'src/app/DataAdapter';
import { SafeURLPipe } from 'src/app/SafeURL.pipe';
import { NameFilterPipe } from 'src/app/Filter.pipe';
import { Observable, OperatorFunction } from 'rxjs';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-add-purchase',
  standalone: true,
  templateUrl: './add-purchase.component.html',
  styleUrls: ['./add-purchase.component.css'],
	imports: [NgbAccordionModule, AlertComponent, RouterModule, NgIf, NgFor, NgbDatepickerModule, SafeURLPipe, NgbTypeaheadModule,
    NameFilterPipe,CurrencyPipe, DecimalPipe, NgClass, FormsModule],
  /*providers: [
		{ provide: NgbDateAdapter, useClass: CustomAdapter },
		{ provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter },
	],
*/})
export class AddPurchaseComponent implements OnInit {
  @ViewChild('accordion') accordionComponent: NgbAccordionDirective;
  date: NgbDateStruct;
  purchase: Purchase;
  listStores: Store[] = [];
  listProducts: Product[] = [];
  successMessage: string;
  errorMessage: string;
  //listProductsPurchase: ProductPurchase[] = [];
  title: string;
  searchText: string;
  selectedStore: number;
  idPurchase: number;
  sortedData: ProductPurchase[] = [];
  currentSortDirection : string;
  currentSortProperty : string = "index";
  //selectedStore: Store;

  /*search: OperatorFunction<string, readonly string[]> = (text$: Observable<string>) =>
  text$.pipe(
    debounceTime(200),
    distinctUntilChanged(),
    map((term) =>
      term.length < 2 ? [] : this.listProducts.filter((p) => p.name.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10),
    ),
  );*/


  constructor(private storeService: StoreService,
    private productService: ProductService,
    private purchaseService: PurchaseService,
    private router: Router,
    private aRoute: ActivatedRoute,
    private modalService: NgbModal) {

    this.idPurchase = +this.aRoute.snapshot.paramMap.get("id")!;
  }


  ngOnInit(): void {

    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.getStores();
    this.getProducts();
    this.esEditar();
  }

  esEditar() {
    if (this.idPurchase != undefined && this.idPurchase != 0) {
      this.purchaseService.getPurchase(this.idPurchase).subscribe(
        data => {
          console.log(data);
          this.purchase = {
            idPurchase: this.idPurchase,
            date: new Date(data.date),
            dateString: "",
            store: data.store,  
            productList: data.productList,
            count: 0,
            total: 0
          };
          this.sort();
          this.purchase.dateString = this.purchase.date.toLocaleDateString();
          console.log(this.purchase.dateString);
          console.log (this.purchase);
          this.date = { day: this.purchase.date.getDate(), month: this.purchase.date.getMonth() + 1, year: this.purchase.date.getFullYear() };
          this.title = "Compra del día " + this.date.day + "/" + this.date.month + "/" + this.date.year + " en " + this.purchase.store.name;
          this.selectedStore = this.purchase.store.id;
          console.log(this.selectedStore);
          this.getProductsPurchase();
          this.accordionComponent.toggle("Product");

          console.log("Next");
          console.log(this.purchaseService.nextId);
          console.log("Previous");
          console.log(this.purchaseService.previousId);
          if (this.purchaseService.nextId == undefined)
          {
            this.purchaseService.getPurchases().subscribe(purchases => {this.purchaseService.changePurchase(this.idPurchase)});
            
          }
        },
        error => {
          console.log(error);
          this.errorMessage = "Error al obtener la compra";
        }
      )
    }
    else
    {
      this.purchase = new Purchase ();
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
        this.errorMessage ="Error al obtener las tiendas";
      });
  }

  getProducts() {
    this.productService.getProducts().subscribe(
      data => {
        console.log(data);
        this.listProducts = data.items;
      },
      error => {
        console.log(error);
        this.errorMessage = "Error al obtener los productos";
      });
  }

  getProductsPurchase() {
    this.purchaseService.getProductsByPurchase(this.purchase.idPurchase).subscribe(
      data => {
        console.log(data);
        this.purchase.productList = data;
        this.purchase.count = this.purchase.productList.length;
        this.sort();
      },
      error => {
        console.log(error);
        this.errorMessage = "Error al obtener los productos de la compra";
      }
    )
  }

  onDateSelection(date: NgbDate) {
    console.log(this.date);
    this.date = date;
    this.purchase.date = new Date(this.date.year, this.date.month - 1, this.date.day)
    this.purchase.dateString = this.date.day + "/" + this.date.month + "/" + this.date.year;
    this.title = "Compra del día " + this.date.day + "/" + this.date.month + "/" + this.date.year;
    console.log(this.purchase.date);
    this.accordionComponent.toggle("Store");
  }

  selectedProduct (event)
  {
    if (event.target.value != "")
    {
      console.log(event);
      let product = this.listProducts.find(s => s.name == event.target.value);
      if (product == null)
      {
        //Add product y abrir modal
        let productItem: Item = {id: 0, name: event.target.value};
              //Product = { id: this.listProducts.length + 1, name: res, productDetails: [] };
              this.productService.addProduct(productItem).subscribe(
                (response) => {
                  this.listProducts.push(response);
                  this.addProductToPurchase(response.id);
                  console.log(response);
                  this.successMessage = "Se ha añadido el nuevo producto";
                },
                (error) => {
                  console.log(error);
                  this.errorMessage = "Error al añadir el producto";
                });
      }
      else
      {
        this.addProductToPurchase(product.id);
      }
      this.searchText = "";
    }
  }

  selectStore(id: number) {
    console.log("Select Store" + id.toString());
    console.log(this.listStores);
    this.purchase.store = this.listStores.find(s => s.id == id);
    this.selectedStore = id;
    this.title += " en " + this.purchase.store.name;
    this.accordionComponent.toggle("Product");
  }

  triggerAddModal(typeItem: string) {
    if (typeItem == 'tienda') {
      const modalRef = this.modalService.open(AddStoreComponent, { ariaLabelledBy: 'modal-basic-title' });
      modalRef.componentInstance.storeEdit = undefined;
      modalRef.componentInstance.newStore = true;
      modalRef.result.then(
        (res) => {
          this.listStores.push(res);
          this.successMessage ="Tienda añadida correctamente";
        }, (error) => {
          console.log(error);
          this.errorMessage = "Error al añadir la tienda";
        });
    }

    if (typeItem == 'producto') {
      const modalRef = this.modalService.open(AddItemComponent, { ariaLabelledBy: 'modal-basic-title' });
      modalRef.componentInstance.typeItem = typeItem;
      modalRef.result.then(
        (res) => {
          //Añadir producto a la lista
          console.log(res);
          if (res != null) {

            let productItem: ProductPurchase = res;
            //Product = { id: this.listProducts.length + 1, name: res, productDetails: [] };
            this.productService.addProduct(productItem).subscribe(
              (response) => {
                this.listProducts.push(response);
                console.log(response);
                this.successMessage = "Se ha añadido el nuevo producto";
              },
              (error) => {
                console.log(error);
                this.errorMessage = "Error al añadir el producto";
              });
          }
        },
        (error) => {
          console.log(error);
          this.errorMessage ="Error al añadir el producto";
        }
      )
    }
  }

  addProductToPurchase(id: number) {
    //Mostrar el modal con el formulario para las diferentes opciones de un producto
    const modalRef = this.modalService.open(AddProductComponent, { ariaLabelledBy: 'modal-basic-title' });

    let productPurchase: ProductPurchase = {
      id: 0,
      purchaseId: this.idPurchase,
      productId: id,
      product: this.listProducts.find(p => p.id == id),
      quantity : 0,
      price : 0,
      weight: 0,
      details: '',
      productDetail: {
        id: 0,
        product: this.listProducts.find(p => p.id == id),
        productId: id,
        brand: undefined,
        brandId: 0,
        format: undefined,
        formatId: 0,
        lastPrice: 0
      },
      purchase: this.purchase
    };
    modalRef.componentInstance.productPurchase = productPurchase;
    modalRef.result.then(
      async (res) => {
        console.log(res);

        if (res != undefined) {
          //TODO: Añadir la compra y el producto a base de datos
          if (this.purchase.idPurchase == undefined || this.purchase.idPurchase == 0) {

            // let purchase: Purchase =
            // {
            //   date: new Date(this.date.year, this.date.month, this.date.day),
            //   store: this.purchase.store,
            //   count: 0,
            //   total: 0,
            //   idPurchase: this.purchase.idPurchase,
            //   productList: undefined
            // };
            console.log("Añadimos la compra");
            console.log(this.purchase);
        
            await this.purchaseService.addPurchase(this.purchase).subscribe(
              data => {
                console.log(data);
                console.log("Compra guardada");
                this.purchase.idPurchase = data.idPurchase;
                console.log("AddedPurchase: " + this.purchase.idPurchase.toString());
                console.log(this.purchase);
              console.log("Añadimos un producto a la compra " + this.purchase.idPurchase.toString());
              //Añadimos el producto a la compra
              this.purchaseService.addProductToPurchase(res, this.purchase.idPurchase).subscribe(
                dataProduct => {
                  console.log(dataProduct);
                  this.successMessage = "Producto añadido a la compra";
                  res.id = dataProduct.id;
                  console.log(res);
                  this.purchase.productList = [];
                  this.purchase.productList.push(dataProduct);
                  this.sort();
                  console.log(this.purchase);
                },
                error => {
                  console.log(error);
                  this.errorMessage = "Error al añadir el producto a la compra";
                });
              },
              error => {
                console.log(error);
                this.purchase.idPurchase = 0;
                this.errorMessage ="Error al añadir la compra";
              });
          }
          else {
            console.log("Añadimos un producto a la compra " + this.purchase.idPurchase.toString());
            //Añadimos el producto a la compra
            this.purchaseService.addProductToPurchase(res, this.purchase.idPurchase).subscribe(
              data => {
                console.log(data);
                this.successMessage = "Producto añadido a la compra";
                console.log(res);
                this.purchase.productList.push(data);
                this.sort();
                console.log(this.purchase.productList);
              },
              error => {
                console.log(error);
                this.errorMessage ="Error al añadir el producto a la compra";
              });
          }
        }
      }, (error) => {
        console.log(error);
        this.errorMessage = "Error al añadir el producto a la compra";
      });
  }

  deleteProductPurchase(idProductPurchase : number) {
    this.purchaseService.deleteProductFromPurchase(this.purchase.idPurchase, idProductPurchase).subscribe(
      data => {
        console.log(data);
        this.successMessage = "Producto eliminado de la compra";

        let indexDelete = this.purchase.productList.findIndex(p => p.id == idProductPurchase);
        this.purchase.productList.splice(indexDelete, 1);
        this.sort();
      },
      error => {
        console.log(error);
        this.errorMessage = "Error al eliminar el producto de la compra";
      }
    )
  }

  editProductPurchase (idProductPurchase: number) {
    const modalRef = this.modalService.open(AddProductComponent, { ariaLabelledBy: 'modal-basic-title' });
    
    modalRef.componentInstance.productPurchase = this.purchase.productList.find(p => p.id == idProductPurchase);
    modalRef.result.then(
      async (res) => {
        console.log(res);
        if (res != undefined)
        {
        console.log("Modificamos un producto de la compra " + this.purchase.idPurchase.toString());
            //Añadimos el producto a la compra
            this.purchaseService.addProductToPurchase(res, this.purchase.idPurchase).subscribe(
              data => {
                console.log(data);
                this.successMessage = "Producto modificado en la compra";
                //Vuelve sin la información para mostrar en la tabla
                let index = this.purchase.productList.findIndex(p => p.id == idProductPurchase);
                //this.purchase.productList.splice(indexDelete, 1);
                //this.purchase.productList.push(res);
                this.purchase.productList[index] = res;
                this.sort();
                console.log(this.purchase.productList);
              },
              error => {
                console.log(error);
                this.errorMessage ="Error al modificar el producto de la compra";
              });
        }
      }, (error) => {
        console.log(error);
        this.errorMessage ="Error al modificar el producto de la compra";
      }
    );
  }

  updatePurchase ()
  {
    if (this.purchase.idPurchase != undefined || this.purchase.idPurchase != 0) 
    {

      console.log("Modificamos la compra");
      console.log(this.purchase);
  
      this.purchaseService.updatePurchase(this.purchase).subscribe(
        data => {
          console.log(data);
          this.successMessage = "Compra guardada";
        },
        error => {
          console.log(error);
          this.errorMessage ="Error al modificar la compra";
        });
    }
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
    for (let i = 0; i < this.purchase.productList.length; i++) {
      total += this.purchase.productList[i].price * this.purchase.productList[i].quantity;
    }

    return total;
  }

  sort (property : string = "index") {
    if (property == 'index' && this.currentSortProperty == "index")
    {
      //No se ordena
      console.log("no se ordena")
      this.sortedData = this.purchase.productList;
      return;
    }
    else if (property == "index" && this.currentSortProperty != "index")
    {
      console.log("se utiliza el orden anterior " + this.currentSortProperty);
      //Se utiliza el orden anterior
      this.sort(this.currentSortProperty);
    }
    else if (property == this.currentSortProperty) {
      if (this.currentSortDirection === 'asc')
      {
        console.log("Orden desc");
        this.currentSortDirection = 'desc';
      }
      else if (this.currentSortDirection == '')
      {
        console.log("Orden asc");
        this.currentSortDirection = 'asc';
      }
      else
      {
        console.log("Sin orden");
        this.sortedData = this.purchase.productList;
        this.currentSortDirection = '';
        this.currentSortProperty = "index";
        return;
      }
    }
    else
    {
      console.log("informa variables current");
      this.currentSortDirection = 'asc';
      this.currentSortProperty = property;
    }
    console.log(this.currentSortDirection);
    console.log(this.currentSortProperty);


    this.sortedData.sort((a, b) => {
      let comparison : number;
      switch(property) {
        case "product":
          {
            comparison = a.product.name > b.product.name ? 1 : -1;
            break;
          }
      }
      console.log(a);
      console.log(b);
      return this.currentSortDirection === 'asc' ? comparison : -comparison;
    });
  }

  async addPurchase() {
    let purchase: Purchase =
    {
      date: new Date(this.date.year, this.date.month, this.date.day),
      dateString: this.date.day + "/" + this.date.month + "/" + this.date.year,
      store: this.purchase.store,
      count: 0,
      total: 0,
      productList: undefined
    };

    await this.purchaseService.addPurchase(purchase).subscribe(
      data => {
        console.log(data);
        this.successMessage = "Compra guardada";
        this.purchase.idPurchase = data.idPurchase;
      },
      error => {
        console.log(error);
        this.purchase.idPurchase = 0;
        this.errorMessage ="Error al añadir la compra"
      });
  }

  nextPurchase() {
    let next = this.purchaseService.nextId;
    this.purchaseService.changePurchase(next);
    this.router.navigate(['/purchase', next]);
  }

  prevPurchase() {
    let previous = this.purchaseService.previousId;
    this.purchaseService.changePurchase(previous);
    this.router.navigate(['/purchase', previous]);
  }
}
