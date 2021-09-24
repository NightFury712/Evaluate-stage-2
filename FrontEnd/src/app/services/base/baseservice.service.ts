import { Injectable } from '@angular/core';
import { MISACode } from '../../resources/MISAEnums';
import { Toasts } from 'src/app/resources/MISAConst';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})

export class BaseService {

  constructor(private toastService: ToastrService) { }

  /**
   * Hàm xử lý thông điệp trả về khi gọi api
   * @param response thông điệp phản hồi
   * @returns Kết quả và thông điệp
   * Author: HHDang (24/09/2021)
   */
  public HandleResponseMessage(response: any): Object {
    let result;
    if( response.MISACode === MISACode.Ok) 
    {
      result = {
        flag: true,
        messenger: response.Messenger,
      };
      this.toastService.success(response.Messenger)
    } 
    else if( response.MISACode === MISACode.Exception)
    {
      result = {
        flag: true,
        messenger: response.Messenger,
      };
      this.toastService.error(response.Messenger)
    } else 
    {
      result = {
        flag: false,
        messenger: response.Messenger,
      }
    }
    return result;
  }
}
