import { Component, OnInit } from '@angular/core';
import { RouterLinkInfo } from '../../../resources/MISAConst'

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  routerInfo: any | undefined;

  constructor() { }

  ngOnInit(): void {
    this.routerInfo = RouterLinkInfo;
  }

}
