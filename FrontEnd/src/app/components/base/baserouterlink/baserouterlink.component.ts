import { Component, Input, OnInit } from '@angular/core';
import { Router } from "./RouterInterface"

@Component({
  selector: 'app-baserouterlink',
  templateUrl: './baserouterlink.component.html',
  styleUrls: ['./baserouterlink.component.css']
})
export class BaserouterlinkComponent implements OnInit {
  @Input() routerInfo: Router = {
    href: "/dashboard",
    icon: "mi-sidebar-dashboard",
    title: "Tổng quan",
    name: "DashBoard"
  };

  constructor() { }

  ngOnInit(): void {

  }

}
