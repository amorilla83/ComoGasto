import { Component, OnInit, Input } from '@angular/core';
import { Store } from 'src/app/models/store';
import { StoreService } from '../../services/store.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DeleteModalComponent } from '../delete-modal/delete-modal.component';
import { AddStoreComponent } from '../add-store/add-store.component';
import { AddPurchaseComponent } from '../add-purchase/add-purchase.component';

@Component({
  selector: 'app-stores',
  templateUrl: './stores.component.html',
  styleUrls: ['./stores.component.css']
})
export class StoresComponent implements OnInit {
  @Input() public name : string
  listStores: Store[] = [];
  closeModal: string;

  constructor(private storeService: StoreService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.getStores();
  }

  triggerDeleteModal(store: Store) {
    console.log("Store in trigger: ");
    console.log(store);
    const modalRef = this.modalService.open(DeleteModalComponent, { ariaLabelledBy: 'modal-basic-title' });
    console.log(store.name);
    modalRef.componentInstance.name = store.name;
    modalRef.componentInstance.id = store.id;
    modalRef.componentInstance.type = "tienda";
      modalRef.result.then(
      (res) => {
        this.closeModal = `Closed with: ${res}`;
        if (res === 'Save click') {
          this.deleteStore(store);
        }
      }, (res) => {
        this.closeModal = `Dismissed ${this.getDismissReason(res)}`;
      });
  }

  openEditModal(store: Store, newStore: boolean) {
    console.log(store);
    console.log(newStore);
    const modalRef = this.modalService.open(AddPurchaseComponent, { ariaLabelledBy: 'modal-basic-title' });
    modalRef.componentInstance.editar = "Yes";
    modalRef.result.then(
      (res) => {
        this.closeModal = `Closed with: ${res}`;
        if (res === 'Save click') {
          this.deleteStore(store);
        }
      }, (res) => {
        this.closeModal = `Dismissed ${this.getDismissReason(res)}`;
      });
  }

  triggerEditModal(store: Store, newStore: boolean) {
    const modalRef = this.modalService.open(AddStoreComponent, { ariaLabelledBy: 'modal-basic-title' });
    modalRef.componentInstance.storeEdit = store;
    modalRef.componentInstance.newStore = newStore;
    //modalRef.result.then(
    //  (res) => {
    //    this.closeModal = `Closed with: ${res}`;
    //    if (res === 'Save click') {
    //      if (newStore) {
    //        this.addStore(store);
    //      }
    //      else {
    //        this.editStore(store);
    //      }
    //    }
    //  }, (res) => {
    //    this.closeModal = `Dismissed ${this.getDismissReason(res)}`;
    //  });
  }

  triggerAddModal() {
    this.triggerEditModal(undefined, true);
  }

  private getDismissReason(reason: any): string {
    if (reason == ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason == ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking a backdrop';
    } else {
      return `with: ${reason}`;
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

  deleteStore(store : Store) {
    this.storeService.deleteStore(store).subscribe(
      data => {
        console.log(data);
        this.getStores();
      },
      error => {
        console.log(error);
      });
  }

  editStore(store: Store) {
    this.storeService.editStore(store, store.id).subscribe(
      data => {
        console.log(data);
        this.getStores();
      },
      error => {
        console.log(error);
      });
  }

  addStore(store: Store) {
    this.storeService.addStore(store).subscribe(
      data => {
        console.log(data);
        this.getStores();
      },
      error => {
        console.log(error);
      });
  }

}
