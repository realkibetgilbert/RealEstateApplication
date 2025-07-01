import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TenantProfileComponent } from 'src/app/features/tenant/tenant-profile/tenant-profile.component';

@Component({
  selector: 'app-tenant-onboarding',
  templateUrl: './tenant-onboarding.component.html',
  styleUrls: ['./tenant-onboarding.component.scss']
})
export class TenantOnboardingComponent implements OnInit {
tenantProfileForm!: FormGroup;
  paymentForm!: FormGroup;
  selectedProperty: any; // Set this from login or route
  @ViewChild(TenantProfileComponent) tenantProfileComp!: TenantProfileComponent;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.tenantProfileForm = this.fb.group({
      fullName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      idNumber: ['', Validators.required],
      preferredLocation: ['', Validators.required],
      gender: ['', Validators.required],
      email: ['', Validators.required]
    });
  }
  submitProfile() {
    if (this.tenantProfileForm.valid) {
      this.tenantProfileComp.onFormSubmit();
      // The stepper will move to next step when (completed) is emitted from child
    }
  }
  
  submitPayment() {
    // Call child component's submit logic if needed
  }

  submitAll() {
    // Final submission logic
  }
}
