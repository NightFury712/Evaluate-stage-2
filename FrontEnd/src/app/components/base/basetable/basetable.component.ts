import { Component, Input, OnInit } from '@angular/core';
import { faCheck } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-basetable',
  templateUrl: './basetable.component.html',
  styleUrls: ['./basetable.component.css']
})
export class BasetableComponent implements OnInit {
  @Input() ths: any;
  @Input() customers: any;
  faCheck = faCheck;

  constructor() { }
  
  /**
     * Thiết lập max-with, min-width cho các thành phần trong bảng
     * Author: HHDang (13/9/2021)
     */
   public sizeStyle(th: any): any{
    let style: any = {};
    if(th.style) {
      if(th.style.minWidth) {
        style["min-width"] = th.style.minWidth + 'px';
      }
      if(th.style.maxWidth) {
        style["max-width"] = th.style.maxWidth + 'px';
      }
    }
    return style;
  }

  ngOnInit(): void {
  }

}
