import { Component, NgZone, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts';
import { ProductClient, ProductDto } from '../../product-client';

@Component({
  selector: 'product-overview',
  templateUrl: './product-overview-component.html',
  styleUrls: ['./product-overview-component.scss']
})
export class ProductOverviewComponent implements OnInit {
  Highcharts: typeof Highcharts = Highcharts;
  chartOptions: Highcharts.Options = {};
  isLoaded: boolean = false;
  selectedCategory: string = "";
  showTable: boolean = false;
  productDtos: ProductDto[] = [];
  constructor(private productClient: ProductClient, private zone: NgZone) { }

  ngOnInit(): void {
    this.productClient.getAll().subscribe(data => {
      this.productDtos = data.map(r => new ProductDto(r.productId, r.productName, r.serviceName, r.creditVolume, r.debitVolume));
      const pieData = data.reduce((acc, item: ProductDto) => {
        if (item.creditVolume) {
          acc[0].y += item.creditVolume;
        }
        if (item.debitVolume) {
          acc[1].y += item.debitVolume;
        }
        return acc;
      }, [
        { name: 'Credit', y: 0 },
        { name: 'Debit', y: 0 }
      ]);

      let $this = this;
      this.chartOptions = {
        chart: {
          type: 'pie'
        },
        title: {
          text: 'Credit and Debit Volume'
        },
        tooltip: {
          pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>',
          formatter: function () {
            $this.zone.run(() => {
              $this.selectedCategory = this.point.name;
              $this.showTable = true;
            });
            return `${this.point.name}: <b>${this.point.percentage}%</b>`
          }
        },
        plotOptions: {
          pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
              enabled: true,
              format: '<b>{point.name}</b>: {point.percentage:.1f} %'
            },
            showInLegend: true
          }
        },
        series: [{
          name: 'Volume',
          colorByPoint: true,
          data: pieData,
          type: 'pie'
        } as Highcharts.SeriesPieOptions]
      };

      this.isLoaded = true;
    });
  }
}
