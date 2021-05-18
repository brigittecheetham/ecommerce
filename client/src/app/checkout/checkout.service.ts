import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { IOrderToCreate } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  createOrder(orderToCreate: IOrderToCreate) {
    return this.http.post(this.baseUrl + 'orders', orderToCreate);
  }

  getDeliveryMethods() {
    return this.http.get(this.baseUrl+ "orders/deliveryMethods").pipe(
      map((deliveryMethod: IDeliveryMethod[]) => {
        return deliveryMethod.sort((a, b) => b.price - a.price);
      })
    );
  }
}
