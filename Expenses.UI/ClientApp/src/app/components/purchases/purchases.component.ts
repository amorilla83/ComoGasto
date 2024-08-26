import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { FilterPurchase } from 'src/app/models/filterPurchase';
import { Purchase } from 'src/app/models/purchase';
import { Store } from 'src/app/models/store';
import { DetailService } from 'src/app/services/detail.service';
import { PurchaseService } from 'src/app/services/purchase.service';
import { StoreService } from 'src/app/services/store.service';
import { NgFor, NgIf, SlicePipe, DecimalPipe, DatePipe } from '@angular/common';
import { NgbDate, NgbInputDatepicker, NgbPagination } from '@ng-bootstrap/ng-bootstrap';
import { AlertComponent } from '../alert/alert.component';

@Component({
    selector: 'app-purchases',
    templateUrl: './purchases.component.html',
    styleUrls: ['./purchases.component.css'],
    standalone: true,
    imports: [AlertComponent, FormsModule, ReactiveFormsModule, NgbInputDatepicker, NgFor, NgIf, NgbPagination, SlicePipe, DecimalPipe, DatePipe]
})
export class PurchasesComponent implements OnInit {

  filterPurchases: Purchase[] = [];
  totalPurchases: Purchase[] = [];
  listStores: Store[] = [];
  errorMessage : string;
  successMessage: string;
  filterData: UntypedFormGroup;
  currentPage: number = 1;
  response: boolean = false;

  constructor(private purchaseService: PurchaseService, private storeService: StoreService,
    private fb: UntypedFormBuilder, private router: Router) { 
    
  }

  ngOnInit(): void {
    this.filterData = this.fb.group(
      {
        dateStart:[],
        dateEnd: [],
        storeId: [''],
        //maxPrice: ['0'],
        //minPrice: ['0'],
        //priceRange: ['0']
      });
    this.getStores();
    this.getPurchases ();

    //this.filterData.patchValue({maxPrice: Math.max(...this.totalPurchases.map(d => d.total))});
    //this.filterData.patchValue({minPrice: Math.min(...this.totalPurchases.map(d => d.total))});
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

  getPurchases() {
    this.purchaseService.getPurchases().subscribe(
      data => {
        console.log(data);
        this.response = true;
        this.filterPurchases = data;
        this.totalPurchases = data;
      },
      error => {
        console.log(error);
        this.response = true;
        this.errorMessage ="Error al obtener el listado de compras";
      }
    );
    //TODO: Marcar los días del calendario en los que haya compras
  }

  deletePurchase (id : number) {
    this.purchaseService.deletePurchase(id).subscribe(
      data => {
        console.log(data);
        this.successMessage = "Compra eliminada";
        let index = this.filterPurchases.findIndex(p => p.idPurchase == id);
        this.filterPurchases.splice(index, 1);
      },
      error => {
        console.log(error);
        this.errorMessage = "Error al eliminar la compra";
      }
    );
  }

  editPurchase (idPurchase: number) {
    this.purchaseService.changePurchase(idPurchase);
    this.router.navigate(['/purchase', idPurchase]);
  }

  filter () {
    console.log(this.filterData);
    console.log(this.totalPurchases.length);
    let storeId = this.filterData.get('storeId').value;
    let dateStart : NgbDate = this.filterData.get('dateStart').value;
    let dateEnd = this.filterData.get('dateEnd').value;
    let filter = false;
    this.filterPurchases = this.totalPurchases;
    if (storeId != "" && storeId != null)
    {
      console.log ("Se filtra por tienda");
      this.filterPurchases = this.totalPurchases.filter(p => p.store.id == storeId);
      console.log(this.filterPurchases.length);
      filter = true;
    }
    
    if (dateStart != null && dateStart != undefined)
    {
      console.log("Filtramos por fecha de inicio");
      console.log(dateStart);
      this.filterPurchases = this.filterPurchases.filter(p => 
      {
        let datepicker = new Date(dateStart.year, dateStart.month - 1, dateStart.day);        
        return new Date (p.date) > datepicker;
      });
      console.log(this.filterPurchases.length);
      filter = true;
    }

    if (dateEnd != null && dateEnd != undefined)
    {
      console.log("Filtramos por fecha de fin");
      this.filterPurchases = this.filterPurchases.filter(p => 
        {
          let datepicker = new Date(dateEnd.year, dateEnd.month - 1, dateEnd.day);        
          return new Date (p.date) < datepicker;
        });
      console.log(this.filterPurchases.length);
      filter = true;
    }
    
    if (!filter) 
    {
      this.filterPurchases = this.totalPurchases;
    }
    
    //this.purchaseService.setPurchaseList(this.filterPurchases);
  }

  limpiar () {
    console.log (this.totalPurchases.length);
    this.filterPurchases = this.totalPurchases;
    //this.purchaseService.setPurchaseList(this.filterPurchases);
    this.filterData.patchValue({storeId: ''});
    this.filterData.patchValue({dateStart: null});
    this.filterData.patchValue({dateEnd: null});
  }

  onPageChange(newPage: number): void {
    this.currentPage = newPage;
    window.scrollTo(0, 0);
  }
}
