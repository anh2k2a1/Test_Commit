import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';

export interface User {
  name: string;
  email: string;
  phone: string;
  birthDate: string;
  gender: string;
  bio: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userData: User = {
    name: 'Nguyễn Văn A',
    email: 'nguyenvana@example.com',
    phone: '0123456789',
    birthDate: '1990-01-01',
    gender: 'male',
    bio: 'Đây là bio cá nhân.'
  };

  getCurrentUser(): Observable<User> {
    return of(this.userData).pipe(delay(500)); // giả lập gọi API mất 0.5s
  }

  updateUser(user: User): Observable<User> {
    // Giả lập update thành công sau 1s
    return of(user).pipe(delay(1000));
  }
}
