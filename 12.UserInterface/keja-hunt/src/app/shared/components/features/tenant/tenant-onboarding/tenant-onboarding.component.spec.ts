import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TenantOnboardingComponent } from './tenant-onboarding.component';

describe('TenantOnboardingComponent', () => {
  let component: TenantOnboardingComponent;
  let fixture: ComponentFixture<TenantOnboardingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TenantOnboardingComponent]
    });
    fixture = TestBed.createComponent(TenantOnboardingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
