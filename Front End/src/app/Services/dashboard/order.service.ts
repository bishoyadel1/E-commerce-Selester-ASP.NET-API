import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StreamInvocationMessage } from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { Iorder } from 'src/app/Interfaces/iorder';
import { IorderAdmin } from 'src/app/Interfaces/order/iorder-admin';
import { OrderStatus } from 'src/app/Interfaces/order/order-status';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseURL: string = 'https://salester.bsite.net/api/Order';
  AddOrderUrl: string = 'https://salester.bsite.net/api/Order/addOrder'
  modifiedUrl: string = 'https://salester.bsite.net/api/Order/addOrder?addressId=112&payMethod=asd&phoneNumebr=asd'
  UserorderUrl: string = 'https://salester.bsite.net/api/UserProfile/Orders';
  constructor(private http: HttpClient) { }


  // ---------------- [ Add New Order ]
  AddNewOrder(data: Iorder): Observable<void> {
    let params = new HttpParams()
      .set('addressId', data.addressId)
      .set('payMethod', data.payMethod)
      .set('phoneNumebr', data.phoneNumebr);

    return this.http.post<void>(this.AddOrderUrl, null, { params });
  }


  // ---------------- [ Get All Orders ]
  GetAllOrders(PageIndex: number): Observable<IorderAdmin[]> {
    return this.http.get<IorderAdmin[]>(`${this.baseURL}/GetAllOrders/${PageIndex}`);
  }

  // ---------------- [ Get Order By Id  ]
  GetOrderById(id: number): Observable<IorderAdmin> {
    return this.http.get<IorderAdmin>(`${this.baseURL}/${id}`);
  }

  // ---------------- [ Get All User Orders ]
  GetAllUserOrders(): Observable<IorderAdmin[]> {
    return this.http.get<IorderAdmin[]>(`${this.UserorderUrl}`);
  }
}
