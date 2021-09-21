import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-baseradiobtn',
  templateUrl: './baseradiobtn.component.html',
  styleUrls: ['./baseradiobtn.component.css']
})
export class BaseradiobtnComponent implements OnInit {
  @Input() radioInfo: any;
  @Output() radioChange = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }
  /**
   * Lấy giá trị selection khi người dùng lựa chọn
   * @param event biến trạng thái chứa giá trị của radio button'
   * Author: (20/09/2021)
   */
  radioInput(event: any) {
    this.radioChange.emit(event.target.value);
  }
}
