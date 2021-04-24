import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from 'src/app/models/store';
import { StoreService } from '../../services/store.service';

@Component({
  selector: 'app-add-store',
  templateUrl: './add-store.component.html',
  styleUrls: ['./add-store.component.css']
})
export class AddStoreComponent implements OnInit {
  @Input() public storeEdit: Store;
  @Input() public newStore: boolean = true;


  addStore: FormGroup;
  title: string;

  constructor(
    public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private storeService: StoreService,
    private router: Router) {
  }

  ngOnInit(): void {
    console.log("newStore: " + this.newStore);
    console.log("StoreEdit: " + this.storeEdit);
    if (this.newStore) {
      this.title = "Añadir tienda";
    }
    else {
      this.title = "Editar tienda";
    }
    this.addStore = this.fb.group(
      {
        id: [''],
        name: ['', Validators.required],
        logo: ['', Validators.required],
        image: [null]
      });
    if (this.storeEdit != undefined) {
      this.addStore.setValue({
        id: this.storeEdit.id,
        name: this.storeEdit.name,
        image: this.storeEdit.image,
        logo: ''
      });
    }
  }

  add() {
    console.log(this.addStore);

    var formData: any = new FormData();
    formData.append("name", this.addStore.get('name').value);
    formData.append("logo", this.addStore.get('logo').value);

    if (this.newStore) {
      this.storeService.addStore(formData).subscribe(
        (response) => console.log(response),
        (error) => console.log(error));
    }
    else {
      formData.append("id", this.storeEdit.id);
      this.storeService.editStore(formData, this.storeEdit.id).subscribe(
        (response) => console.log(response),
        (error) => console.log(error));
    }

    //const store: Store = {
    //  name: this.addStore.get('name').value,
    //  logo: this.addStore.get('logo').value,
    //  image: this.addStore.get('image').value
    //}
    this.activeModal.close();
    console.log(formData);

    this.router.navigate(['/stores']);

  }

  onFileChange(event) {

    console.log("Dato al entrar: ");
    console.log(this.addStore);

    //Carga la imagen que ya está en el objeto retornada del backend
    //if (this.addStore.get('image').value != undefined) {
      //const file = (event.target as HTMLInputElement).files[0];
      //this.addStore.patchValue({
        //image: file
      //});
    //}
    //else {
      //Carga la imagen en función del input
      const reader = new FileReader();

      if (event.target.files && event.target.files.length) {
        const [file] = event.target.files;
        reader.readAsDataURL(file);

        reader.onload = () => {
          this.addStore.patchValue({
            image: reader.result
          });
        }
      }
    //}
    console.log("Dato al salir: ");
    console.log(this.addStore);
    //const reader = new FileReader();

    //if (event.target.files && event.target.files.length) {
    //  const [file] = event.target.files;
    //  reader.readAsDataURL(file);

    //  reader.onload = () => {
    //    this.imgFile = reader.result as string;
    //    this.addStore.patchValue({
    //      image: reader.result
    //    });
    //  };
    //}
  }

}
