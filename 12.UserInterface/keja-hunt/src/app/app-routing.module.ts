import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { TenantProfileComponent } from './features/tenant/tenant-profile/tenant-profile.component';
import { TenantOnboardingComponent } from './shared/components/features/tenant/tenant-onboarding/tenant-onboarding.component';

const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'tenant', component: TenantProfileComponent },
  { path: 'onboarding', component: TenantOnboardingComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
