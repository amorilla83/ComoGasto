import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Brand } from 'src/app/models/brand';
import { Item } from 'src/app/models/item';
import { Product } from 'src/app/models/product';
import { ProductPurchase } from 'src/app/models/productPurchase';
import { BrandService } from 'src/app/services/brand.service';
import { FormatService } from 'src/app/services/format.service';
import { ProductService } from 'src/app/services/product.service';
import { AddItemComponent } from '../add-item/add-item.component';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  @Input() product: Product;
  addProductToPurchase: FormGroup;
  id = 0;
  brandList: Brand[] = [];

  formatList: Item[] = [];

  constructor(private fb:FormBuilder,
    private productService: ProductService,
    private brandService: BrandService,
    private formatService: FormatService,
    public activeModal: NgbActiveModal,
    private modalService: NgbModal) { 
    this.addProductToPurchase = this.fb.group (
      {
        marca: [{value: '0', disabled: false}],
        formato: [{value: '0', disabled: false}],
        precio: ['', Validators.required],
        unidades: ['1'],
        peso: [''],
        granel: false
      }
    )
  }

  ngOnInit(): void {
    this.getBrands ();  
  }

  getBrands ()
  {
    this.productService.getBrandsByProduct(this.product.id).subscribe(
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
    if (event.target.checked)
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
    let productPurchase : ProductPurchase =
    {
      
      productId : this.product.id,
      product : '',
      brandId : this.addProductToPurchase.get('marca').value,
      brand : brand?.name,
      formatId : this.addProductToPurchase.get('formato').value,
      format: this.formatList.find(f => f.id == this.addProductToPurchase.get('formato').value).name,
      quantity : this.addProductToPurchase.get('unidades').value,
      price : this.addProductToPurchase.get('precio').value,
      weight: this.addProductToPurchase.get('peso').value
    }

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
      //Abrimos el modal de add-item
      const modalRef = this.modalService.open(AddItemComponent, {ariaLabelledBy: 'modal-basic-title'});
      modalRef.componentInstance.typeItem = 'marca';
      modalRef.result.then(
        (res) => {
          //Añadir marca a la lista
          console.log(res);
          if (!res.includes('click')){
            let brandItem : Brand = { name: res, formatList: null, productId: this.product.id};
            this.brandService.addBrand(brandItem).subscribe(
              (response) => 
              {
                console.log("Tenemos la respuesta");
                console.log(response);
                console.log(response.id);
                this.brandList.push(response);
                this.formatList = response.formatList;

                console.log("Nueva marca añadida a la lista");

                this.addProductToPurchase.patchValue({marca: response.id});
              },
              (error) => console.log(error));
          }
        },
        (error) => {
          console.log(error);
        }
      )
    }
    else {
      console.log ('Marca seleccionada ');
      console.log(event);
      this.addProductToPurchase.patchValue({marca: event.target.value});
      this.formatList = this.brandList.find(b => b.id == this.addProductToPurchase.get('marca').value).formatList;
      console.log(this.formatList);
    }
  }

  onFormatSelected(event) { 

    let brand : number = this.addProductToPurchase.get('marca').value

    if (brand == undefined)
    {
      alert("Se debe seleccionar una marca antes de añadir el formato");
      return;
    }
    if (event.target.value == -1) 
    {
      //Abrimos el modal de add-item
      const modalRef = this.modalService.open(AddItemComponent, {ariaLabelledBy: 'modal-basic-title'});
      modalRef.componentInstance.typeItem = 'formato';
      modalRef.result.then(
        (res) => {
          //Añadir formato a la lista
          console.log(res);
          if (!res.includes('click')){
            //TODO: Añadir el formato a la base de datos y recargar la lista
            let formatItem : Item = { name: res, parentId: brand};
            this.formatService.addFormat(formatItem).subscribe(
              (response) => 
              {
                console.log("Tenemos la respuesta");
                console.log(response);
                console.log(response.id);
                this.formatList.push(response);

                console.log("Nuevo formato añadida a la lista");

                this.addProductToPurchase.patchValue({formato: response.id});
              },
              (error) => console.log(error));
            console.log("Nuevo formato añadido a la lista");
          }
        },
        (error) => {
          console.log(error);
        }
      )
    }
    else {
      console.log ('formato Seleccionado');
      console.log (event);
      this.addProductToPurchase.patchValue({formato: event.target.value});
    }
  }
}
