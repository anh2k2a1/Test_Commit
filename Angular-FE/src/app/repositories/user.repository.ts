import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Currency, Language, User } from '../models/user.model';
import { ApiUrls } from '../constants/api_url';

export interface AuthResponse {
  token: string;
  success: boolean;
  errors?: string[];
}

@Injectable({
  providedIn: 'root',
})
export class UserRepository {
  constructor(private http: HttpClient) {}

  register(user: User): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(ApiUrls.register, {
        email: user.email,
        userName: user.userName,
        password: user.password,
      })
      .pipe(
        map((response) => ({
          ...response,
          success: response.success ?? true,
        })),
        catchError(this.handleError)
      );
  }

  login(credentials: {
    email: string;
    password: string;
  }): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(ApiUrls.login, credentials).pipe(
      map((response) => ({
        ...response,
        success: response.success ?? true,
      })),
      catchError(this.handleError)
    );
  }

  getUserById(id: string): Observable<User> {
    return this.http
      .get<User>(`${ApiUrls.users}/${id}`)
      .pipe(catchError(this.handleError));
  }

  updateUser(
    id: string,
    updateData: { userName: string; currency: Currency; language: Language }
  ): Observable<boolean> {
    return this.http.put<boolean>(`${ApiUrls.users}/${id}`, updateData).pipe(
      map(() => true),
      catchError(this.handleError)
    );
  }

  changePassword(credentials: {
    email: string;
    currentPassword: string;
    newPassword: string;
  }): Observable<boolean> {
    return this.http.post<boolean>(ApiUrls.changePassword, credentials).pipe(
      map(() => true),
      catchError(this.handleError)
    );
  }

  forgotPassword(email: string): Observable<string> {
    return this.http
      .post(ApiUrls.forgotPassword, { email }, { responseType: 'text' as 'text' })
      .pipe(catchError(this.handleError));
  }

  private handleError(error: any): Observable<never> {
    console.error('API Error:', error);
    const errorMessage =
      error.error?.message || 'An error occurred; please try again later.';
    return throwError(() => new Error(errorMessage));
  }
}
