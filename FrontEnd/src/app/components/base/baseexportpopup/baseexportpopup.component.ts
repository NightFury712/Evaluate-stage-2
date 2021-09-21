import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ExportRadioValue, Popup } from '../../../resources/MISAConst'

@Component({
  selector: 'app-baseexportpopup',
  templateUrl: './baseexportpopup.component.html',
  styleUrls: ['./baseexportpopup.component.css']
})
export class BaseexportpopupComponent implements OnInit {
  @Input() popupInfoFlag!: Boolean;
  @Output() closeExportPopup = new EventEmitter();
  radioInfo: any = [
    {value: ExportRadioValue.All.value, title: ExportRadioValue.All.title, checked: true},
    {value: ExportRadioValue.Custom.value, title: ExportRadioValue.Custom.title, checked: false}
  ];
  inputInfo: any = {
    begin: {
      type: 'text',
      id: 'begin',
      maxLength: 10,
      placeholder: '(0-)',
      flag: true,
      tooltipText: 'Vui lòng nhập vào giá trị là số!'
    },
    end: {
      type: 'text',
      id: 'end',
      maxLength: 10,
      placeholder: '(0-)',
      flag: true,
      tooltipText: 'Vui lòng nhập vào giá trị là số!'
    }
  }
  popup: any = {
    icon: Popup.Error.icon,
    title: Popup.Error.title,
    center: true,
    btnContinue: {
      flag: false,
      title: "Hủy"
    },
    btnClose: {
      flag: false,
      title: "Không"
    },
    btnSave: {
      flag: true,
      title: "Đóng"
    },
  }
  exportCustomFlag: Boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  /**
   * Hàm xử lý đóng popup khi nhấn nút đóng hoặc hủy
   * Author: HHDang (20/09/2021)
   */
  closeExportPopupClick() {
    this.closeExportPopup.emit()
  }

  onFormExportSubmit() {

  }
  /**
   * Hàm xử lý hiển thị form nhập liệu khi người dùng chọn xuất theo lựa chọn
   * @param value Giá trị ô radio button
   * Author: HHDang (20/09/2021)
   */
  radioChange(value: any) {
    if(value === ExportRadioValue.Custom.value) {
      this.exportCustomFlag = true;
    } else {
      this.exportCustomFlag = false;
    }
  }
  /**
   * Hàm xử lý validate số lượng bản ghi người dùng nhập
   * @param info Thông tin ô input người dùng nhập
   * Author: HHDang (20/09/2021)
   */
  inputChange(info: any) {
    if(info.id === this.inputInfo.begin.id) {
      if(this.isNumbericOrEmpty(info.value)) {
        this.inputInfo.begin.flag = true;
      } else {
        this.inputInfo.begin.flag = false;
      }
    }
    if(info.id === this.inputInfo.end.id) {
      if(this.isNumbericOrEmpty(info.value)) {
        this.inputInfo.end.flag = true;
      } else {
        this.inputInfo.end.flag = false;
      }
    }
  }

  /**
   * Validate các trường có phải là số hay không
   * @param val Giá trị cần validate
   * @returns Kết quả (True/False)
   * Author: HHDang (20/09/2021)
   */
  isNumbericOrEmpty(val: any) {
    if(val === '') {
      return true;
    } 
    return /^\d+$/.test(val);
  }
}
