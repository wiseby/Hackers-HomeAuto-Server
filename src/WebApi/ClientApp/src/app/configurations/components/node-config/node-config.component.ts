import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { NodeDevice } from '@core/models/NodeDevice';
import { ReadingDefinition } from '@core/models/ReadingDefinition';
import { DefinitionService } from '@core/services/definition.service';
import { NodeService } from '@core/services/node.service';

@Component({
  selector: 'node-config',
  templateUrl: './node-config.component.html',
  styleUrls: ['./node-config.component.sass'],
})
export class NodeConfigComponent implements OnInit {
  public selectedNode: NodeDevice;
  public isLoading = true;
  public form: FormGroup;

  constructor(
    private nodeService: NodeService,
    private definitionService: DefinitionService,
    private route: ActivatedRoute,
  ) {}

  public getDefinitions(): ReadingDefinition[] {
    this.selectedNode.readingDefinitions =
      this.definitionService.getDefinitions(this.selectedNode);
    return this.selectedNode.readingDefinitions;
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      const sub = this.nodeService
        .getSingleNode(params['clientId'])
        .subscribe((node: NodeDevice) => {
          this.selectedNode = node;
          console.log(node);
          this.isLoading = false;
          sub.unsubscribe();
        });
    });
  }

  public onSave(): void {
    const sub = this.definitionService
      .updateDefinitions(
        this.selectedNode.clientId,
        this.selectedNode.readingDefinitions,
      )
      .subscribe((response: ReadingDefinition[]) => sub.unsubscribe());
  }
}
