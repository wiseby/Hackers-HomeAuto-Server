import { Component, Input, OnInit } from '@angular/core';
import { NodeDevice } from '@core/models/NodeDevice';

@Component({
  selector: 'node-general-info',
  templateUrl: './general-info.component.html',
  styleUrls: ['./general-info.component.sass'],
})
export class GeneralInfoComponent implements OnInit {
  @Input() public node: NodeDevice = null;
  ngOnInit(): void {}
}
