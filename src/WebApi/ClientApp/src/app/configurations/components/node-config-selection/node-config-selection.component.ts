import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NodeDevice } from '@core/models/NodeDevice';
import { NodeService } from '@core/services/node.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'node-config-selection',
  templateUrl: './node-config-selection.component.html',
  styleUrls: ['./node-config-selection.component.sass'],
})
export class NodeConfigSelectionComponent implements OnInit {
  public nodes: Observable<NodeDevice[]> = null;

  constructor(
    private nodeService: NodeService,
    private route: ActivatedRoute,
    private routing: Router,
  ) {}

  ngOnInit(): void {
    this.nodes = this.nodeService.getAllNodes();
  }

  public navigateToNodeConfig(node: NodeDevice): void {
    this.routing.navigate([node.clientId], { relativeTo: this.route });
  }
}
