import { Component } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
selector: 'app-login',
templateUrl: './login.component.html',
styleUrls: ['./login.component.scss']
})
export class LoginComponent {
loginForm!: FormGroup;
errorMessage: string | null = null;
successMessage: string | null = null;
constructor(
  private fb: FormBuilder,
  private authService:AuthService,
  private cookieService:CookieService,
  private router:Router
){
  this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
}
 onFormSubmit(): void {
    if (this.loginForm.invalid) return;

    this.errorMessage = null;
    this.successMessage = null;

    const { email, password } = this.loginForm.value;

    this.authService.login({ email, password }).subscribe({
      next: (response) => {
        if (response.isSuccess) {
          const data = response.data;

          this.cookieService.set(
            'Authorization',
            `Bearer ${data.token}`,
            undefined, '/', undefined, true, 'Strict'
          );

          this.authService.setUser({
            email: data.email,
            roles: data.roles
          });

          this.successMessage = 'Login successful!';
          setTimeout(() => {
            this.successMessage = null;
            this.router.navigateByUrl('/onboarding');
          }, 1500);
        } else {
          this.errorMessage = response.message || 'Login failed.';
          setTimeout(() => (this.errorMessage = null), 3000);
        }
      },
      error: () => {
        this.errorMessage = 'Login failed. Please check your credentials.';
        setTimeout(() => (this.errorMessage = null), 3000);
      }
    });
  }

}
