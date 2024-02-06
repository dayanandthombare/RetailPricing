import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PricingService } from '../../services/pricing.service'; 
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-pricing-edit',
  templateUrl: './pricing-edit.component.html',
  styleUrls: ['./pricing-edit.component.css']
})
export class PricingEditComponent implements OnInit {
  editForm: FormGroup = new FormGroup({
    storeId: new FormControl('', Validators.required),
    sku: new FormControl('', Validators.required),
    productName: new FormControl('', Validators.required),
    price: new FormControl('', [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/)]),
    date: new FormControl('', Validators.required)
  });
  currentRecordId!: number;
  id!: number;
  isLoading: boolean = false;
  constructor(
    private datePipe: DatePipe,
    private pricingService: PricingService,
    private route: ActivatedRoute,
    private router: Router
  ) { }


  ngOnInit(): void {
    this.route.params.subscribe(params => {
       this.id = +params['id'];
      if (this.id) {
        this.loadRecord(this.id);
      }
    });
  }


  loadRecord(id: number): void {
    this.isLoading = true;
    this.pricingService.getPricingRecordById(id).subscribe(data => {
    
      if (data && data.date) {
        const formattedDate = this.datePipe.transform(data.date, 'yyyy-MM-dd');
        this.editForm.patchValue({
          ...data,
          date: formattedDate 
        });
        this.isLoading = false;
      } else {
      
        console.error('Record not found or date is missing');
      }
    }, error => {
      console.error('Error fetching record:', error);
      this.isLoading = false;
    });
  }
  onSubmit(): void {
    if (this.editForm.valid) {
      this.pricingService.updatePricingRecord(this.id, this.editForm.value).subscribe({
        next: () => {
          console.log('Record updated successfully');
          this.router.navigateByUrl('/pricing-list'); 
        },
        error: (err) => console.error('Error updating record', err)
      });
    }
  }


  get storeId() { return this.editForm.get('storeId'); }
  get sku() { return this.editForm.get('sku'); }
  get productName() { return this.editForm.get('productName'); }
  get price() { return this.editForm.get('price'); }
  get date() { return this.editForm.get('date'); }
}
