import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SecurityPrivacySettingsComponent } from './security-privacy-settings.component';

describe('SecurityPrivacySettingsComponent', () => {
  let component: SecurityPrivacySettingsComponent;
  let fixture: ComponentFixture<SecurityPrivacySettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SecurityPrivacySettingsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SecurityPrivacySettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
