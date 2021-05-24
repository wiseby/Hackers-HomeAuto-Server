import { Component, OnInit } from '@angular/core';
import { NodeDevice } from '@core/models/NodeDevice';
import { NodeService } from '@core/services/node.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-configurations-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.sass'],
})
export class ContainerComponent implements OnInit {
  public nodes: Observable<NodeDevice[]> = null;

  constructor(private nodeService: NodeService) {}

  ngOnInit(): void {
    this.nodes = this.nodeService.getAllNodes();
  }
}
