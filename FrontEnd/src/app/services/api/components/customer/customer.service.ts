import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http'
import { Observable, of } from 'rxjs';
import { Customer } from '../../../../entities/Customer';
import { APIConfig } from '../../config/apiconfig';

@Injectable({
  providedIn: 'root'
})

export class CustomerService {
  private apiUrl = `${APIConfig.development}/api/v1`

  constructor(private http:HttpClient) { }

  public GetCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.apiUrl);
  } 

  public async GetFilterCustomer(pageInfo: any) {
    let pageIndex = pageInfo.pageIndex;
    let pageSize = pageInfo.pageSize;
    let customerFilter = pageInfo.customerFilter;
    let res;
    try {
      res = await this.http
      .get<any>(`${this.apiUrl}/Customers/customerFilter?pageIndex=${pageIndex}&customerFilter=${customerFilter}&pageSize=${pageSize}`)
      .toPromise();
    } catch (error) {
      console.log(error);
    }
    return res;
  }

  public async UploadImportFile(file: any) {
    let response;
    try {
      response = await this.http.post<any>(`${this.apiUrl}/ImportCustomers/reader`, file).toPromise();
    } catch (error: any) {
      response = error.response.data;
    }
    return response;
  }
}
