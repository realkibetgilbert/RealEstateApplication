import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TenantService } from '../services/tenant.service';
import { AuthService } from '../../auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tenant-profile',
  templateUrl: './tenant-profile.component.html',
  styleUrls: ['./tenant-profile.component.scss']
})
export class TenantProfileComponent implements OnInit {
  isEditMode = false;
  successMessage: string | null = null;
  errorMessage: string | null = null;
  @Input() form!: FormGroup;
  @Output() completed = new EventEmitter<void>();
  constructor(
    private fb: FormBuilder,
    private tenantService: TenantService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    
    const user = this.authService.getUser();
    if (user?.email) {
      this.form.patchValue({ email: user.email });

      this.tenantService.getTenantProfile(user.email).subscribe({
        next: (res) => {
          if (res.isSuccess && res.data) {
            this.isEditMode = true;
            this.form.patchValue(res.data);
          }
        },
        error: () => {
          this.isEditMode = false;
        }
      });
    }
  }
 onFormSubmit(): void {
    if (this.form.invalid) return;

    const profile = this.form.value;

    if (this.isEditMode) {
      this.tenantService.updateTenantProfile(profile).subscribe({
        next: (res) => {
          if (res.isSuccess) {
            this.successMessage = 'Profile updated successfully!';
            setTimeout(() => {
              this.successMessage = null;
              this.completed.emit();
            }, 1000);
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
    } else {
      this.tenantService.createTenantProfile(profile).subscribe({
        next: (res) => {
          if (res.isSuccess) {
            this.successMessage = 'Profile created successfully!';
            setTimeout(() => {
              this.successMessage = null;
              this.completed.emit();
            }, 1000);
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
  }
}
