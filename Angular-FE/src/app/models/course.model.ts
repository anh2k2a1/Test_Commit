export type Tag =
  | 'Investing'
  | 'Budgeting'
  | 'Personal Finance'
  | 'Debt Management'
  | 'Saving'
  | 'Retirement Planning'
  | 'Stock Market'
  | 'Cryptocurrency'
  | 'Financial Literacy'
  | 'Tax Planning'
  | 'Real Estate'
  | 'Wealth Building'
  | 'Insurance'
  | 'Beginner'
  | 'Advanced';

export interface CourseItem {
  id: number;
  title: string;
  subTitle: string;
  videoUrl: string;
  isLearning: boolean;
}

export type TransactionType = 'Online' | 'Offline' | 'Hybrid';

export interface Course {
  id: string;
  title: string;
  desc: string;
  rating: number;
  reviewCount: number;
  price: number;
  discount: number;
  imageUrl: string;
  tags: Tag[];
  courseList: CourseItem[];
  lock: boolean;

  transactionType?: TransactionType;
}
