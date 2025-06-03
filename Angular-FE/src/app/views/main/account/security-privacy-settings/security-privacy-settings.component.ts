import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';

@Component({
  selector: 'app-security-privacy-settings',
  standalone: true,
  templateUrl: './security-privacy-settings.component.html',
  styleUrls: ['./security-privacy-settings.component.css'],
  imports: [
    CommonModule,
    FormsModule,
    MatCheckboxModule
  ],
})
export class SecurityPrivacySettingsComponent {
  twoFactorAuth = false;
  loginAlerts = true;
  dataSharing = false;

  onSave() {
    alert('Security and privacy settings saved!');
  }
}