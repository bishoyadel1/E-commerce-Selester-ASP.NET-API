import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ireport } from 'src/app/Interfaces/ireport';
import { Ireportprofit } from 'src/app/Interfaces/ireportprofit';

@Injectable({
  providedIn: 'root'
})
export class ReporstService {
  baseURL: string = 'https://salester.bsite.net/api/AdminReport/GetByDate';
  baseURLForProfitSeller: string = 'https://salester.bsite.net/api/AdminReport/GetSellerProfitByDate';
  constructor(private http: HttpClient) { }


  // ---------------- [ Get All Orders ]
  GetAllOrders(startDate: Date, endDate: Date): Observable<Ireport[]> {
    return this.http.get<Ireport[]>(`${this.baseURL}?startDate=${startDate}&endDate=${endDate}`);
  }
  GetAllProfitSeller(startDate: Date, endDate: Date): Observable<Ireportprofit[]> {
    return this.http.get<Ireportprofit[]>(`${this.baseURLForProfitSeller}?startDate=${startDate}&endDate=${endDate}`);
  }
}
