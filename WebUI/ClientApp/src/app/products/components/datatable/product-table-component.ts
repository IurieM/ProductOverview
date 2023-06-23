import { Component, Input } from '@angular/core';
import { ProductDto } from '../../product-client';

@Component({
    selector: 'product-table',
    templateUrl: './product-table-component.html'
})
export class ProductTableComponent {
    @Input() category: string = "";
    @Input() data: ProductDto[] = [];

    displayedColumns: string[] = ['productName', 'serviceName', 'volume']; 
    dataSource: ProductDto[] = []; 

    ngOnChanges() {
        this.dataSource = this.data.filter(item => this.category.toLowerCase() === "credit" ? item.isCredit : item.isDebit);
    }
}