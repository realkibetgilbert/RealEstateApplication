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
  onFormSubmit():void{
    this.errorMessage = null;
    this.successMessage = null;
    this.authService.login(this.model)
      .subscribe({
        next:(response)=>{
          //set auth cookie
          console.log(response.Email)
          this.cookieService.set('Authorization',`Bearer ${response.Token}`,
            undefined,'/',undefined,true,'Strict'
          );
          this.authService.setUser({
            email:response.Email,
            roles:response.Roles
          });
          this.successMessage = 'Login successful!';
          setTimeout(() => {
            this.successMessage = null;
            this.router.navigateByUrl('/');
          }, 1500); // show success for 1.5 seconds, then redirect
        },
        error: (err) => {
          this.errorMessage = 'Login failed. Please check your credentials.';
          setTimeout(() => {
            this.errorMessage = null;
          }, 3000); // message disappears after 3 seconds
        }
      })
  }
}
