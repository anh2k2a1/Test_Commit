import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule,
  ],
})
export class ForgotPasswordComponent {
  showResetStep = true;
  resetEmailForm: FormGroup;
  newPasswordForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router,
    private authService: AuthService
  ) {
    this.resetEmailForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });

    this.newPasswordForm = this.fb.group({
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
    });
  }

  submitEmail(): void {
    if (this.resetEmailForm.valid) {
      const email = this.resetEmailForm.value.email;
      this.authService.forgotPassword(email).subscribe({
        next: (response) => {
          this.snackBar.open(response, 'Close', { duration: 3000 });
        },
        error: (error) => {
          this.snackBar.open('Error sending reset email', 'Close', {
            duration: 3000,
          });
        },
      });
    }
  }

  changePassword(): void {
    if (this.newPasswordForm.invalid) return;

    const { newPassword, confirmPassword } = this.newPasswordForm.value;
    if (newPassword !== confirmPassword) {
      this.snackBar.open('Passwords do not match', 'Close', { duration: 3000 });
      return;
    }

    this.snackBar.open('Password changed successfully!', 'Close', {
      duration: 3000,
    });
    this.router.navigate(['/login']);
  }

  goToLogin(): void {
    this.router.navigate(['/login']);
  }
}
