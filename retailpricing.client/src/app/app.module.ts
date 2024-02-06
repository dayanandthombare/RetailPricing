import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PricingGridComponent } from './components/pricing-grid/pricing-grid.component';
import { PricingEditComponent } from './components/pricing-edit/pricing-edit.component';
import { FileUploadComponent } from './components/file-upload/file-upload.component';
import { PricingService } from './services/pricing.service';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { DatePipe } from '@angular/common';
@NgModule({
  declarations: [
    AppComponent,
    PricingGridComponent,
    PricingEditComponent,
    FileUploadComponent
  ],
  imports: [
    BrowserModule, BrowserAnimationsModule,
    TableModule,
    ButtonModule,
    InputTextModule, DialogModule, FormsModule,
    ReactiveFormsModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [PricingService,DatePipe, provideAnimationsAsync()],
  bootstrap: [AppComponent]
})
export class AppModule { }
