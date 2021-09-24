import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators'
import { Customer } from '../../../../entities/Customer';
import { APIConfig } from '../../config/apiconfig';

@Injectable({
  providedIn: 'root'
})

export class CustomerService {
  private apiUrl = `${APIConfig.development}/api/v1`

  constructor(private http:HttpClient) { }

  /**
   * Thực hiện lấy tất cả dữ liệu khách hàng
   * @returns Danh sách khách hàng
   * Author: HHDang (21/09/2021)
   */
  public GetCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.apiUrl);
  } 

  /**
   * Thực hiện lấy danh sách khách hàng có phân trang
   * @param pageInfo Thông tin phân trang
   * @returns Danh sách khách hàng
   * Author: HHDang (24/09/2021)
   */
  public GetFilterCustomer(pageInfo: any): Observable<any> {
    let pageIndex = pageInfo.pageIndex;
    let pageSize = pageInfo.pageSize;
    let customerFilter = pageInfo.customerFilter;
    let res;
    return this.http
      .get<any>(`${this.apiUrl}/Customers/customerFilter?pageIndex=${pageIndex}&customerFilter=${customerFilter}&pageSize=${pageSize}`)
      .pipe(
        catchError(error => {
          return throwError(error);
        })
      );
  }
  /**
   * Thực hiện nhập khẩu dữ liệu khách hàng
   * @param customers Danh sách khách hàng nhập khẩu
   * @returns Thông điệp và kết quả
   * Author: HHDang (24/09/2021)
   */
  public UploadImportFile(customers: any) {
    return this.http
      .post<any>(`${this.apiUrl}/ImportCustomers/reader`, customers)
      .pipe(
        catchError(error => {
          return throwError(error);
        })
      )
  }

  /**
   * Thực hiện xuất khẩu dữ liệu
   * @param start vị trí bản ghi bắt đầu
   * @param end vị trí bản ghi kết thúc
   * @returns Kết quả và thông điệp
   * Author: HHDang (24/09/2021)
   */
  public ExportCustomers(start: any, end: any) {
    return this.http
      .get<any>(`${this.apiUrl}/ExportCustomers/sender?start=${start}&end=${end}`)
      .pipe(
        catchError(error => {
          return throwError(error);
        })
      );
  }
}
