const baseUrl = 'http://localhost:5101/api/';

export const ApiUrls = {
  register: `${baseUrl}Users`,
  login: `${baseUrl}Users/login`,
  users: `${baseUrl}Users`,
  changePassword: `${baseUrl}Users/change-password`,
  forgotPassword: `${baseUrl}Users/forgot-password`,
  transactionUser: `${baseUrl}Transactions/user`,
  transaction: `${baseUrl}Transactions`,
  budgetUser: `${baseUrl}Budgets/user`,
  budget: `${baseUrl}Budgets`,
  course: `${baseUrl}FinancialCourses`,
};