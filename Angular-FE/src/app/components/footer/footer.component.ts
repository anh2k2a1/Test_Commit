import { Component } from '@angular/core';
import { MaterialModule } from '../../services/material.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [MaterialModule, CommonModule],
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {
  readonly slogan = {
    title: 'Take your finances to the next level!',
    subtitle: "Don't hesitate, money matters."
  };
  
  readonly copyright = '© 2025 Finalty, JSC. All rights reserved.';
  
  readonly links = [
  'About us', 'Career', 'Blog', 'Status', 'Privacy Policy', 'Terms of Service'
  ];
}