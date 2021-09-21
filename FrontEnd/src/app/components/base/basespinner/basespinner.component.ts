import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-basespinner',
  templateUrl: './basespinner.component.html',
  styleUrls: ['./basespinner.component.css']
})
export class BasespinnerComponent implements OnInit {
  loaderUrl: any = "../../../../assets/content/img/loading.svg";

  constructor() { }

  ngOnInit(): void {
  }

}
