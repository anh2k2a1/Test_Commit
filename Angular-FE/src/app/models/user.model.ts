export enum Currency {
  USD = 'USD',
  VND = 'VNĐ'
}

export enum Language {
  English = 'English',
  Vietnamese = 'Tiếng Việt'
}

export interface User {
  id?: string;
  email: string;
  userName: string;
  password?: string;
  currency?: Currency;
  language?: Language;
  createdAt?: Date;
}