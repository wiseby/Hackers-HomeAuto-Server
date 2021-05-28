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
import { NodeService } from '@core/services/node.service';
import * as Chart from 'chart.js';
import {
  ChartConfiguration,
  ChartData,
  ChartOptions,
  ChartType,
} from 'chart.js';

@Component({
  selector: 'node-statistics',
  templateUrl: './node-statistics.component.html',
  styleUrls: ['./node-statistics.component.sass'],
})
export class NodeStatisticsComponent
  implements OnInit, OnChanges, AfterViewInit {
  @Input() public node: NodeDevice;

  private chartRef: Chart;

  public get chartConf(): ChartConfiguration {
    const conf: ChartConfiguration = {
      type: this.lineChartType,
      data: this.chartData,
      options: this.lineChartOptions,
    };
    return conf;
  }

  public chartData: ChartData = {
    datasets: [],
    labels: [],
  };

  public lineChartLabels = [];

  public lineChartOptions: ChartOptions = {
    responsive: true,
    scales: {
      xAxes: [
        {
          id: 'dates',
        },
      ],
      yAxes: [
        {
          id: 'temperatureValues',
          position: 'left',
        },
        {
          id: 'humidityValues',
          position: 'right',
        },
      ],
    },
  };

  public lineChartLegend = true;
  public lineChartType: ChartType = 'line';

  @ViewChild('temperatureChart') temperatureChart: ElementRef;
  @ViewChild('canvasColumn') canvasColumn: ElementRef;

  constructor(private nodeService: NodeService) {}

  private getFormatedDate(value: string): string {
    const date = new Date(value);
    const formatedDate = `${date.getFullYear()}-${
      date.getMonth() + 1
    }-${date.getDate()}`;
    return formatedDate;
  }

  private buildTempChart(): void {
    const tempData = [];
    const humData = [];
    const labels = [];
    const sortedReadings = this.node.readings.sort((a, b) =>
      a.createdAt < b.createdAt ? 1 : 0,
    );

    sortedReadings.forEach((reading) => {
      tempData.push(reading.values.temperature);
      humData.push(reading.values.humidity);
      labels.push(this.getFormatedDate(reading.createdAt.toString()));
    });
    this.chartData.datasets = [];
    this.chartData.datasets.push({
      data: tempData,
      label: this.node.clientId + ' - Temperature',
      yAxisID: 'temperatureValues',
      xAxisID: 'dates',
      borderColor: '#ff6161',
    });
    this.chartData.datasets.push({
      data: humData,
      label: this.node.clientId + ' - Humidity',
      yAxisID: 'humidityValues',
      xAxisID: 'dates',
      borderColor: '#7361ff',
    });
    this.chartData.labels = labels;
  }

  ngOnInit(): void {
    this.buildTempChart();
  }

  ngOnChanges(): void {
    this.buildTempChart();
    if (this.temperatureChart !== undefined) {
      this.chartRef.update();
    }
  }

  ngAfterViewInit(): void {
    this.chartRef = new Chart(
      this.temperatureChart.nativeElement,
      this.chartConf,
    );
  }
}
