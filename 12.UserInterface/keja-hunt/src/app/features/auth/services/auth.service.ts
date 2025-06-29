import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/user.model';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';
import { environment } from 'src/environments/environment.development';
import { ServiceResponse } from 'src/app/shared/models/service-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
// BehaviorSubject to hold the current user state
$user = new BehaviorSubject<User | undefined>(undefined);

  constructor(private httpClient:HttpClient, private cookieService: CookieService) { }

  login(model: LoginRequest): Observable<LoginResponse> {
    return this.httpClient.post<ServiceResponse<LoginResponse>>(
      `${environment.localApiUrl}/Auth/Login`,
      {
        userName: model.email,
        password: model.password,
      }
    ).pipe(
      // Map the response to extract the LoginResponse
      map(response => {
        if (response.isSuccess && response.data) {
          // If login is successful, return the LoginResponse
          return response.data;
        } else {
          // If login fails, throw an error
          throw new Error(response.message || 'Login failed');
        }
      })
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
