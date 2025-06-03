export interface TransactionCategories {
  expense: string[];
  income: string[];
  'debt/loan': string[];
}

export type TransactionType = keyof TransactionCategories;

export interface Transaction {
  id: string;
  note: string | null;
  amount: number;
  createdAt: string;  reminderAt: string | null;
  type: TransactionType;
  category: string;
  
}

export const TransactionCategories: TransactionCategories = {
  expense: [
    'food & beverage',
    'bills & utilities',
    'rentals',
    'water bill',
    'phone bill',
    'electricity bill',
    'gas bill',
    'television bill',
    'internet bill',
    'other utility bills',
    'shopping',
    'personal items',
    'houseware',
    'makeup',
    'family',
    'home maintenance',
    'pets',
    'transportation',
    'vehicle maintenance',
    'health & fitness',
    'medical check-up',
    'fitness',
    'education',
    'entertainment',
    'streaming service',
    'fun money',
    'gift & donations',
    'insurances',
    'investment',
    'other expense',
    'outgoing transfer',
    'pay interest',
    'uncategorized expense',
  ],
  income: [
    'salary',
    'other income',
    'incoming transfer',
    'collect interest',
    'uncategorized income',
  ],
  'debt/loan': ['loan', 'repayment', 'debt collection', 'debt'],
};
