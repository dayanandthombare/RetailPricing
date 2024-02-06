import { Component } from '@angular/core';
import { HttpEventType, HttpResponse } from '@angular/common/http';
import { PricingService } from '../../services/pricing.service'; 

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent {
  selectedFile: File | null = null;
  uploadProgress: number = 0;
  fileName: string = 'Choose file...';
  isLoading: boolean = false;
  uploadSuccess: boolean | null = null;
  uploadMessage: string = '';
  isError: boolean = false;
  constructor(private pricingService: PricingService) { }

  onFileSelected(event: any) {
    const element = event.currentTarget as HTMLInputElement;
    const file = element.files && element.files.length > 0 ? element.files[0] : null;
    if (file) {
      this.selectedFile = file;
      this.fileName = this.selectedFile.name; 
    }
    else {
      this.fileName = ''; 
    }
  }

  onUpload(): void {
    if (!this.selectedFile) {
      this.uploadMessage = 'No file selected.';
      this.isError = true;
      return;
    }

    this.isLoading = true;
    this.pricingService.uploadPricingFile(this.selectedFile).subscribe({
      next: (response) => {
       
        this.uploadMessage = response.message || 'File successfully processed and data stored.';
        this.isError = false;
      },
      error: (error) => {
       
        this.uploadMessage = error.error.message || 'An error occurred during file upload.';
        this.isError = true;
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }
}
