import { Component } from '@angular/core';
import { IFpass } from 'src/app/Interfaces/ifpass';
import { LoginService } from '../../../Services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgetpassword',
  templateUrl: './forgetpassword.component.html',
  styleUrls: ['./forgetpassword.component.css']
})
export class ForgetpasswordComponent {
  user: IFpass = {
    email: '',
  };

  constructor(private loginService: LoginService, private router: Router) { }

  submitForm() {
    this.loginService.forgotPassword(this.user).subscribe(
      // Handle the response or error as needed
      (response) => {
        // Assuming the response indicates success
        console.log('Password reset email sent successfully.');
        // Navigate to the login page
        this.router.navigate(['/login']);
      },
      (error) => {
        // Handle the error
        console.error('Error sending password reset email:', error);
      }
    );
  }
}
