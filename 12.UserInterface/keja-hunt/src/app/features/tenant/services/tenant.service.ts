import { Injectable } from '@angular/core';
import { CreateTenantProfile } from '../models/create-tenant-profile.model';
import { ApiResponse } from 'src/app/shared/models/service-response.model';
import { Observable } from 'rxjs/internal/Observable';
import { TenantProfileToDisplay } from '../models/tenant-profile-display.model';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TenantService {

 constructor(private httpClient: HttpClient) {}
 
  createTenantProfile(profile: CreateTenantProfile): Observable<ApiResponse<TenantProfileToDisplay>> {
    return this.httpClient.post<ApiResponse<TenantProfileToDisplay>>(
      `${environment.localApiUrl}/tenant-profile`,
      profile
    );
  }
  getTenantProfile(email: string): Observable<ApiResponse<TenantProfileToDisplay>> {
  const encodedEmail = encodeURIComponent(email); // Optional but safe
  return this.httpClient.get<ApiResponse<TenantProfileToDisplay>>(
    `${environment.localApiUrl}/tenant-profile/${encodedEmail}`
  );
}



updateTenantProfile(profile: CreateTenantProfile): Observable<ApiResponse<TenantProfileToDisplay>> {
  return this.httpClient.put<ApiResponse<TenantProfileToDisplay>>(
    `${environment.localApiUrl}/tenant-profile`,
    profile
  );
}

}
