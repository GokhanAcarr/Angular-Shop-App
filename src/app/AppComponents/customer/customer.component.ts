import { Component, inject, AfterViewInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomerDialogBoxComponent } from '../customer-dialog-box/customer-dialog-box.component';

declare var bootstrap: any;

@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [CommonModule, FormsModule, CustomerDialogBoxComponent],
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements AfterViewInit {
  http = inject(HttpClient);
  CustomerList: any[] = [];

  customerToEdit: any = null;
  editModalInstance: any;

  ngOnInit(): void {
    this.loadCustomers();
  }

  ngAfterViewInit(): void {
    const modalEl = document.getElementById('editCustomerModal');
    if (modalEl) {
      this.editModalInstance = new bootstrap.Modal(modalEl);
      modalEl.addEventListener('hidden.bs.modal', () => {
        this.customerToEdit = null;
      });
    }
  }

  loadCustomers(): void {
    this.http.get('https://localhost:7288/api/Customer').subscribe({
      next: (data: any) => this.CustomerList = data,
      error: err => console.error(err)
    });
  }

  deleteCustomer(customerId: number): void {
    this.http.delete(`https://localhost:7288/api/Customer/${customerId}`).subscribe({
      next: () => this.loadCustomers(),
      error: err => console.error(err)
    });
  }

  openEditModal(customer: any): void {
    this.customerToEdit = { ...customer };

    if (!this.editModalInstance) {
      const modalEl = document.getElementById('editCustomerModal');
      if (modalEl) {
        this.editModalInstance = new bootstrap.Modal(modalEl);
      } else {
        console.error('Edit modal element not found!');
        return;
      }
    }

    this.editModalInstance.show();
  }

  updateCustomer(): void {
    if (!this.customerToEdit) {
      console.error('No customer to update!');
      return;
    }

    this.http.put('https://localhost:7288/api/Customer', this.customerToEdit).subscribe({
      next: () => {
        this.editModalInstance.hide();
        this.loadCustomers();
      },
      error: err => console.error(err)
    });
  }
}
