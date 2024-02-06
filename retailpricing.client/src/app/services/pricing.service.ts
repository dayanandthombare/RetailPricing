import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, map, of } from 'rxjs';

export interface PricingRecord {
  id?: number;
  storeId: string;
  sku: string;
  productName: string;
  price: number;
  date: string;
}

@Injectable({
  providedIn: 'root'
})
export class PricingService {
  private apiUrl = 'https://localhost:7011'; 

  constructor(private http: HttpClient) { }

  uploadPricingFile(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file, file.name);
    return this.http.post(`${this.apiUrl}/upload`, formData);
  }
  getPricingRecordById(id: number): Observable<PricingRecord | null> {
    return this.http.get<PricingRecord[]>(`${this.apiUrl}/${id}`).pipe(
      map(records => records.length > 0 ? records[0] : null),
      catchError(error => {
        console.error('Error fetching record:', error);
        return of(null); 
      })
    );
  }

  searchPricingRecords(criteria: any): Observable<PricingRecord[]> {
    return this.http.get<PricingRecord[]>(`${this.apiUrl}/search`, { params: criteria });
  }

  addPricingRecord(record: PricingRecord): Observable<PricingRecord> {
    return this.http.post<PricingRecord>(this.apiUrl, record);
  }

  updatePricingRecord(id: number, record: PricingRecord): Observable<any> {
    return this.http.put(`${this.apiUrl}/update/${id}`, record);
  }

  deletePricingRecord(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
