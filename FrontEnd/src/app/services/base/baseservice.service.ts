import { Injectable } from '@angular/core';
import { MISACode } from '../../resources/MISAEnums';
import { Icons } from 'src/app/resources/MISAConst';

@Injectable({
  providedIn: 'root'
})
export class BaseserviceService {

  constructor() { }

  public HandleResponseMessage(response: any): any {
    let result;
    if( response.MISACode === MISACode.Ok) 
    {
      result = {
        isShow: false,
        messenger: response.Messenger,
        icon: Icons.success
      }
    } 
    else if( response.MISACode === MISACode.Exception)
    {
      result = {
        isShow: false,
        messenger: response.Messenger,
        icon: Icons.error
      }
    } else 
    {
      result = {
        isShow: true,
        messenger: response.Messenger,
        icon: Icons.error
      }
    }
    return result;
  }
}
