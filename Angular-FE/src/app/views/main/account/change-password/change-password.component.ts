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
import { Router, RouterModule } from '@angular/router';
import { MaterialModule } from '../../../../services/material.service';
import { AuthService } from '../../../../services/auth.service';
import { UserRepository } from '../../../../repositories/user.repository';
import { trigger, state, style, transition, animate } from '@angular/animations';


@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    MaterialModule
  ],
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css'],
  animations: [
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
    trigger('fieldAnimation', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateX(-10px)' }),
        animate('200ms 100ms ease-out', style({ opacity: 1, transform: 'translateX(0)' }))
      ])
    ]),
    trigger('errorAnimation', [
      transition(':enter', [
        style({ opacity: 0, height: 0 }),
        animate('200ms ease-out', style({ opacity: 1, height: '*' }))
      ]),
      transition(':leave', [
        animate('150ms ease-in', style({ opacity: 0, height: 0 }))
      ])
    ]),
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
export class ChangePasswordComponent implements OnInit {
  passwordForm: FormGroup;
  passwordError: string | null = null;
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

  ngOnInit() {}

  passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const newPassword = control.get('newPassword')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;
    return newPassword === confirmPassword ? null : { mismatch: true };
  }

  onChangePassword() {
    if (this.passwordForm.valid) {
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
                this.passwordForm.reset();
                // You might want to add a success notification here
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