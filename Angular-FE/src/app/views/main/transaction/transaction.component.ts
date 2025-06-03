import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../services/material.service';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Transaction, TransactionCategories, TransactionType } from '../../../models/transaction.model';
import { SearchComponent } from '../../../components/search/search.component';
import { TransactionRepository } from '../../../repositories/transaction.repository';
import { AuthService } from '../../../services/auth.service';
import { BudgetService } from '../../../services/budget.service';

@Component({
  selector: 'app-transaction',
  standalone: true,
  imports: [CommonModule, MaterialModule, ReactiveFormsModule, SearchComponent],
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css'],
})
export class TransactionComponent implements OnInit {
  transactions: Transaction[] = [];
  filteredTransactions: Transaction[] = [];
  showForm = false;
  transactionForm: FormGroup;
  editingTransactionId: string | null = null;
  editingTransaction: Transaction | null = null;
  formError: string | null = null;
  submitted = false;
  transactionTypes: TransactionType[] = Object.keys(TransactionCategories) as TransactionType[];
  categories: string[] = [];
  availableTransactionTypes: string[] = this.transactionTypes;
  allTransactionCategories: string[] = Object.values(TransactionCategories).flat();

  constructor(
    private fb: FormBuilder,
    private transactionRepository: TransactionRepository,
    private authService: AuthService,
    private budgetService: BudgetService
  ) {
    this.transactionForm = this.fb.group({
      note: ['', Validators.maxLength(150)],
      amount: [0, [Validators.required, Validators.min(0.01)]],
      date: [new Date(), Validators.required],
      remind: [null],
      type: ['', Validators.required],
      category: ['', Validators.required],
    });

    this.transactionForm.get('type')?.valueChanges.subscribe((type: string) => {
      if (this.isTransactionType(type)) {
        this.categories = TransactionCategories[type];
        this.transactionForm.get('category')?.setValue('');
      } else {
        this.categories = [];
        this.transactionForm.get('category')?.setValue('');
      }
    });
  }

  ngOnInit() {
    // this.loadTransactions();
  }

  private loadTransactions() {
    this.transactionRepository.getTransactions().subscribe({
      next: (transactions) => {
        this.transactions = transactions.map(t => ({
          ...t,
          type: this.getTransactionType(t.category),
        }));
        this.filteredTransactions = [...this.transactions];
      },
      error: (error) => {
        this.formError = error.message || 'Error loading transactions.';
      },
    });
  }

  private isTransactionType(type: string): type is TransactionType {
    return Object.keys(TransactionCategories).includes(type);
  }

  private getTransactionType(category: string): TransactionType {
    for (const [type, categories] of Object.entries(TransactionCategories)) {
      if (categories.includes(category)) {
        return type as TransactionType;
      }
    }
    return 'expense';
  }

  onSearchChanged(criteria: {
    query: string;
    rating: number;
    priceRange: number[];
    selectedTags: string[];
    selectedTransactionTypes: string[];
    selectedTransactionCategories: string[];
    dateRange: { start: Date | null; end: Date | null };
  }) {
    this.filteredTransactions = this.transactions.filter((transaction) => {
      const matchesQuery =
        !criteria.query ||
        (transaction.note?.toLowerCase().includes(criteria.query.toLowerCase()) ||
          transaction.category.toLowerCase().includes(criteria.query.toLowerCase()) ||
          transaction.type.toLowerCase().includes(criteria.query.toLowerCase()));

      const matchesTypes =
        criteria.selectedTransactionTypes.length === 0 ||
        criteria.selectedTransactionTypes.includes(transaction.type);

      const matchesCategories =
        criteria.selectedTransactionCategories.length === 0 ||
        criteria.selectedTransactionCategories.includes(transaction.category);

      const transactionDate = transaction.createdAt
        ? new Date(transaction.createdAt)
        : new Date();
      const matchesDateRange =
        !criteria.dateRange.start ||
        !criteria.dateRange.end ||
        (transactionDate >= criteria.dateRange.start &&
          transactionDate <= criteria.dateRange.end);

      return matchesQuery && matchesTypes && matchesCategories && matchesDateRange;
    });
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    this.formError = null;
    this.submitted = false;
    if (!this.showForm) {
      this.resetForm();
    }
  }

