import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../../services/material.service';
import { FormsModule } from '@angular/forms';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
@Component({
  selector: 'app-notification-settings',
  standalone: true,
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    MatSlideToggleModule
  ],
  templateUrl: './notification-settings.component.html',
  styleUrls: ['./notification-settings.component.css'],
})
export class NotificationSettingsComponent {
  emailNotifications = true;
  pushNotifications = false;
  smsNotifications = false;

  onToggleEmail() {
    this.emailNotifications = !this.emailNotifications;
  }

  onTogglePush() {
    this.pushNotifications = !this.pushNotifications;
  }

  onToggleSms() {
    this.smsNotifications = !this.smsNotifications;
  }

  onSave() {
    alert('Notification settings saved!');
  }
}
