import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css']
})
export class AddItemComponent implements OnInit {
  @Input() public typeItem: string;
  addName: string;

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  cancel() {
    this.activeModal.close('Cancel click');
  }

  save() {
    this.activeModal.close(this.addName);
  }

  dismiss() {
    this.activeModal.close('Cross click');
  }
}
