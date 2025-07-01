import { Component } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
selector: 'app-login',
templateUrl: './login.component.html',
styleUrls: ['./login.component.scss']
})
export class LoginComponent {
model:LoginRequest;
errorMessage: string | null = null;
successMessage: string | null = null;
constructor(
  private authService:AuthService,
  private cookieService:CookieService,
  private router:Router
){
  this.model={email:'',password:''}
}
onFormSubmit(): void {
this.errorMessage = null;
this.successMessage = null;

this.authService.login(this.model).subscribe({
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
        this.router.navigateByUrl('/tenant');
      }, 1500);
    } else {
      this.errorMessage = response.message || 'Login failed.';
      setTimeout(() => (this.errorMessage = null), 3000);
    }
  },
  error: (err) => {
    this.errorMessage = 'Login failed. Please check your credentials.';
    setTimeout(() => (this.errorMessage = null), 3000);
  }
});
}

}
