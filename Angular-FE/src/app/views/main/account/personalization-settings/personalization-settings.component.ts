import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatRadioModule, MatRadioChange } from '@angular/material/radio';
import { MatSliderModule } from '@angular/material/slider';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-personalization-settings',
  standalone: true,
  templateUrl: './personalization-settings.component.html',
  styleUrls: ['./personalization-settings.component.css'],
  imports: [
    CommonModule,
    FormsModule,
    MatRadioModule,
    MatSliderModule,
    MatButtonModule
  ],
})
export class PersonalizationSettingsComponent {
  theme = 'light';
  fontSize = 14;

  onThemeChange(event: MatRadioChange) {
    this.theme = event.value;
  }

onFontSizeChange(value: number) {
  this.fontSize = value;
}

  onSave() {
    alert('Personalization settings saved!');
  }
}
