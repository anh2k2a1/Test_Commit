import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthResponse, UserRepository } from '../repositories/user.repository';
import { User } from '../models/user.model';
import { jwtDecode } from 'jwt-decode';

interface JwtPayload {
  nameid?: string;
  [key: string]: any;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  getCurrentUserId() {
    throw new Error('Method not implemented.');
  }
  constructor(private userRepository: UserRepository, private router: Router) {}

  register(user: User): Observable<AuthResponse> {
    return this.userRepository.register(user).pipe(
      tap((response) => {
        if (response.success && response.token) {
          localStorage.setItem('jwt_token', response.token);
        }
      })
    );
  }

  login(email: string, password: string): Observable<AuthResponse> {
    return this.userRepository.login({ email, password }).pipe(
      tap((response) => {
        if (response.success && response.token) {
          localStorage.setItem('jwt_token', response.token);
        }
      })
    );
  }

  forgotPassword(email: string): Observable<string> {
    return this.userRepository.forgotPassword(email);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('jwt_token');
  }

  logout(): void {
    localStorage.removeItem('jwt_token');
    this.router.navigate(['/auth/login']);
  }

  getUserId(): string | null {
    const token = localStorage.getItem('jwt_token');
    if (token) {
      try {
        const decoded: JwtPayload = jwtDecode(token);
        return decoded.nameid || null;
      } catch (error) {
        return null;
      }
    }
    return null;
  }
}