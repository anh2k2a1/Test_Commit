import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalizationSettingsComponent } from './personalization-settings.component';

describe('PersonalizationSettingsComponent', () => {
  let component: PersonalizationSettingsComponent;
  let fixture: ComponentFixture<PersonalizationSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PersonalizationSettingsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonalizationSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
