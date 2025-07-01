import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/user.model';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';
import { environment } from 'src/environments/environment.development';
import { ApiResponse } from 'src/app/shared/models/service-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
// BehaviorSubject to hold the current user state
$user = new BehaviorSubject<User | undefined>(undefined);

  constructor(private httpClient:HttpClient, private cookieService: CookieService) { }
  
register(model: LoginRequest): Observable<ApiResponse<LoginResponse>> {
  return this.httpClient.post<ApiResponse<LoginResponse>>(
    `${environment.localApiUrl}/Auth/Register`,
    {
      email: model.email,
      password: model.password
    }
  );
}

  login(model: LoginRequest): Observable<ApiResponse<LoginResponse>> {
    return this.httpClient.post<ApiResponse<LoginResponse>>(
      `${environment.localApiUrl}/Auth/Login`,
      {
        userName: model.email,
        password: model.password,
      }
    );
  }
  logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authorization', '/');
    this.$user.next(undefined);
  }
  //Set the user in the BehaviorSubject
  setUser(user: User): void {
    //STORE
    this.$user.next(user);
    localStorage.setItem('user-email', user.email);
    localStorage.setItem('user-roles', user.roles.join(','));
  }
  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }
  
  getUser(): User | undefined {
    const email = localStorage.getItem('user-email');
    const roles = localStorage.getItem('user-roles');

    if (email && roles) {
      const user: User = {
        email: email,
        roles: roles.split(','),
      };
      return user;
    }
    return undefined;
  }
}
