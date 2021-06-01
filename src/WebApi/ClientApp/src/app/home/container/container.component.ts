import { Component, OnInit } from '@angular/core';
import { NodeDevice } from '@core/models/NodeDevice';
import { NodeService } from '@core/services/node.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.sass'],
})
export class ContainerComponent implements OnInit {
  public nodes: Observable<NodeDevice[]> = null;
  public selectedNode: NodeDevice;
  public showDefinitionsModal = false;

  constructor(private nodeService: NodeService) {}

  ngOnInit(): void {
    this.nodes = this.nodeService.getAllNodes();
  }

  public nodeSelected(node: NodeDevice): void {
    const sub = this.nodeService
      .getSingleNode(node.clientId)
      .subscribe((node: NodeDevice) => {
        this.selectedNode = node;
        sub.unsubscribe();
      });
  }

  public toogleDefinitionsModal(): void {
    this.showDefinitionsModal = !this.showDefinitionsModal;
  }
}
