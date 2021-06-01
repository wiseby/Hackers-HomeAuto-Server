import { Injectable } from '@angular/core';
import { NodeDevice } from '@core/models/NodeDevice';
import { Reading } from '@core/models/Reading';
import * as Chart from 'chart.js';
import {
  ChartConfiguration,
  ChartData,
  ChartOptions,
  ChartType,
} from 'chart.js';

@Injectable({
  providedIn: 'root',
})
export class StatisticsService {
  private lineChartLegend = true;
  private lineChartType: ChartType = 'line';
  public lineChartLabels = [];

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

  public buildTempChart(node: NodeDevice, readings?: Reading[]): ChartData {
    const tempData = [];
    const humData = [];
    const labels = [];
    let sortedReadings = [];
    if (readings !== undefined) {
      sortedReadings = readings.sort((a, b) =>
        a.createdAt < b.createdAt ? 1 : 0,
      );
    } else {
      sortedReadings = node.readings.sort((a, b) =>
        a.createdAt < b.createdAt ? 1 : 0,
      );
    }

    sortedReadings.forEach((reading) => {
      tempData.push(reading.values.temperature);
      humData.push(reading.values.humidity);
      labels.push(this.getFormatedDate(reading.createdAt.toString()));
    });
    this.chartData.datasets = [];
    this.chartData.datasets.push({
      data: tempData,
      label: node.clientId + ' - Temperature',
      yAxisID: 'temperatureValues',
      xAxisID: 'dates',
      borderColor: '#ff6161',
    });
    this.chartData.datasets.push({
      data: humData,
      label: node.clientId + ' - Humidity',
      yAxisID: 'humidityValues',
      xAxisID: 'dates',
      borderColor: '#7361ff',
    });
    this.chartData.labels = labels;
    console.log(this.chartConf);
    return this.chartConf.data;
  }

  private getFormatedDate(value: string): string {
    const date = new Date(value);
    const formatedDate = `${date.getFullYear()}-${
      date.getMonth() + 1
    }-${date.getDate()}`;
    return formatedDate;
  }
}
