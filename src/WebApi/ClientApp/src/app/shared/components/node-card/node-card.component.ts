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

  private _readings: Dictionary;
  public get readings(): Dictionary {
    return this._readings;
  }
  public set readings(value: Dictionary) {
    this._readings = value;
  }

  public defaultIcon = faQuestionCircle;
  public temperatureIcon = faThermometerHalf;
  public humidityIcon = faWater;
  public dooropenIcon = faDoorOpen;
  public rssIcon = faRss;
  public locationIcon = faMapMarkedAlt;

  ngOnInit(): void {
    const dictInit = [];
    for (const [key, value] of Object.entries(this.node.latestReading.values)) {
      dictInit.push({ key: key, value: value });
    }
    this._readings = new Dictionary(dictInit);
  }

  public getIcon(key: string): IconDefinition {
    const definition = this.node.readingDefinitions.filter((def) => {
      return def.name === key;
    })[0];
    switch (ReadingIconsEnum[definition.icon]) {
      case ReadingIconsEnum.Temperature:
        return this.temperatureIcon;
      case ReadingIconsEnum.Humidity:
        return this.humidityIcon;
      case ReadingIconsEnum.OpenClose:
        return this.dooropenIcon;
      default:
        return this.defaultIcon;
    }
  }

  public getTypeAsString(value: Record<string, unknown>): string {
    for (const key in value) {
      if (value.hasOwnProperty(key)) {
        const object = Object.keys(value)[0];
        console.log('Property name: ', object);
        return object;
      }
    }
  }
}
