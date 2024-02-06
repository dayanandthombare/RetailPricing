import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PricingGridComponent } from './components/pricing-grid/pricing-grid.component';
import { PricingEditComponent } from './components/pricing-edit/pricing-edit.component';
import { FileUploadComponent } from './components/file-upload/file-upload.component';

const routes: Routes = [
  
  { path: 'pricing-list', component: PricingGridComponent },
  { path: 'pricing-edit/:id', component: PricingEditComponent },
  { path: 'file-upload', component: FileUploadComponent },
  { path: ' ', redirectTo: '/', pathMatch: 'full' },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
