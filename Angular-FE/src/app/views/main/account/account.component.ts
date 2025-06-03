import { Component, OnInit } from '@angular/core';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
  ValidationErrors,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { MaterialModule } from '../../../services/material.service';
import { User } from '../../../models/user.model';
import { AuthService } from '../../../services/auth.service';
import { UserRepository } from '../../../repositories/user.repository';
import { trigger, state, style, transition, animate } from '@angular/animations';

import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [MaterialModule, ReactiveFormsModule, CommonModule,RouterModule],
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css'],
  animations: [
    // Card entrance animation
    trigger('cardAnimation', [
      state('void', style({
        opacity: 0,
        transform: 'translateY(20px)'
      })),
      transition(':enter', [
        animate('300ms cubic-bezier(0.35, 0, 0.25, 1)', style({
          opacity: 1,
          transform: 'translateY(0)'
        }))
      ])
    ]),
    // Form field animations
    trigger('fieldAnimation', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateX(-10px)' }),
        animate('200ms 100ms ease-out', style({ opacity: 1, transform: 'translateX(0)' }))
      ])
    ]),
    // Error message animation
    trigger('errorAnimation', [
      transition(':enter', [
        style({ opacity: 0, height: 0 }),
        animate('200ms ease-out', style({ opacity: 1, height: '*' }))
      ]),
      transition(':leave', [
        animate('150ms ease-in', style({ opacity: 0, height: 0 }))
      ])
    ]),
    // Button hover animation
    trigger('buttonHover', [
      state('normal', style({
        transform: 'scale(1)',
        boxShadow: '0 2px 4px rgba(0,0,0,0.1)'
      })),
      state('hover', style({
        transform: 'scale(1.02)',
        boxShadow: '0 4px 8px rgba(0,0,0,0.15)'
      })),
      transition('normal <=> hover', animate('150ms ease-in-out'))
    ])
  ]
})
export class AccountComponent implements OnInit {
  passwordForm: FormGroup;
  profileError: string | null = null;
  passwordError: string | null = null;
  userId: string | null = null;
  buttonState = 'normal';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private userRepository: UserRepository,
    private router: Router
  ) {
    this.passwordForm = this.fb.group(
      {
        currentPassword: ['', Validators.required],
        newPassword: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', Validators.required],
      },
      { validators: this.passwordMatchValidator }
    );
  }

  ngOnInit() {
    // Get user ID from JWT token
    const jwtHelper = new JwtHelperService();
    const token = localStorage.getItem('jwt_token');
    if (token) {
      try {
        const decodedToken = jwtHelper.decodeToken(token);
        console.log('Decoded Token:', decodedToken); // Debug token contents
        this.userId = decodedToken.nameid;
        console.log('User ID:', this.userId); // Debug user ID
        if (
          this.userId &&
          this.userId !== 'undefined' &&
          /^[0-9a-fA-F]{24}$/.test(this.userId)
        ) {
        } else {
          console.error('Invalid or missing nameid in token:', decodedToken);
          this.profileError = 'Invalid session. Please log in again.';
          localStorage.removeItem('jwt_token'); // Clear invalid token
          this.router.navigate(['/auth/login']);
        }
      } catch (e) {
        console.error('Error decoding token:', e);
        this.profileError = 'Unable to load profile. Please log in again.';
        localStorage.removeItem('jwt_token'); // Clear invalid token
        this.router.navigate(['/auth/login']);
      }
    } else {
      console.error('No JWT token found in localStorage');
      this.profileError = 'Please log in to access your profile';
      this.router.navigate(['/auth/login']);
    }
  }

  passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const newPassword = control.get('newPassword')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;
    return newPassword === confirmPassword ? null : { mismatch: true };
  }

  onChangePassword() {
    if (this.passwordForm.valid) {
      // Add loading animation state
      this.buttonState = 'normal';
      
      const { currentPassword, newPassword } = this.passwordForm.value;
      const email = this.getUserEmailFromToken();

      if (email) {
        this.userRepository
          .changePassword({ email, currentPassword, newPassword })
          .subscribe({
            next: (success) => {
              if (success) {
                this.passwordError = null;
                // You could add a success animation here
                this.passwordForm.reset();
                // Trigger success visual feedback
              } else {
                this.passwordError = 'Failed to change password';
              }
            },
            error: (err) => {
              this.passwordError = err.message || 'Invalid current password';
              console.error('Error changing password:', err);
            },
          });
      } else {
        this.passwordError = 'Unable to identify user';
      }
    }
  }

  private getUserEmailFromToken(): string | null {
    const jwtHelper = new JwtHelperService();
    const token = localStorage.getItem('jwt_token');
    if (token) {
      try {
        const decodedToken = jwtHelper.decodeToken(token);
        return decodedToken.email || null;
      } catch (e) {
        console.error('Error decoding token:', e);
        return null;
      }
    }
    return null;
  }

  onButtonHover(hovering: boolean) {
    this.buttonState = hovering ? 'hover' : 'normal';
  }
}