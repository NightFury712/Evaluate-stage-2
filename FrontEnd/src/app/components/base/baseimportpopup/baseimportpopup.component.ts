import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Observable, Subscriber } from 'rxjs';
import { CustomerFieldName } from 'src/app/resources/MISAConst';
import { ImportfileService } from 'src/app/services/readfile/importfile.service';

@Component({
  selector: 'app-baseimportpopup',
  templateUrl: './baseimportpopup.component.html',
  styleUrls: ['./baseimportpopup.component.css']
})
export class BaseimportpopupComponent implements OnInit {
  //#region DECLARE
  @Output() closeImportPopup = new EventEmitter();
  @Output() submitFormData = new EventEmitter();
  file: any = null;
  importField: any;
  fieldSelectedAll: Boolean = false;
  fieldSelected: Boolean[] = [];
  //#endregion

  //#region CONSTRUCTOR
  constructor(private importfileService: ImportfileService) {
    this.importField = this.initImportField();
    this.fieldSelected = new Array(this.importField.length);
    this.fieldSelected.fill(false);
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
      importFieldArr.push({
        fieldName: fieldName,
        displayName: CustomerFieldName[fieldName],
        flag: false
      })
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
    if (!this.file) {
      alert('Please fill valid details!');
      return;
    }
    // Thực hiện đọc file 
    const excelObservable = new Observable((subscriber: Subscriber<any>) => {
      this.importfileService.ReadFile(this.file, subscriber);
    })

    excelObservable.subscribe((datas) => {
      let customers: any = [];
      datas.forEach((data: any) => {
        let customer: any = {};
        this.importField.forEach((item: any) => {
          if(item.flag) {
            customer[item.fieldName] = data[item.displayName] === undefined ? null : String(data[item.displayName])
          }
        })
        customers.push(customer);
      })
      console.log(customers);
      // this.submitFormData.emit(customers);
    })
    
    // this.submitFormData.emit({
    //   fileImport: this.file,
    //   importField: this.importField
    // });
  }

  ngOnInit(): void {
  }
  //#endregion

}
