import { Component, NgZone, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts';
import { ProductClient, ProductDto } from '../../product-client';
import { Observable, map, startWith } from 'rxjs';

@Component({
  selector: 'product-overview',
  templateUrl: './product-overview-component.html',
  styleUrls: ['./product-overview-component.scss']
})
export class ProductOverviewComponent implements OnInit {
  Highcharts: typeof Highcharts = Highcharts;
  selectedCategory: string = "";
  showTable: boolean = false;
  $products: Observable<any> = new Observable<any>();
  constructor(private productClient: ProductClient) { }

  ngOnInit(): void {
    this.$products = this.productClient.getAll().pipe(
      map(data => {
        return {
          products: data.map(r => new ProductDto(r.productId, r.productName, r.serviceName, r.creditVolume, r.debitVolume)),
          chartOptions: this.getChartOptions(data),
        };
      }));
  }

  getChartOptions(products: ProductDto[]): Highcharts.Options {
    let $this = this;
    return {
      chart: {
        type: 'pie'
      },
      title: {
        text: 'Credit and Debit Volume'
      },
      tooltip: {
        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>',
        formatter: function () {
          $this.selectedCategory = this.point.name;
          $this.showTable = true;
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
        data: this.getChartData(products),
        type: 'pie'
      } as Highcharts.SeriesPieOptions]
    };
  }

  getChartData(products: ProductDto[]): Highcharts.PointOptionsObject[] {
    return products.reduce((acc, item: ProductDto) => {
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
  }
}
