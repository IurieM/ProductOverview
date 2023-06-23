import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HighchartsChartModule } from 'highcharts-angular';

import { ProductOverviewComponent } from './products/components/overview/product-overview-component';
import { ProductTableComponent } from './products/components/datatable/product-table-component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProductOverviewComponent,
    ProductTableComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    HighchartsChartModule,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
