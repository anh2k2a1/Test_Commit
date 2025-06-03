// services/helper.service.ts
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserRepository } from '../repositories/user.repository';
import { Currency } from '../models/user.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class HelperService {
  constructor(
    private authService: AuthService,
    private userRepository: UserRepository
  ) {}

  getFormattedPrice(price: number): Observable<string> {
    const userId = this.authService.getUserId();
    if (!userId) {
      return of(`$${price.toFixed(2)}`); 
    }
    return this.userRepository.getUserById(userId).pipe(
      map((user) => {
        if (user && user.currency === Currency.VND) {
          return `${(price * 25000).toFixed(0)} VNĐ`;
        }
        return `$${price.toFixed(2)}`;
      })
    );
  }

  generateStars(rating: number): string[] {
    const maxStars = 5;
    const fullStars = Math.floor(rating);
    const hasHalfStar = rating % 1 >= 0.3 && rating % 1 <= 0.7;
    const emptyStars = maxStars - fullStars - (hasHalfStar ? 1 : 0);

    return [
      ...Array(fullStars).fill('full'),
      ...(hasHalfStar ? ['half'] : []),
      ...Array(emptyStars).fill('empty'),
    ];
  }
}