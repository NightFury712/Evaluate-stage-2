import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-basepopupinfo',
  templateUrl: './basepopupinfo.component.html',
  styleUrls: ['./basepopupinfo.component.css']
})
export class BasepopupinfoComponent implements OnInit {
  @Input() popup: any;

  constructor() { }

  ngOnInit(): void {
  }
  
  togglePopup() {

  }

  btnCloseClick() {

  }

  btnSaveClick() {
    
  }
}
