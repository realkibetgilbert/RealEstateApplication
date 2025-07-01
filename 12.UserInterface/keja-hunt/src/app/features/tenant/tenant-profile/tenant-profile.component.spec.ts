import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TenantProfileComponent } from './tenant-profile.component';

describe('TenantProfileComponent', () => {
  let component: TenantProfileComponent;
  let fixture: ComponentFixture<TenantProfileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TenantProfileComponent]
    });
    fixture = TestBed.createComponent(TenantProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
