import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ExportRadioValue, Popup } from '../../../resources/MISAConst';
import { CustomerService } from 'src/app/services/api/components/customer/customer.service';
import { BaseService } from 'src/app/services/base/baseservice.service';

@Component({
  selector: 'app-baseexportpopup',
  templateUrl: './baseexportpopup.component.html',
  styleUrls: ['./baseexportpopup.component.css']
})
export class BaseexportpopupComponent implements OnInit {
  @Input() pageInfo: any;
  @Output() closeExportPopup = new EventEmitter();
  @Output() toggleSpinner = new EventEmitter();
  radioInfo: any = [
    {value: ExportRadioValue.All.value, title: ExportRadioValue.All.title, checked: true},
    {value: ExportRadioValue.Custom.value, title: ExportRadioValue.Custom.title, checked: false}
  ];
  inputInfo: any = {
    begin: {
      type: 'text',
      id: 'begin',
      maxLength: 10,
      placeholder: `(1-)`,
      flag: true,
      tooltipText: 'Vui lòng nhập vào giá trị là số!'
    },
    end: {
      type: 'text',
      id: 'end',
      maxLength: 10,
      placeholder: `(2-)`,
      flag: true,
      tooltipText: 'Vui lòng nhập vào giá trị là số!'
    }
  }
  popup: any = {
    icon: Popup.Error.icon,
    title: Popup.Error.title,
    isShow: false,
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
  exportIndex: any = {
    begin: 1,
    end: 0
  }
  exportCustomFlag: Boolean = false;
  

  constructor(private customerSerivce: CustomerService, private baseService: BaseService) { 
  }

  ngOnInit(): void {
    this.inputInfo.begin.placeholder = `(1-${this.pageInfo.totalRecord - 1})`;
    this.inputInfo.end.placeholder = `(2-${this.pageInfo.totalRecord})`;
    // Khởi tạo giá trị ban đầu cho trường chứa vị trí bản ghi xuất khẩu
    this.exportIndex.begin = 1;
    this.exportIndex.end = this.pageInfo.totalRecord;
  }

  /**
   * Hàm xử lý đóng popup khi nhấn nút đóng hoặc hủy
   * Author: HHDang (20/09/2021)
   */
  closeExportPopupClick() {
    this.closeExportPopup.emit()
  }

  onFormExportSubmit() {
    const begin = this.exportIndex.begin;
    const end = this.exportIndex.end;
    const totalRecord = this.pageInfo.totalRecord;
    const result = this.validate(begin, end, totalRecord);
    // Nếu trường nhập liệu ko hợp lệ:
    if(!result.flag) {
      // Gán thông điệp
      this.popup.title = result.messager
      // Hiển thị popup thông báo lỗi
      this.popup.isShow = true;
      return;
    } 
    this.toggleSpinner.emit(true);
    // Nếu trường nhập liệu là hợp lệ thì gọi api:
    this.customerSerivce.ExportCustomers(begin, end).subscribe((resonse) => {
      // Ẩn spinner
      this.toggleSpinner.emit(false);
      // Xử lý response trả về
      this.baseService.HandleResponseMessage(resonse);
      // Đóng popup
      this.closeExportPopup.emit()
    },
    (error) => {
      console.log(error.response);
      // Ẩn spinner
      this.toggleSpinner.emit(false);
      // Xử lý response trả về
      this.baseService.HandleResponseMessage(error.response);
      // Đóng popup
      this.closeExportPopup.emit()
    });
  }

  btnSavePopupInfo() {
    this.popup.isShow = false;
  }
  /**
   * Hàm xử lý hiển thị form nhập liệu khi người dùng chọn xuất theo lựa chọn
   * @param value Giá trị ô radio button
   * Author: HHDang (20/09/2021)
   */
  radioChange(value: any) {
    if(value === ExportRadioValue.Custom.value) {
      
      // Hiển thị form nhập liệu
      this.exportCustomFlag = true;
      // Khởi tạo giá trị ban đầu cho trường chứa vị trí bản ghi xuất khẩu
      this.exportIndex.begin = 0;
      this.exportIndex.end = 0;
      // Cập nhật lại cờ validate
      this.inputInfo.begin.flag = true;
      this.inputInfo.end.flag = true
    } else {
      // Ẩn form nhập liệu
      this.exportCustomFlag = false;
      // Khởi tạo giá trị cho trường chứa vị trí bản ghi xuất khẩu
      this.exportIndex.begin = 1;
      this.exportIndex.end = this.pageInfo.totalPage;
    }
  }
  /**
   * Hàm xử lý validate và cập nhật lại vị trí bản ghi người dùng nhập
   * @param info Thông tin ô input người dùng nhập
   * Author: HHDang (20/09/2021)
   */
  inputChange(info: any) {
    if(info.id === this.inputInfo.begin.id) {
      this.inputInfo.begin.flag = this.isNumbericOrEmpty(info.value);
      this.exportIndex.begin = info.value;
    }
    if(info.id === this.inputInfo.end.id) {
      this.inputInfo.end.flag = this.isNumbericOrEmpty(info.value);
      this.exportIndex.end = info.value;
    }
  }

  /**
   * Hàm thực hiện validate trước khi submit form
   * @param begin Giá trị bắt đầu
   * @param end Giá trị kết thúc
   * @param totalRecord Tổng số bản ghi
   * @returns Kết quả và thông điệp
   * Author: HHDang (22/09/2021)
   */
  private validate(begin: any, end: any, totalRecord: any) {
    if(!this.isNumbericOrEmpty(begin) || !this.isNumbericOrEmpty(end)) {
      return {
        flag: false,
        messager: `Vui lòng nhập vào giá trị là số và không được phép để trống!`
      };
    } 
    const beginValue = parseInt(begin);
    const endValue = parseInt(end);
    if( beginValue < 1 || beginValue > totalRecord - 1) {
      return {
        flag: false,
        messager: `Vui lòng nhập vào giá trị bắt đầu từ 1 đến ${totalRecord - 1}!`
      };
    }
    if( endValue < 2 || endValue > totalRecord) {
      return {
        flag: false,
        messager: `Vui lòng nhập vào giá trị kết thúc từ 2 đến ${totalRecord}!`
      };
    }
    if(endValue <= beginValue) {
      return {
        flag: false,
        messager: `Vui lòng nhập vào giá trị bắt đầu lớn hơn giá trị kết thúc!`
      };
    }
    return {
      flag: true,
      messager: `Không có lỗi xảy ra!`
    }
  }
  /**
   * validate các trường có phải là số hay không
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
