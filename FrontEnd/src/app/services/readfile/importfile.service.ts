import { Injectable } from '@angular/core';
import { Subscriber } from 'rxjs';
import * as XLSX from 'xlsx';

@Injectable({
  providedIn: 'root',
})
export class ImportfileService {
  constructor() {}
  /**
   * Hàm thực thi chức năng đọc file excel
   * @param file tệp cần đọc
   * @param subscriber
   * Author: HHDang (16/09/2021)
   */
  public ReadFile(file: any, subscriber: Subscriber<any>) {
    const reader: FileReader = new FileReader();

    reader.onload = (e: any) => {
      const bstr: string = e.target.result;

      const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });

      const wsname: string = wb.SheetNames[0];
      const ws: any = wb.Sheets[wsname];

      const columnHeaders = this.getCoLumnHeaders(ws);
      // console.log(columnHeaders);
      const data: any = XLSX.utils.sheet_to_json(ws);
      
      subscriber.next(data);
      subscriber.complete();
    };

    reader.readAsBinaryString(file);
  }

  /**
   * Lấy thông tin tiêu đề các cột trong worksheet hiện tại
   * @param worksheet thông tin worksheet hiện tại
   * @returns mảng tiêu đề cho các cột
   */
  private getCoLumnHeaders(worksheet: any) {
    let columnHeaders = [];
    for (let key in worksheet) {
      let regEx = new RegExp('^(\\w)(1){1}$');
      if (regEx.test(key)) {
        columnHeaders.push(worksheet[key].v);
      }
    }
    return columnHeaders;
  }
}
