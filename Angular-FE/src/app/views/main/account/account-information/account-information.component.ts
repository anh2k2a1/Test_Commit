import { Component, OnInit } from '@angular/core';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../../../services/auth.service';
import { UserRepository } from '../../../../repositories/user.repository';
import { UserService, User } from '../../../../services/user.service';
import { MaterialModule } from '../../../../services/material.service';
@Component({
  selector: 'app-account-information',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule,
    MaterialModule
  ],
  templateUrl: './account-information.component.html',
  styleUrls: ['./account-information.component.css'],
})
export class AccountInformationComponent implements OnInit {
  form!: FormGroup;
  loading = false;
  success = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private userRepo: UserRepository,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^\d{9,10}$/)]],
      birthDate: ['', Validators.required],
      gender: ['', Validators.required],
      bio: ['', Validators.maxLength(200)],
    });

    this.loadUser();
  }

  loadUser() {
    this.userService.getCurrentUser().subscribe(user => {
      this.form.patchValue(user);
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    this.loading = true;
    this.success = false;
    const updatedUser: User = this.form.value;

    this.userService.updateUser(updatedUser).subscribe(() => {
      this.loading = false;
      this.success = true;
      setTimeout(() => this.success = false, 3000);
    });
  }

  onCancel() {
    this.loadUser();
    this.success = false;
  }
}