import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  imports: [CommonModule, RouterModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css',
})
export class AuthComponent {
  constructor(private router: Router) {}

  get backgroundImage(): string {
    let imageUrl: string;
    if (this.router.url.includes('/auth/forgot-password')) {
      imageUrl = '/images/forgot-password.png';
    } else if (this.router.url.includes('/auth/login')) {
      imageUrl = '/images/login-pho.jpeg';
    } else {
      imageUrl = '/images/register.jpeg';
    }
    return `linear-gradient(#00000080, rgba(0, 0, 0, 0.5)), url(${imageUrl})`;
  }
}
