import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-basepopupinfo',
  templateUrl: './basepopupinfo.component.html',
  styleUrls: ['./basepopupinfo.component.css']
})
export class BasepopupinfoComponent implements OnInit {
  @Input() popup: any;
  @Output() btnSave = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }
  
  togglePopup() {

  }

  btnCloseClick() {

  }

  btnSaveClick() {
    this.btnSave.emit();
  }
}
