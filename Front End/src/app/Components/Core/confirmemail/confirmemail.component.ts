import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoginService } from '../../../Services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-confirmemail',
  templateUrl: './confirmemail.component.html',
  styleUrls: ['./confirmemail.component.css']
})
export class ConfirmemailComponent implements OnInit {
  email: string | null;
  token: string | null;

  constructor(private route: ActivatedRoute, private loginService: LoginService, private router: Router) {
    this.email = this.route.snapshot.queryParamMap.get('email');
    this.token = this.route.snapshot.queryParamMap.get('token');
  }

  ngOnInit(): void { }

  onSubmit(): void {
    // Check if passwords match

    // Call the service method to reset password
    this.loginService.confirmEmail({
      Email: this.email || '',
      Token: this.token || ''
    }).subscribe(
      () => {
        // Password reset successful, handle accordingly (e.g., show success message)
        console.log('email Confirmation successful');
        this.router.navigate(['/login']);
      },
      (error) => {
        // Handle password reset error (e.g., display error message)
        console.error('Email ConfirmationUrl failed', error);
      }
    );
  }
}
