import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';


export class ProductDto {
    constructor(
        public productId: number,
        public productName: string,
        public serviceName: string | undefined,
        public creditVolume: number | undefined,
        public debitVolume: number | undefined) {
    }

    get isCredit(): boolean {
        return !!this.creditVolume && this.creditVolume > 0;
    }

    get isDebit(): boolean {
        return !!this.debitVolume && this.debitVolume > 0;
    }

    get volume(): number | undefined {
        return this.isCredit ? this.creditVolume : this.debitVolume;
    }
}

export interface IProductClient {
    getAll(): Observable<ProductDto[]>;
}

@Injectable({
    providedIn: 'root'
})
export class ProductClient implements IProductClient {
    private http: HttpClient;

    constructor(@Inject(HttpClient) http: HttpClient) {
        this.http = http;
    }

    getAll(): Observable<ProductDto[]> {
        return this.http.get<ProductDto[]>(`${environment.baseUrl}/products`)
    }
}