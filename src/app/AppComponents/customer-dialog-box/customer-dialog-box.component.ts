import { Component, EventEmitter, Output, inject, AfterViewInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';

declare var bootstrap: any;

@Component({
  selector: 'app-customer-dialog-box',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './customer-dialog-box.component.html',
})
export class CustomerDialogBoxComponent implements AfterViewInit {
  http = inject(HttpClient);

  @Output() customerAdded = new EventEmitter<void>();

  customer = {
    customerName: '',
    phone: '',
    registrationDate: ''
  };

  modalInstance: any;

  ngAfterViewInit() {
    const modalEl = document.getElementById('customerModal');
    if (modalEl) {
      this.modalInstance = new bootstrap.Modal(modalEl);
    }
  }

  submitCustomer(): void {
    console.log('submitCustomer called');
    const apiUrl = 'https://localhost:7288/api/Customer';
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    this.http.post(apiUrl, this.customer, httpOptions).subscribe({
      next: () => {
        this.customerAdded.emit();
        this.resetForm();

        if (this.modalInstance) {
          this.modalInstance.hide();
        }
      },
      error: (err) => console.error('Error:', err)
    });
  }

  resetForm() {
    this.customer = {
      customerName: '',
      phone: '',
      registrationDate: ''
    };
  }
}