  addTransaction(): void {
    this.submitted = true;
    this.formError = null;
    this.transactionForm.markAllAsTouched();

    if (this.transactionForm.valid) {
      const formValue = this.transactionForm.value;
      const createdAt = formValue.date ? new Date(formValue.date).toISOString() : new Date().toISOString();
      const reminderAt = formValue.remind ? new Date(formValue.remind).toISOString() : null;

      if (isNaN(new Date(createdAt).getTime())) {
        this.formError = 'Invalid date selected.';
        return;
      }
      if (reminderAt && isNaN(new Date(reminderAt).getTime())) {
        this.formError = 'Invalid remind date selected.';
        return;
      }

      const transaction: Transaction = {
        id: '', // Omit ID for new transactions
        note: formValue.note || null,
        amount: formValue.amount,
        createdAt: createdAt,
        reminderAt: reminderAt,
        type: formValue.type,
        category: formValue.category,
      };

      if (this.editingTransactionId !== null && this.editingTransaction) {
        transaction.id = this.editingTransactionId;
        this.transactionRepository.updateTransaction(this.editingTransactionId, transaction, this.editingTransaction).subscribe({
          next: () => {
            this.loadTransactions();
            this.resetForm();
            this.showForm = false;
            this.submitted = false;
            this.budgetService.notifyBudgetUpdate();
          },
          error: (error) => {
            this.formError = error.message || 'Error updating transaction.';
          },
        });
      } else {
        this.transactionRepository.addTransaction(transaction).subscribe({
          next: () => {
            this.loadTransactions();
            this.resetForm();
            this.showForm = false;
            this.submitted = false;
            this.budgetService.notifyBudgetUpdate();
          },
          error: (error) => {
            this.formError = error.message || 'Error adding transaction.';
          },
        });
      }
    } else {
      const invalidControls = Object.keys(this.transactionForm.controls)
        .filter(key => this.transactionForm.controls[key].invalid);
      this.formError = `Please check: ${invalidControls.join(', ')}`;
    }
  }

  editTransaction(transaction: Transaction): void {
    this.editingTransactionId = transaction.id || null;
    this.editingTransaction = { ...transaction };
    this.showForm = true;
    this.formError = null;
    this.submitted = false;

    this.transactionForm.patchValue({
      note: transaction.note ?? '',
      amount: transaction.amount,
      date: transaction.createdAt ? new Date(transaction.createdAt) : new Date(),
      remind: transaction.reminderAt ? new Date(transaction.reminderAt) : null,
      type: transaction.type,
      category: transaction.category,
    });

    this.categories = TransactionCategories[transaction.type];
  }

  deleteTransaction(id: string): void {
    const transaction = this.transactions.find(t => t.id === id);
    if (!transaction) {
      this.formError = 'Transaction not found.';
      return;
    }
    this.transactionRepository.deleteTransaction(id, transaction).subscribe({
      next: () => {
        this.loadTransactions();
        this.budgetService.notifyBudgetUpdate();
      },
      error: (error) => {
        this.formError = error.message || 'Error deleting transaction.';
      },
    });
  }

  resetForm(): void {
    this.transactionForm.reset({
      note: '',
      amount: 0,
      date: new Date(),
      remind: null,
      type: '',
      category: '',
    });
    this.editingTransactionId = null;
    this.editingTransaction = null;
    this.categories = [];
    this.submitted = false;
  }

  getAmountColor(type: TransactionType): string {
    return type === 'income' ? 'green' : 'red';
  }

  getAmountPrefix(type: TransactionType): string {
    return type === 'income' ? '+' : '-';
  }

  getIconForCategory(category: string): string {
    const iconMap: { [key: string]: string } = {
      'food & beverage': 'restaurant',
      'bills & utilities': 'receipt_long',
      rentals: 'home',
      'water bill': 'water_drop',
      'phone bill': 'phone',
      'electricity bill': 'bolt',
      'gas bill': 'local_gas_station',
      'television bill': 'tv',
      'internet bill': 'wifi',
      'other utility bills': 'miscellaneous_services',
      shopping: 'shopping_cart',
      'personal items': 'person',
      houseware: 'kitchen',
      makeup: 'face',
      family: 'family_restroom',
      'home maintenance': 'home_repair_service',
      pets: 'pets',
      transportation: 'directions_car',
      'vehicle maintenance': 'car_repair',
      'health & fitness': 'fitness_center',
      'medical check-up': 'medical_services',
      fitness: 'directions_run',
      education: 'school',
      entertainment: 'theater_comedy',
      'streaming service': 'movie',
      'fun money': 'celebration',
      'gift & donations': 'card_giftcard',
      insurances: 'security',
      investment: 'trending_up',
      'other expense': 'money_off',
      'outgoing transfer': 'send',
      'pay interest': 'percent',
      'uncategorized expense': 'category',
      salary: 'attach_money',
      'other income': 'account_balance',
      'incoming transfer': 'call_received',
      'collect interest': 'percent',
      'uncategorized income': 'category',
      loan: 'monetization_on',
      repayment: 'payment',
      'debt collection': 'account_balance_wallet',
      debt: 'money_off',
    };
    return iconMap[category.toLowerCase()] || 'category';
  }
}