import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-purchase',
  templateUrl: './add-purchase.component.html',
  styleUrls: ['./add-purchase.component.css']
})
export class AddPurchaseComponent implements OnInit {
  @Input() editar: string;

  constructor(
    public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    console.log("Add Purchase On Init");
  }

}
