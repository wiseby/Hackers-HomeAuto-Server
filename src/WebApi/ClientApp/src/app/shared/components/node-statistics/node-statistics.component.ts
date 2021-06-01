import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnChanges,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NodeDevice } from '@core/models/NodeDevice';
import { Reading } from '@core/models/Reading';
import { NodeService } from '@core/services/node.service';
import { StatisticsService } from '@core/services/statistics.service';
import * as Chart from 'chart.js';

@Component({
  selector: 'node-statistics',
  templateUrl: './node-statistics.component.html',
  styleUrls: ['./node-statistics.component.sass'],
})
export class NodeStatisticsComponent implements OnInit, OnChanges {
  @Input() public node: NodeDevice;
  public chartRef: Chart;

  private readings: Reading[];
  @ViewChild('temperatureChart') temperatureChart: ElementRef;

  constructor(
    private nodeService: NodeService,
    private statisticsService: StatisticsService,
  ) {}

  private updateReadings(): void {
    this.nodeService
      .getReadings(this.node.clientId)
      .subscribe((readings: Reading[]) => {
        this.readings = readings;
        console.log(readings);
        this.statisticsService.buildTempChart(this.node, readings);
        if (this.chartRef === undefined) {
          this.chartRef = new Chart(
            this.temperatureChart.nativeElement,
            this.statisticsService.chartConf,
          );
        } else {
          this.chartRef.update();
        }
      });
  }

  ngOnInit(): void {
    this.updateReadings();
  }

  ngOnChanges(): void {
    this.updateReadings();
  }
}
