import { Component,EventEmitter,Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-basebutton',
  templateUrl: './basebutton.component.html',
  styleUrls: ['./basebutton.component.css']
})
export class BasebuttonComponent implements OnInit {
  @Input() btnStyle: string = '';
  @Input() text: string = 'Nhập';
  @Input() icon: string = 'fa fa-check';
  @Input() tooltipInfo: any; 
  @Output() btnClick = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  onClick() {
    this.btnClick.emit();
  }
}
