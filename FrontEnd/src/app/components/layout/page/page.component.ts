import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/entities/Customer';
import { CustomerService } from 'src/app/services/api/components/customer/customer.service';
import { CustomerFieldName } from '../../../resources/MISAConst'

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.css']
})
export class PageComponent implements OnInit {
  //#region DECLARE
  public ths: any = [
    { fieldName: "CustomerCode", content: CustomerFieldName.CustomerCode, style: { minWidth: 150}},
    { fieldName: "CustomerName", content: CustomerFieldName.CustomerName, style: { minWidth: 250} },
    { fieldName: "PhoneNumber", content: CustomerFieldName.PhoneNumber, style: { minWidth: 150}},
    { fieldName: "CompanyPhoneNumber", content: CustomerFieldName.CompanyPhoneNumber, style: { minWidth: 150} },
    { fieldName: "PersonalTaxCode", content: CustomerFieldName.PersonalTaxCode, style: { minWidth: 200}},
    { fieldName: "Country", content: CustomerFieldName.Country, style: { minWidth: 200}},
    { fieldName: "Province", content: CustomerFieldName.Province, style: { minWidth: 200}},
    { fieldName: "District", content: CustomerFieldName.District, style: { minWidth: 200}},
    { fieldName: "Ward", content: CustomerFieldName.Ward, style: { minWidth: 200}},
  ]
  public customers: Customer[] = [];
  public pageInfo: any = {
    pageIndex: 0, 
    customerFilter: '', 
    pageSize: 20
  }
  public pagingArray: any = [];
  importPopupFlag: Boolean = false;
  exportPopupFlag: Boolean = false;
  spinnerFlag: Boolean = false;
  //#endregion

  constructor( private customerService: CustomerService) {}

  /**
   * Hàm xử lý khi nhấn nút nhập file
   * Author: HHDang (14/09/2021)
   */
  btnImportClick() {
    this.importPopupFlag = true;
  }

  /**
   * Hàm xử lý khi nhấn nút xuất file
   * Author: HHDang (14/09/2021)
   */
  btnExportClick() {
    this.exportPopupFlag = true;
  }
  /**
   * Hàm xử lý sự kiện khi nhấn nút đóng import popup
   * Author: HHDang (14/09/2021)
   */
  closeImportPopup() {
    this.importPopupFlag = false;
  }
  /**
   * Hàm xử lý sự kiện khi nhấn nút đóng export popup
   * Author: HHDang (14/09/2021)
   */
  closeExportPopup() {
    this.exportPopupFlag = false;
  }
  /**
   * Hàm xử lý ẩn/hiện spinner
   * @param flag cờ trạng trái
   * Author: HHDang (22/09/2021)
   */
  toggleSpinner(flag: Boolean) {
    if(flag === true) {
      this.spinnerFlag = true;
    } else {
      this.spinnerFlag = false;
    }
  }
  /**
   * Hàm xử lý upload data lên server
   * @param importData Thông tin về file
   * Author: (15/09/2021)
   */
  async submitFormData(customers: any) {
    this.spinnerFlag = true;
    const response = await this.customerService.UploadImportFile(customers);
    this.spinnerFlag = false;

    // Đóng popup 
    if(response.MISACode === 200) {
      this.importPopupFlag = false;
    }
  }
  
  /**
   * Hàm xử lý call api khi chuyển trang
   * @param currentPage trang hiện tại
   * Author: HHDang (13/09/2021)
   */
  async radioChange(currentPage: any) {
    // Cập nhật trang hiện tại
    this.pageInfo.pageIndex = currentPage;
    // Lấy danh sách khách hàng ở trang hiện tại
    await this.getCustomers();
    // Khởi tạo lại mảng số trang
    this.pagingArray = this.initPagingArray(this.pageInfo);
  }
  /**
   * Lấy thông tin danh sách khách hàng
   * Author:HHDang (13/09/2021)
   */
  public async getCustomers() {
    // Hiển thị spinner
    this.spinnerFlag = true;
    // Call api lấy dữ liệu
    const data: any = await this.customerService.GetFilterCustomer(this.pageInfo)
    // Ẩn spinner
    this.spinnerFlag = false;
    // Lấy thông tin tổng số trang
    this.pageInfo.totalPage = data.TotalPage;
    // Lấy thông tin tổng số bản ghi
    this.pageInfo.totalRecord = data.TotalRecord;
    // Lấy thông tin danh sách khách hàng
    this.customers = data.Data;
  }
  /**
     * Hàm khởi tạo mảng số trang
     * @param {Object} pageInfo Thông tin phân trang 
     * @returns Mảng số trang
     * Author: HHDang (13/09/2021)
     */
   initPagingArray(pageInfo: any): any {
    let arr = [];
    // Gán thông tin tổng số trang
    let totalPage = pageInfo.totalPage;
    // Gán thông tin trang hiện tại
    let pageIndex = pageInfo.pageIndex
    // Nếu tổng số trang nhỏ hơn hoặc bằng 5
    if(totalPage <= 5) {
      // Hiển thị 5 trang đầu
      for(let i = 0; i < totalPage; i++) {
        arr.push(i);
      }
      return arr;
    }
    // Nếu tổng số trang lớn hơn 8 và trang hiện tại < 3
    // Hiển thị thêm dấu ... (vị trí -1)
    if(totalPage == 6 && pageIndex < 3) {
      arr = [0, 1, 2, 3, -1, 5];
      return arr;
    } 
    // Nếu tổng số trang bằng 6 và trang hiện tại > 3
    // Hiển thị thêm dấu ... (vị trí -1)
    if(totalPage == 6 && pageIndex >= 3) {
      arr = [0, -1, 2, 3, 4, 5];
      return arr;
    }
    // Nếu tổng số trang lớn hơn 6 và trang hiện tại nhỏ hơn 3
    if(pageIndex < 3) {
      arr = [0, 1, 2, 3, -1, totalPage - 1];
      return arr;
    }
    // Nếu tổng số trang lớn hơn 6 và trang hiện tại nhỏ hơn tổng số trang - 3
    if(pageIndex >= 3 && (pageIndex < totalPage - 3)) {
      arr = [0, -1, pageIndex - 1, pageIndex, pageIndex + 1, -1, totalPage - 1];
      return arr;
    }
    // Nếu tổng số trang lớn hơn 6 và trang hiện tại lớn hơn bằng tổng số trang - 3
    if(pageIndex >= totalPage - 3) {
      arr = [0, -1, totalPage - 4, totalPage - 3, totalPage -2, totalPage - 1];
      return arr;
    }
  }
  
  async ngOnInit() {
    await this.getCustomers();
    this.pagingArray = this.initPagingArray(this.pageInfo);
  };
}
