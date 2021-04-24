import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-delete-modal',
  templateUrl: './delete-modal.component.html',
  styleUrls: ['./delete-modal.component.css']
})
export class DeleteModalComponent implements OnInit {
  @Input() public name: string;
  @Input() public type: string;
  @Input() public id: string;

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  cancel() {
    this.activeModal.close('Cancel click');
  }

  delete() {
    this.activeModal.close('Save click');
  }

  dismiss() {
    this.activeModal.close('Cross click');
  }

}
