import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoginService } from '../../../Services/login.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.css']
})
export class ResetpasswordComponent implements OnInit {
  email: string | null;
  token: string | null;

  constructor(private route: ActivatedRoute, private loginService: LoginService, private router: Router) {
    this.email = this.route.snapshot.queryParamMap.get('email');
    this.token = this.route.snapshot.queryParamMap.get('token');
  }

  ngOnInit(): void { }

  onSubmit(newPassword: string, confirmPassword: string): void {
    // Check if passwords match
    if (newPassword !== confirmPassword) {
      // Handle password mismatch error
      console.error('Passwords do not match');
      return;
    }

    // Call the service method to reset password
    this.loginService.resetPassword({
      Email: this.email || '',
      NewPassword: newPassword,
      ConfirmPassword: confirmPassword,
      Token: this.token || ''
    }).subscribe(
      () => {
        // Password reset successful, handle accordingly (e.g., show success message)
        console.log('Password reset successful');
        this.router.navigate(['/login']);
      },
      (error) => {
        // Handle password reset error (e.g., display error message)
        console.error('Password reset failed', error);
      }
    );
  }
}
