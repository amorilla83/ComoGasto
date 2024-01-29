import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
import { NgIf } from '@angular/common';

@Component({
    selector: 'app-alert',
    templateUrl: './alert.component.html',
    styleUrls: ['./alert.component.css'],
    standalone: true,
    imports: [NgIf, NgbAlert]
})
export class AlertComponent implements OnInit {
  @Input() successMessage : string;
  @Input() errorMessage: string;

  @ViewChild('selfClosingAlert', {static: false}) selfClosingAlert: NgbAlert | undefined;
  constructor() { }

  ngOnInit(): void {
    setTimeout(() => this.selfClosingAlert?.close(), 3000);
    

    // this._success.subscribe(message => this.successMessage = message);
    // this._success.pipe(debounceTime(5000)).subscribe(() => {
    //   if (this.selfClosingAlert) {
    //     this.selfClosingAlert.close();
    //   }
    // });
  }

}
