import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { AuthService } from './auth.service';
import { Transaction, TransactionCategories } from '../models/transaction.model';
import { ApiUrls } from '../constants/api_url';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BudgetService {
  private budgetUpdated = new Subject<void>();
  budgetUpdated$ = this.budgetUpdated.asObservable();

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  notifyBudgetUpdate() {
    this.budgetUpdated.next();
  }

  getBudget(): Promise<number> {
    const userId = this.authService.getUserId();
    if (!userId) {
      return Promise.resolve(0);
    }
    return new Promise((resolve, reject) => {
      this.http.get<Transaction[]>(`${ApiUrls.transactionUser}/${userId}`).pipe(
        catchError((error) => {
          resolve(0);
          return throwError(() => error);
        })
      ).subscribe({
        next: (transactions) => {
          const totalBudget = transactions.reduce((total, transaction) => {
            const type = this.getTransactionType(transaction.category);
            return type === 'income' ? total + transaction.amount : total - transaction.amount;
          }, 0);
          resolve(totalBudget);
        },
        error: () => {
          resolve(0);
        },
      });
    });
  }

  private getTransactionType(category: string): string {
    for (const [type, categories] of Object.entries(TransactionCategories)) {
      if (categories.includes(category)) {
        return type;
      }
    }
    return 'expense';
  }
}