import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-basetextbox',
  templateUrl: './basetextbox.component.html',
  styleUrls: ['./basetextbox.component.css']
})
export class BasetextboxComponent implements OnInit {
  @Input() inputInfo: any;
  @Output() inputChange = new EventEmitter();
  inputRef: any;
  constructor() { }

  ngOnInit(): void {
  }

  /**
   * Hàm thực hiện emit giá trị ô input
   * @param event biến trạng thái chứa giá trị ô input
   * Author: HHDang (20/09/2021)
   */
  onInput(event: any) {
    this.inputChange.emit({
      id: this.inputInfo.id,
      value: event.target.value
    });
  }
  
}
