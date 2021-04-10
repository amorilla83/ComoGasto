import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from 'src/app/models/store';
import { StoreService } from '../../services/store.service';

@Component({
  selector: 'app-add-store',
  templateUrl: './add-store.component.html',
  styleUrls: ['./add-store.component.css']
})
export class AddStoreComponent implements OnInit {
  addStore: FormGroup

  constructor(private fb: FormBuilder, private storeService: StoreService, private router: Router) {
    this.addStore = this.fb.group(
      {
        name: ['', Validators.required],
        logo: ['', Validators.required],
        image: [null]
      });  
  }

  ngOnInit(): void {
  }

  add() {
    console.log(this.addStore);

    var formData: any = new FormData();
    formData.append("name", this.addStore.get('name').value);
    formData.append("logo", this.addStore.get('image').value);

    this.storeService.addStore(formData).subscribe(
      (response) => console.log(response),
      (error) => console.log(error));

    //const store: Store = {
    //  name: this.addStore.get('name').value,
    //  logo: this.addStore.get('logo').value,
    //  image: this.addStore.get('image').value
    //}

    console.log(formData);

    this.router.navigate(['/stores']);

  }

  onFileChange(event) {
    const file = (event.target as HTMLInputElement).files[0];
    this.addStore.patchValue({
      image: file
    });

    //const reader = new FileReader();

    //if (event.target.files && event.target.files.length) {
    //  const [file] = event.target.files;
    //  reader.readAsDataURL(file);

    //  reader.onload = () => {
    //    this.addStore.patchValue({
    //      image: reader.result
    //    });
    //  };
    //}
  }

}
