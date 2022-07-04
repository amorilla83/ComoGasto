import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Item } from 'src/app/models/item';
import { BrandService } from 'src/app/services/brand.service';
import { FormatService } from 'src/app/services/format.service';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css']
})
export class AddItemComponent implements OnInit {
  @ViewChild ('inputName') inputNameElement : ElementRef
  @Input() public typeItem: string;
  addName: string;
  selectedItem: Item;
  listItem: Item[] = [];

  constructor(public activeModal: NgbActiveModal, private brandService: BrandService,
    private formatService: FormatService) {
    
   }


  ngOnInit(): void {
    //Cargar toda la lista de base de datos
    if (this.typeItem == 'marca')
    {
      this.brandService.getBrands().subscribe(
        data => {
          console.log(data);
          this.listItem = data;
        },
        error => {
          console.log(error);
        });
    }
    else if (this.typeItem == 'formato')
    {
      this.formatService.getFormats().subscribe(
        data => {
          console.log(data);
          this.listItem = data;
        },
        error => {
          console.log(error);
        });
    }
  }

  ngAfterViewInit():void {

    this.inputNameElement.nativeElement.focus();
  }

  cancel() {
    this.activeModal.close(null);
  }

  save() {
    if (this.selectedItem == undefined)
    {
      //No se ha seleccionado ningún valor, se añade una nueva item con el texto
      this.selectedItem= {id: 0, name: this.addName};
    }

    this.activeModal.close(this.selectedItem);
  }

  dismiss() {
    this.activeModal.close(null);
  }

  onItemSelected (event) {
    this.selectedItem = this.listItem.find(i => i.id == event.target.value);
    this.addName = this.selectedItem.name;
  }

  selectItem (event)
  {
    console.log(event);
    if (this.selectedItem == undefined)
    {
    this.selectedItem = this.listItem.find(s => s.name == event.target.value);
    console.log(this.selectedItem);
    }
    else 
    {
      this.save();
    }
  }
}
