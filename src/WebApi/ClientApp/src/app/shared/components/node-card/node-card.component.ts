import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  faDoorOpen,
  faMapMarkedAlt,
  faQuestionCircle,
  faRss,
  faThermometerHalf,
  faWater,
  IconDefinition,
} from '@fortawesome/free-solid-svg-icons';
import { ReadingIconsEnum } from '@core/enums/ReadingIconsEnum';
import { Dictionary } from '@core/Utils/Dictionary';
import { NodeDevice } from '@core/models/NodeDevice';

@Component({
  selector: 'app-node-card',
  templateUrl: './node-card.component.html',
  styleUrls: ['./node-card.component.sass'],
})
export class NodeCardComponent implements OnInit {
  @Input() public node: NodeDevice;
  @Input() public showConfigure = false;
  @Input() public showDetails = true;
  @Output() public detailsClicked = new EventEmitter<NodeDevice>();
  @Output() public configureClicked = new EventEmitter<NodeDevice>();

  private _readings: Dictionary<string>;
  public get readings(): Dictionary<string> {
    return this._readings;
  }
  public set readings(value: Dictionary<string>) {
    this._readings = value;
  }
  public icons: IconDefinition[] = [
    faQuestionCircle,
    faThermometerHalf,
    faWater,
    faDoorOpen,
    faRss,
    faMapMarkedAlt,
  ];

  public defaultIcon = faQuestionCircle;

  ngOnInit(): void {
    this._readings = new Dictionary<string>();
    for (const [key, value] of Object.entries(this.node.latestReading.values)) {
      this._readings.add(key, value as string);
    }
  }

  public getIcon(key: string): IconDefinition {
    const definition = this.node.readingDefinitions?.filter((def) => {
      return def.name === key;
    })[0];
    if (definition !== undefined) {
      return this.icons.filter(
        (icon: IconDefinition) => icon.iconName.toString() === definition.icon,
      )[0];
    }
    return this.defaultIcon;
  }

  public getTypeAsString(value: Record<string, unknown>): string {
    for (const key in value) {
      if (value.hasOwnProperty(key)) {
        const object = Object.keys(value)[0];
        return object;
      }
    }
  }
}
