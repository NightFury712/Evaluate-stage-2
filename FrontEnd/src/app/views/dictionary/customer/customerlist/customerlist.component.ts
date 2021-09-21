import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-customerlist',
  templateUrl: './customerlist.component.html',
  styleUrls: ['./customerlist.component.css']
})
export class CustomerlistComponent implements OnInit {
  @Input() index: Number = 0;
  @Input() ths: any;
  
  constructor() { }

  ngOnInit(): void {
  }

}
