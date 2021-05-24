import { Component, OnInit } from '@angular/core';
import { NodeDevice } from '@core/models/NodeDevice';
import { NodeService } from '@core/services/node.service';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-node-details-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.sass'],
})
export class ContainerComponent implements OnInit {
  public node: NodeDevice = null;

  constructor(
    private nodeService: NodeService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      const clientId = params['clientId'];
      const getSub = this.nodeService
        .getSingleNode(clientId)
        .subscribe((node: NodeDevice) => {
          this.node = node;
          console.log('Fetched node in container: ', node);

          getSub.unsubscribe();
        });
    });
    console.log('In DetailsContainer');
  }
}
