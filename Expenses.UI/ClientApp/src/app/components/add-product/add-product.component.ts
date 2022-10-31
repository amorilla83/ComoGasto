import { AfterContentChecked, AfterViewInit, ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Brand } from 'src/app/models/brand';
import { Item } from 'src/app/models/item';
import { Product } from 'src/app/models/product';
import { ProductDetails } from 'src/app/models/productDetails';
import { ProductPurchase } from 'src/app/models/productPurchase';
import { Purchase } from 'src/app/models/purchase';
import { BrandService } from 'src/app/services/brand.service';
import { FormatService } from 'src/app/services/format.service';
import { ProductService } from 'src/app/services/product.service';
import { AddItemComponent } from '../add-item/add-item.component';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit, AfterContentChecked {
  @Input() productPurchase: ProductPurchase;
  addProductToPurchase: UntypedFormGroup;
  id = 0;
  brandList: Item[] = [];

  formatList: Item[] = [];

  constructor(private fb:UntypedFormBuilder,
    private productService: ProductService,
    private brandService: BrandService,
    private formatService: FormatService,
    public activeModal: NgbActiveModal,
    private modalService: NgbModal,
    private cdRef:ChangeDetectorRef) { 
    this.addProductToPurchase = this.fb.group (
      {
        marca: [{value: '', disabled: false}],
        formato: [{value: '', disabled: false}],
        precio: ['', Validators.required],
        unidades: ['1'],
        peso: [''],
        granel: false,
        detalles: ['']
      }
    )
  }

  ngOnInit(): void {
    //this.getBrands ();  
    this.getDetails ();
    if (this.productPurchase.id != undefined && this.productPurchase.id != 0)
    {
      console.log(this.productPurchase);
      this.addProductToPurchase.setValue({
        marca: this.productPurchase.productDetail != undefined &&
                  this.productPurchase.productDetail.brand != undefined ? this.productPurchase.productDetail.brand.id : 0,
        formato: this.productPurchase.productDetail != undefined &&
                  this.productPurchase.productDetail.format != undefined ? this.productPurchase.productDetail.format.id : 0,
        precio: this.productPurchase.price,
        unidades: this.productPurchase.quantity,
        peso: this.productPurchase.weight,
        granel: (this.productPurchase.weight != undefined),
        detalles: this.productPurchase.details
      }
      );

      if (this.productPurchase.productDetail == undefined)
      {
        this.granel(true);
      }
    }
    console.log(this.addProductToPurchase);

    this.cdRef.detectChanges();
  }

  ngAfterContentChecked() {

    this.cdRef.detectChanges();
     }

  getDetails ()
  {
    if (this.productPurchase.productDetail != undefined)
    {
    this.productService.getProductDetails(this.productPurchase.productDetail.product.id).subscribe(
      data => {
        console.log(data);
        this.productPurchase.productDetail.product = data;

        let list = this.productPurchase.productDetail.product.productDetails.
          filter(pd => pd.brand != null).map(pd => pd.brand);
          console.log(list);
        this.brandList = list.filter((item, index, self) => self.findIndex(i => i.id == item.id) === index);
        
        if (this.productPurchase.productDetail.brand != undefined && this.productPurchase.productDetail.brand.id != 0)
        {
          this.formatList = this.productPurchase.productDetail.product.productDetails
          .filter(p => p.brand != null && p.brand.id == this.productPurchase.productDetail.brand.id && p.format != null).map(p => p.format);
        }
      },
      error => {
        console.log(error);
      });
    }
  }

  getBrands ()
  {
    this.productService.getBrandsByProduct(this.productPurchase.productDetail.product.id).subscribe(
      data => {
        console.log(data);
        this.brandList = data;
      },
      error => {
        console.log(error);
      });
  }

  checkedChanged (event)
  {
    console.log(event.target.checked);
    this.granel(event.target.checked);
  }

  granel (checked: boolean)
  {
    if (checked)
    {
    this.addProductToPurchase.get('marca').disable();
    this.addProductToPurchase.get('formato').disable();
    }
    else
    {

    this.addProductToPurchase.get('marca').enable();
    this.addProductToPurchase.get('formato').enable();
    }
  }

  addProduct () {
    console.log(this.addProductToPurchase);
    // Peta el product id y marca y formato están vacíos
    let brand = this.brandList.find(b => b.id == this.addProductToPurchase.get('marca').value);
    let format = this.formatList.find(f => f.id == this.addProductToPurchase.get('formato').value);
    let productPurchase : ProductPurchase =
    {
      id: this.productPurchase.id,
      productId: this.productPurchase.product.id,
      product: this.productPurchase.product,
      purchaseId: this.productPurchase.purchaseId,
      quantity : this.addProductToPurchase.get('unidades').value,
      price : this.addProductToPurchase.get('precio').value,
      weight: this.addProductToPurchase.get('peso').value,
      details: this.addProductToPurchase.get('detalles').value,
      productDetail: undefined,
      purchase: undefined
      // productDetail: {
      //   id: this.productPurchase.productDetail.id,
      //   product: this.productPurchase.product,
      //   productId: this.productPurchase.product.id,
      //   brand: brand,
      //   brandId: brand != undefined ? brand.id : 0,
      //   format: format,
      //   formatId: format != undefined ? format.id : 0,
      //   lastPrice: this.addProductToPurchase.get('precio').value
      // }
    }

    if (this.productPurchase.productDetail != undefined)
    {
      productPurchase.productDetail = {
        id: this.productPurchase.productDetail.id,
        product: this.productPurchase.product,
        productId: this.productPurchase.product.id,
        brand: brand,
        brandId: brand != undefined ? brand.id : undefined,
        format: format,
        formatId: format != undefined ? format.id : undefined,
        lastPrice: this.addProductToPurchase.get('precio').value
      };
    }

    console.log('ProductPurchase a añadir')
    console.log(productPurchase);
    this.activeModal.close(productPurchase);

  }

  cancel () {
    this.activeModal.close();
  }

  dismiss() {
    this.activeModal.close();
  }

  onBrandSelected(event) { 
    if (event.target.value == -1) 
    {
      //Abrimos el modal de add-item para añadir una nueva marca al listado
      const modalRef = this.modalService.open(AddItemComponent, {ariaLabelledBy: 'modal-basic-title'});
      modalRef.componentInstance.typeItem = 'marca';
      modalRef.result.then(
        (res) => {
          //Añadir marca a la lista
          console.log(res);
          if (res != null){
            let brandItem: Brand = res;
            //let brandItem : Brand = { name: res, formatList: null, productId: this.product.id};
            if (brandItem.id == 0)
            {
              //Es una nueva marca              
              this.brandService.addBrand(brandItem).subscribe(
                (response) => 
                {
                  console.log("Tenemos la respuesta");
                  console.log(response);
                  console.log(response.id);
                  brandItem.id = response.id;
                  //this.brandList.push(response);
                  //this.formatList = response.formatList;

                  console.log("Nueva marca añadida a la lista");

                  this.addProductToPurchase.patchValue({marca: response.id});
                },
                (error) => console.log(error));
            }
            else
            {
              //La marca ya existe en base de datos pero no está asociada al producto
              //Buscar en productDetails los formatos asociados a la marca existente
              this.productService.getFormatsByBrand(brandItem.id).subscribe(
                (response) => 
                {
                  if (response[0] == null)
                  {
                    this.formatList = [];
                  }
                  else 
                  {
                  this.formatList = response;
                  }
                }
              );
              this.addProductToPurchase.patchValue({marca: brandItem.id});
            }
            this.brandList.push(brandItem);

          }
        },
        (error) => {
          console.log(error);
        }
      )

      this.addProductToPurchase.patchValue({precio: ''});
    }
    else {
      console.log ('Marca seleccionada ');
      this.addProductToPurchase.patchValue({marca: event.target.value});
      console.log(this.productPurchase.productDetail.product.productDetails);
      console.log (this.productPurchase.productDetail.product.productDetails.filter(p => p.brand != null && p.brand.id == event.target.value));
      this.formatList = this.productPurchase.productDetail.product.productDetails
      .filter(p => p.brand != null && p.brand.id == this.addProductToPurchase.get('marca').value && p.format != null).map(p => p.format);
      this.formatList.sort((a: Item, b: Item) => (a.name < b.name) ? 1 : -1);
      let productDetail = this.productPurchase.productDetail.product.productDetails
                    .find(p => (p.format == null) &&
                    p.brand != null && p.brand.id == this.addProductToPurchase.get('marca').value);
      this.addProductToPurchase.patchValue({productDetail: productDetail, precio: productDetail.lastPrice});
                 
      if (productDetail == undefined)
      {
        this.addProductToPurchase.patchValue({precio: ''});
      }
      console.log(this.formatList);
    }

    this.addProductToPurchase.patchValue({formato: ''});
  }

  onFormatSelected(event) { 

    let brand : number = this.addProductToPurchase.get('marca').value

    // if (brand == undefined)
    // {
    //   alert("Se debe seleccionar una marca antes de añadir el formato");
    //   return;
    // }
    if (event.target.value == -1) 
    {
      //Abrimos el modal de add-item
      const modalRef = this.modalService.open(AddItemComponent, {ariaLabelledBy: 'modal-basic-title'});
      modalRef.componentInstance.typeItem = 'formato';
      modalRef.result.then(
        (res) => {
          //Añadir formato a la lista
          console.log(res);
          if (res != null){
            let formatItem : Item = res;

            if (formatItem.id == 0)
            {
              //Es un nuevo formato a añadir a la base de datos
              this.formatService.addFormat(formatItem).subscribe(
                (response) => 
                {
                  console.log("Tenemos la respuesta");
                  console.log(response);
                  console.log(response.id);
                  formatItem.id = response.id;
                  //this.formatList.push(response);
  
                  console.log("Nuevo formato añadida a la lista");

                  this.addProductToPurchase.patchValue({formato: response.id});
                },
                (error) => console.log(error));
              console.log("Nuevo formato añadido a la lista");
            }
            else
            {
              this.addProductToPurchase.patchValue({formato: formatItem.id}); 
            }

            this.formatList.push(formatItem);
            console.log(this.addProductToPurchase);
          }
        },
        (error) => {
          console.log(error);
        }
      )
      this.addProductToPurchase.patchValue({precio: ''});
    }
    else {
      console.log ('formato Seleccionado');
      console.log (event);
      let productDetail = this.productPurchase.productDetail.product.productDetails
        .find(p => (p.format != null && p.format.id == this.addProductToPurchase.get('formato').value) &&
              p.brand != null && p.brand.id == this.addProductToPurchase.get('marca').value);
      this.addProductToPurchase.patchValue({productDetail: productDetail, formato: event.target.value, precio: productDetail.lastPrice});
    }
  }
}
