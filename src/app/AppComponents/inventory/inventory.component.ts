import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-inventory',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent {

disableProductIdInput = false;
populateFormForEdit(inventory: any) {
  this.InventoryData.ProductId = inventory.productId;
  this.InventoryData.ProductName = inventory.productName;
  this.InventoryData.AvailableQty = inventory.availableQty;
  this.InventoryData.ReOrderPoint = inventory.reOrderPoint;

  this.disableProductIdInput = true;
}
deleteInventory(ProductId: number): void {
  const apiUrl = `https://localhost:7288/api/Inventory/${ProductId}`;
  this.httpClient.delete(apiUrl)
    .subscribe({
      next: () => {
        console.log(`Product ${ProductId} deleted.`);
        this.getInventoryDetails();
      },
      error: (error) => {
        console.error('Error:', error);
      }
    });
}


  httpClient = inject(HttpClient);

  InventoryData = {
    ProductId: 0,
    ProductName: '',
    AvailableQty: 0,
    ReOrderPoint: 0
  };

  InventoryDetails: any;

  ngOnInit(): void {
    this.getInventoryDetails();
    
  }

  getInventoryDetails(): void {
    const apiUrl = 'https://localhost:7288/api/Inventory';
    this.httpClient.get(apiUrl)
      .subscribe(data=>{
        this.InventoryDetails = data;
      });
      this.InventoryData = {
    ProductId: 0,
    ProductName: '',
    AvailableQty: 0,
    ReOrderPoint: 0
  };
  this.disableProductIdInput = false;
    }


  onSubmit(): void {
    const apiUrl = 'https://localhost:7288/api/Inventory';
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    if(this.disableProductIdInput === true){
      this.httpClient.put(apiUrl, this.InventoryData, httpOptions)
      .subscribe({
        next: (response) => {
          console.log('Success:', response);
          this.ngOnInit();
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
    }
    else{
      this.httpClient.post(apiUrl, this.InventoryData, httpOptions)
      .subscribe({
        next: (response) => {
          console.log('Success:', response);
          this.ngOnInit();
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
    }
  }
}
