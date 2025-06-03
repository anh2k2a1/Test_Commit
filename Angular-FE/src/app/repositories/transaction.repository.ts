import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { ApiUrls } from '../constants/api_url';
import { Transaction } from '../models/transaction.model';
import { AuthService } from '../services/auth.service';
import { BudgetService } from '../services/budget.service';

@Injectable({
  providedIn: 'root',
})
export class TransactionRepository {
  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private budgetService: BudgetService
  ) {}

  getTransactions(): Observable<Transaction[]> {
    const userId = this.authService.getUserId();
    if (!userId) {
      return throwError(() => new Error('User ID not found in JWT token'));
    }
    const url = `${ApiUrls.transactionUser}/${userId}`;
    return this.http.get<Transaction[]>(url).pipe(catchError(this.handleError));
  }

  addTransaction(transaction: Transaction): Observable<Transaction> {
    const userId = this.authService.getUserId();
    if (!userId) {
      return throwError(() => new Error('User ID not found in JWT token'));
    }
    const transactionPayload = {
      userId,
      amount: transaction.amount,
      category: transaction.category,
      note: transaction.note || null,
      createdAt: transaction.createdAt || new Date().toISOString(),
      reminderAt: transaction.reminderAt || null,
    };
    return this.http
      .post<Transaction>(ApiUrls.transaction, transactionPayload)
      .pipe(
        switchMap((newTransaction) => {
          if (!newTransaction || !newTransaction.id) {
            return throwError(
              () => new Error('Invalid transaction response: id missing')
            );
          }
          this.budgetService.notifyBudgetUpdate();
          return this.http.get<Transaction>(
            `${ApiUrls.transaction}/${newTransaction.id}`
          );
        }),
        catchError(this.handleError)
      );
  }

  updateTransaction(
    id: string,
    transaction: Transaction,
    originalTransaction: Transaction
  ): Observable<Transaction> {
    const userId = this.authService.getUserId();
    if (!userId) {
      return throwError(() => new Error('User ID not found in JWT token'));
    }
    const url = `${ApiUrls.transaction}/${id}`;
    const transactionPayload = {
      id: id,
      amount: transaction.amount,
      category: transaction.category,
      note: transaction.note || '',
      reminderAt: transaction.reminderAt || null,
    };
    return this.http.put<Transaction>(url, transactionPayload).pipe(
      switchMap((updatedTransaction) => {
        this.budgetService.notifyBudgetUpdate();
        return this.http.get<Transaction>(`${ApiUrls.transaction}/${id}`);
      }),
      catchError(this.handleError)
    );
  }

  deleteTransaction(id: string, transaction: Transaction): Observable<void> {
    const userId = this.authService.getUserId();
    if (!userId) {
      return throwError(() => new Error('User ID not found in JWT token'));
    }
    const url = `${ApiUrls.transaction}/${id}`;
    return this.http.delete<void>(url).pipe(
      switchMap(() => {
        this.budgetService.notifyBudgetUpdate();
        return new Observable<void>((observer) => {
          observer.next();
          observer.complete();
        });
      }),
      catchError(this.handleError)
    );
  }

  private handleError(error: any): Observable<never> {
    const errorMessage =
      error.error?.message ||
      error.message ||
      'An error occurred; please try again later.';
    return throwError(() => new Error(errorMessage));
  }
}