import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onFormSubmit(): void {
    if (this.registerForm.invalid) return;

    this.errorMessage = null;
    this.successMessage = null;

    const { email, password } = this.registerForm.value;

    this.authService.register({ email, password }).subscribe({
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
