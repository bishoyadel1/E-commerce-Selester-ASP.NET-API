import { ClaimsService } from './claims.service';
import { Injectable } from '@angular/core';
import { ILogin } from '../Interfaces/ilogin';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Itoken } from '../Interfaces/itoken';
import { CurrentuserService } from './currentuser.service';
import { CartService } from './cart.service';
import { FavoriteService } from './favorite.service';

import { IFpass } from '../Interfaces/ifpass';
import { IResetPassword } from '../Interfaces/iresetpassword';
import { IemailConfirmation } from '../Interfaces/emailConfirmation';
@Injectable({
  providedIn: 'root'
})
export class LoginService {

  IsLoggedIn = new BehaviorSubject<boolean>(false)
  IsAdmin = new BehaviorSubject<boolean>(false)
  IsDelivery = new BehaviorSubject<boolean>(false)
  IsSeller = new BehaviorSubject<boolean>(false)
  CurrentUserName = new BehaviorSubject<string>('')

  BaseUrl: string = "https://salester.bsite.net/api/Accounts/Login"

  constructor(private httpclien: HttpClient, private claim: ClaimsService, private data: CurrentuserService, private cartservice: CartService, private favoriteservice: FavoriteService) { }

  Login(user: ILogin): Observable<Itoken> {
    return this.httpclien.post<Itoken>(this.BaseUrl, user).pipe(tap((res: any) => {
      if (res) {
        localStorage.setItem('token', res.token);
        sessionStorage.setItem('token', res.token);

        // Get Name of the User from Token Claims and display it in NavBar
        let claims = JSON.parse(window.atob(res.token.split('.')[1]));

        this.CurrentUserName.next(claims[this.claim.claimTypes.GivenName]);

        // check for the admin if logged in or not!
        if (Array.isArray(claims[this.claim.claimTypes.Role]) && claims[this.claim.claimTypes.Role].includes('Admin')) {
          this.IsAdmin.next(true);
        } else if (claims[this.claim.claimTypes.Role] === 'Admin') {
          this.IsAdmin.next(true);
        } else {
          this.IsAdmin.next(false);
        }
        if (Array.isArray(claims[this.claim.claimTypes.Role]) && claims[this.claim.claimTypes.Role].includes('Delivery')) {
          this.IsDelivery.next(true);
        } else if (claims[this.claim.claimTypes.Role] === 'Delivery') {
          this.IsDelivery.next(true);
        } else {
          this.IsDelivery.next(false);
        }
        if (Array.isArray(claims[this.claim.claimTypes.Role]) && claims[this.claim.claimTypes.Role].includes('Seller')) {
          this.IsSeller.next(true);
        } else if (claims[this.claim.claimTypes.Role] === 'Seller') {
          this.IsSeller.next(true);
        } else {
          this.IsSeller.next(false);
        }

      }

      if (sessionStorage.getItem('token')) {

        this.IsLoggedIn.next(true)

        this.cartservice.GetCart().subscribe();

        this.favoriteservice.GetFavorite().subscribe();
      }

      /*  if (localStorage.getItem('token')) {
  
          this.IsLoggedIn.next(true)
  
          this.cartservice.GetCart().subscribe();
  
          this.favoriteservice.GetFavorite().subscribe();
        }*/


    }))
  }
  forgotPassword(user: IFpass): Observable<any> {
    const forgotPasswordUrl = 'https://salester.bsite.net/api/Accounts/ForgotPassword';
    return this.httpclien.post<any>(forgotPasswordUrl, user);
  }
  resetPassword(resetPasswordDto: IResetPassword): Observable<any> {
    const resetPasswordUrl = 'https://salester.bsite.net/api/Accounts/ResetPassword';
    return this.httpclien.post<any>(resetPasswordUrl, resetPasswordDto);
  }
  confirmEmail(emailConfirmationDto: IemailConfirmation): Observable<any> {
    const emailConfirmationUrl = 'https://salester.bsite.net/api/Accounts/EmailConfirmation';
    return this.httpclien.post<any>(emailConfirmationUrl, emailConfirmationDto);
  }


}
