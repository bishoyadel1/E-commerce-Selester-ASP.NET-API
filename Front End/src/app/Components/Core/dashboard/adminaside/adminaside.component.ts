import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from 'src/app/Services/cart.service';
import { ClaimsService } from 'src/app/Services/claims.service';
import { CurrentuserService } from 'src/app/Services/currentuser.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-adminaside',
  templateUrl: './adminaside.component.html',
  styleUrls: ['./adminaside.component.css']
})
export class AdminasideComponent implements OnInit {
  constructor(public log: LoginService, public data: CurrentuserService, private claim: ClaimsService, private router: Router) { }

  ngOnInit(): void {
    let token = localStorage.getItem('token');



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
      if (Array.isArray(claims[this.claim.claimTypes.Role]) && claims[this.claim.claimTypes.Role].includes('Delivery')) {
        this.log.IsDelivery.next(true);
      } else if (claims[this.claim.claimTypes.Role] === 'Delivery') {
        this.log.IsDelivery.next(true);
      } else {
        this.log.IsDelivery.next(false);
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

}
