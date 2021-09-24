import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Observable, Subscriber } from 'rxjs';
import { CustomerFieldName, Popup } from 'src/app/resources/MISAConst';
import { CustomerService } from 'src/app/services/api/components/customer/customer.service';
import { BaseService } from 'src/app/services/base/baseservice.service';
import { ImportfileService } from 'src/app/services/readfile/importfile.service';

@Component({
  selector: 'app-baseimportpopup',
  templateUrl: './baseimportpopup.component.html',
  styleUrls: ['./baseimportpopup.component.css']
})
export class BaseimportpopupComponent implements OnInit {
  //#region DECLARE
  @Output() closeImportPopup = new EventEmitter();
  @Output() reloadData = new EventEmitter();
  @Output() toggleSpinner = new EventEmitter();
  file: any = null;
  importField: any;
  fieldSelectedAll: Boolean = false;
  fieldSelected: Boolean[] = [];
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
  //#endregion

  //#region CONSTRUCTOR
  constructor(
      private importfileService: ImportfileService,
      private customerService: CustomerService,
      private baseService: BaseService
    ) 
    {
    this.importField = this.initImportField();
  }
  //#endregion

  //#region METHOD
  /**
   * Khởi tạo mảng chứa thông tin các trường nhập khẩu
   * @returns mảng thông tin các trường nhập khẩu
   * Author: HHDang (15/09/2021)
   */
  initImportField() {
    let fieldNames = Object.keys(CustomerFieldName);
    let importFieldArr = [];
    for(let fieldName of fieldNames) {
      if(fieldName === 'CustomerId' || fieldName === 'CustomerCode') {
        // Do nothing
      } else {
        importFieldArr.push({
          fieldName: fieldName,
          displayName: CustomerFieldName[fieldName],
          flag: false,
        })
      }
    }
    return importFieldArr;
  }
  /**
   * Hàm xử lý sự kiện khi đóng popup
   * Author: HHDang (15/09/2021)
   */
  closeImportPopupClick() {
    this.closeImportPopup.emit();
  }

  btnSavePopupInfo() {
    this.popup.isShow = false;
  }
  /**
   * Hàm lấy thông tin tệp nhập khẩu
   * @param event Thông tin tệp nhập khẩu
   * Author: HHDang (15/09/2021)
   */
  fileInputChange(event: any) {
    // Lấy thông tin tệp nhập khẩu
    const file = event.target.files[0];
    this.file = file;
  }
  /**
   * Hàm xử lý khi người dùng lựa chọn tất cả tất cả các trường
   * Author: HHDang (15/09/2021)
   */
  selectedAll() {
    if(this.fieldSelectedAll) {
      this.importField = this.importField.map((item: any) => {
        return {
          ...item,
          flag: true
        }
      })
    } else {
      this.importField = this.importField.map((item: any) => {
        return {
          ...item,
          flag: false
        }
      })
    }
  }
  /**
   * Hàm xử lý khi upload tệp nhập khẩu
   * Author: HHDang (15/09/2021)
   */
  onFormSubmit() {
    if(this.validate()) {
      // Lấy danh sách các trường nhập khẩu
      const importFieldArr = this.addFieldRequired();
      // Thực hiện đọc file 
      const excelObservable = new Observable((subscriber: Subscriber<any>) => {
        this.importfileService.ReadFile(this.file, subscriber);
      })
      // Lấy danh sách khách hàng từ file và thực hiện nhập khẩu
      excelObservable.subscribe((datas) => {
        let customers: any = [];
        datas.forEach((data: any) => {
          let customer: any = {};
          importFieldArr.forEach((item: any) => {
            if(item.flag) {
              customer[item.fieldName] = data[item.displayName] === undefined ? null : String(data[item.displayName].trim())
            }
          })
          customers.push(customer);
        })
        // Call api nhập khẩu
        this.uploadData(customers);
      })
    }
  }

  private validate(): Boolean {
    if(!this.file) {
      this.popup.title = 'Vui lòng chọn file nhập khẩu!'
      this.popup.isShow = true;
      return false;
    }
    if(this.importField.findIndex((item: any) => item.flag === true) === -1) {
      this.popup.title = 'Vui lòng chọn ít nhất 1 trường nhập khẩu!'
      this.popup.isShow = true;
      return false;
    }
    return true;
  }
  /**
   * Thực hiện call api nhập khẩu
   * @param customers Danh sách khách hàng
   * Author: HHDang (24/09/2021)
   */
  private uploadData(customers: any) {
    // this.spinnerFlag = true;
    this.toggleSpinner.emit(true);
    this.customerService.UploadImportFile(customers).subscribe((response) => {
      // Hiển thị thông báo:
      const result: any = this.baseService.HandleResponseMessage(response);
      if(result.flag) {
        // Ẩn spinner
        this.toggleSpinner.emit(false);
        // Đóng popup
        this.closeImportPopup.emit();
        // Load lại dữ liệu
        this.reloadData.emit();
      } else {
        // Ẩn spinner
        this.toggleSpinner.emit(false);
        // Hiển thị thông báo lỗi
        this.popup.title = result.messenger;
        this.popup.isShow = true;
      }
    },
    (error) => {
      console.log(error);
      // Ẩn spinner
      this.toggleSpinner.emit(false);
      // Đóng popup
      this.closeImportPopup.emit();
      // Xử lý hiển thị thông báo:
      this.baseService.HandleResponseMessage(error.response);
      this.file = null;
    })
  }

  /**
   * Thêm các trường bắt buộc vào danh sách
   * @returns Danh sách các trường nhập khẩu
   * Author: HHDang (24/09/2021)
   */
  private addFieldRequired() {
    let importFieldArr = JSON.parse(JSON.stringify(this.importField));
    importFieldArr.push({
      fieldName: 'CustomerId',
      displayName: CustomerFieldName['CustomerId'],
      flag: true,
    })
    importFieldArr.push({
      fieldName: 'CustomerCode',
      displayName: CustomerFieldName['CustomerCode'],
      flag: true,
    })
    return importFieldArr;
  }

  ngOnInit(): void {
  }
  //#endregion

}
