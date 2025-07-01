import { Component } from '@angular/core';
import { CreateTenantProfile } from '../models/create-tenant-profile.model';
import { TenantService } from '../services/tenant.service';
import { AuthService } from '../../auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tenant-profile',
  templateUrl: './tenant-profile.component.html',
  styleUrls: ['./tenant-profile.component.scss']
})
export class TenantProfileComponent {
model: CreateTenantProfile = {
    fullName: '',
    phoneNumber: '',
    idNumber: '',
    preferredLocation: '',
    gender: '',
    email: ''
  };
  
  isEditMode = false; 
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(
    private tenantService: TenantService,
    private authService: AuthService,
    private router: Router
  ) {}

ngOnInit(): void {
    const user = this.authService.getUser();
    if (user?.email) {
      this.model.email = user.email;

      this.tenantService.getTenantProfile(user.email).subscribe({
        next: (res) => {
          if (res.isSuccess && res.data) {
            this.model = res.data;
            this.isEditMode = true;
          }
        },
        error: () => {
          // silent fail, show blank form
          this.isEditMode = false;
        }
      });
    }
  }
  onFormSubmit(): void {
    if (this.isEditMode) {
      this.updateTenantProfile();
    } else {
      this.createTenantProfile();
    }
  }
  
  private createTenantProfile(): void {
    this.tenantService.createTenantProfile(this.model).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.successMessage = 'Profile created successfully!';
          setTimeout(() => {
            this.successMessage = null;
            this.router.navigateByUrl('/properties');
          }, 1500);
        } else {
          this.errorMessage = res.message || 'Failed to create profile.';
          setTimeout(() => (this.errorMessage = null), 3000);
        }
      },
      error: () => {
        this.errorMessage = 'Server error. Please try again.';
        setTimeout(() => (this.errorMessage = null), 3000);
      }
    });
  }
private updateTenantProfile(): void {
    this.tenantService.updateTenantProfile(this.model).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.successMessage = 'Profile updated successfully!';
          setTimeout(() => {
            this.successMessage = null;
            this.router.navigateByUrl('/properties');
          }, 1500);
        } else {
          this.errorMessage = res.message || 'Failed to update profile.';
          setTimeout(() => (this.errorMessage = null), 3000);
        }
      },
      error: () => {
        this.errorMessage = 'Server error. Please try again.';
        setTimeout(() => (this.errorMessage = null), 3000);
      }
    });
  }
}
