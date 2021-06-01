import { Component, Input, OnInit } from '@angular/core';
import { ReadingDefinition } from '@core/models/ReadingDefinition';
import { DefinitionService } from '@core/services/definition.service';
import { IconDefinition } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'definitions-form',
  templateUrl: './definitions-form.component.html',
  styleUrls: ['./definitions-form.component.sass'],
})
export class DefinitionsFormComponent implements OnInit {
  @Input() definition: ReadingDefinition;

  public get icons(): IconDefinition[] {
    return this.definitionService.availableIcons;
  }

  constructor(private definitionService: DefinitionService) {}

  ngOnInit(): void {
    if (this.definition === undefined || this.definition === null) {
      this.definition = new ReadingDefinition();
      this.definition.icon = this.definitionService.defaultIcon.iconName;
    }
  }

  public getIcon(key: string): IconDefinition {
    return this.definitionService.availableIcons.filter(
      (icon) => icon.iconName === key,
    )[0];
  }

  public iconChanged(icon: IconDefinition): void {
    this.definition.icon = icon.iconName;
  }
}
