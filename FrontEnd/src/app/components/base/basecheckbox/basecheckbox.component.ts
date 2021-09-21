import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-basecheckbox',
  templateUrl: './basecheckbox.component.html',
  styleUrls: ['./basecheckbox.component.css']
})
export class BasecheckboxComponent implements OnInit {
  @Input() checkboxInfo: any;
  constructor() { }

  ngOnInit(): void {
  }

}
