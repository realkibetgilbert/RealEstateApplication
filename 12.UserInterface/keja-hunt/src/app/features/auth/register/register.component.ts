import { Component } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  model: LoginRequest = { email: '', password: '' };
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onFormSubmit(): void {
  this.errorMessage = null;
  this.successMessage = null;

  this.authService.register(this.model).subscribe({
    next: (response) => {
      if (response.isSuccess) {
        this.successMessage = 'Account created successfully! Redirecting to login...';
        setTimeout(() => {
          this.successMessage = null;
          this.router.navigateByUrl('/login');
        }, 1500);
      } else {
        this.errorMessage = response.message || 'Registration failed.';
        setTimeout(() => (this.errorMessage = null), 3000);
      }
    },
    error: () => {
      this.errorMessage = 'Registration failed. Please try again.';
      setTimeout(() => (this.errorMessage = null), 3000);
    }
  });
}

}
