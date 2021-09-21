import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-basepagination',
  templateUrl: './basepagination.component.html',
  styleUrls: ['./basepagination.component.css']
})
export class BasepaginationComponent implements OnInit {
  @Input() pageInfomation: any;
  @Input() pagingArray: any;
  @Output() radioValue = new EventEmitter<any>();
  oldRadioInput: any = 0;
  radioInput: any;

  constructor() { }
  
  /**
     * Hàm xử lý khi nhấn nút 'Trước' (Trang hiện tại - 1)
     * Author: HHDang (18/08/2021)
     */
   movePre() {
    this.radioInput = parseInt(this.radioInput) - 1;
  }
  /**
   * Hàm xử lý khi nhấn nút 'Sau' (Trang hiện tại + 1)
   * Author: HHDang (18/08/2021)
   */
  moveNext() {
    this.radioInput = parseInt(this.radioInput) + 1;
  }

  choose(e: any) {
    this.radioInput = parseInt(e.target.value);
  }

  ngOnInit(): void {
    this.radioInput = 1;
  }

  ngDoCheck() {
    if(this.oldRadioInput !== this.radioInput) {
      this.oldRadioInput = this.radioInput;
      this.radioValue.emit(this.radioInput - 1);
    }
  }
}
