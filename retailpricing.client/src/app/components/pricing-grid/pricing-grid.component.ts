import { Component, OnInit } from '@angular/core';
import { PricingService, PricingRecord } from '../../services/pricing.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-pricing-grid',
    templateUrl: './pricing-grid.component.html',
    styleUrls: ['./pricing-grid.component.css']
})
export class PricingGridComponent implements OnInit {
    isLoading: boolean = true; 
    pricingRecords: PricingRecord[] = [];
    globalFilterFields: string[] = ['storeId', 'sku', 'productName', 'price', 'date'];
    constructor(private pricingService: PricingService, private router: Router) { }

    ngOnInit(): void {
        this.loadPricingRecords();
    }

    loadPricingRecords(): void {
        this.isLoading = true;
        this.pricingService.searchPricingRecords({}).subscribe(
            data => {
                this.pricingRecords = data;
                this.isLoading = false;
            },
            error => {
                console.error('Error loading pricing records:', error);
                this.isLoading = false;
            }
        );
    }

    onEdit(id: number): void {
        this.router.navigate(['/pricing-edit', id]);
    }

    applyGlobalFilter(event: Event, dt: any): void {
        const filterValue = (event.target as HTMLInputElement).value;
        dt.filterGlobal(filterValue, 'contains');
    }
}
