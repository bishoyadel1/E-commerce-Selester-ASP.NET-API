import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { catchError } from 'rxjs';
import { Ireport } from 'src/app/Interfaces/ireport';
import { Ireportprofit } from 'src/app/Interfaces/ireportprofit';
import { OrderStatus } from 'src/app/Interfaces/order/order-status';
import { ClaimsService } from 'src/app/Services/claims.service';
import { ReporstService } from 'src/app/Services/dashboard/reporst.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-admin-reports',
  templateUrl: './admin-reports.component.html',
  styleUrls: ['./admin-reports.component.css']
})
export class AdminReportsComponent implements OnInit {

  reports: Ireport[] = [];
  reportsprofit: Ireportprofit[] = [];
  startDate!: Date;
  endDate!: Date;
  reportForm!: FormGroup;
  showDialog = false;

  constructor(public log: LoginService, private reportService: ReporstService, private fb: FormBuilder, private claim: ClaimsService) {
    this.createForm();
  }
  ngOnInit(): void {
    let token = sessionStorage.getItem('token');


    if (token) {

      // in case there is a token then the login button will disappear
      this.log.IsLoggedIn.next(true);

      // in case the user is logged in load it's data


      // we get the name of the logged in user from the token claims in case it stored in the browser
      let claims = JSON.parse(window.atob(token.split('.')[1]));
      this.log.CurrentUserName.next(claims[this.claim.claimTypes.GivenName])

      // check if the admin logged in
      if (Array.isArray(claims[this.claim.claimTypes.Role]) && claims[this.claim.claimTypes.Role].includes('Admin')) {
        this.log.IsAdmin.next(true);
      } else if (claims[this.claim.claimTypes.Role] === 'Admin') {
        this.log.IsAdmin.next(true);
      } else {
        this.log.IsAdmin.next(false);
      }

      if (Array.isArray(claims[this.claim.claimTypes.Role]) && claims[this.claim.claimTypes.Role].includes('Seller')) {
        this.log.IsSeller.next(true);
      } else if (claims[this.claim.claimTypes.Role] === 'Seller') {
        this.log.IsSeller.next(true);
      } else {
        this.log.IsSeller.next(false);
      }


    }
    else
      this.log.IsLoggedIn.next(false);
  }



  createForm() {
    this.reportForm = this.fb.group({
      startDate: [null, Validators.required],
      endDate: [null, Validators.required]
    });
  }

  onSubmit() {
    if (this.reportForm.valid) {
      this.startDate = this.reportForm.value.startDate;
      this.endDate = this.reportForm.value.endDate;
      let token = sessionStorage.getItem('token');

      if (token) {

        let claims = JSON.parse(window.atob(token.split('.')[1]));
        this.log.CurrentUserName.next(claims[this.claim.claimTypes.GivenName])
        if (Array.isArray(claims[this.claim.claimTypes.Role]) && claims[this.claim.claimTypes.Role].includes('Admin')) {
          this.reportService.GetAllOrders(this.startDate, this.endDate).subscribe((data) => { this.reports = data; });
        } else if (claims[this.claim.claimTypes.Role] === 'Admin') {
          this.reportService.GetAllOrders(this.startDate, this.endDate).subscribe((data) => { this.reports = data; });
        }

      }

      this.reportService.GetAllProfitSeller(this.startDate, this.endDate).subscribe((data) => { this.reportsprofit = data; });

      // ---------------- [ Close the dialog after submitting ]
      this.showDialog = false;
    }
  }

  // ---------------- [ get Order Status Name ]
  getOrderStatusName(status: number): string {
    return OrderStatus[status];
  }


}
