import { Component, AfterViewInit } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { ChartData, ChartOptions, ChartEvent, ActiveElement } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../../../services/material.service';
import { Transaction } from '../../../models/transaction.model';
import { trigger, transition, style, animate, query, stagger } from '@angular/animations';
import { ShortenIdPipe } from '../../../models/shorten-id';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MaterialModule, BaseChartDirective, RouterModule, CurrencyPipe, DatePipe],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],

    providers: [CurrencyPipe, DatePipe], 

  animations: [
    trigger('fadeInUp', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(20px)' }),
        animate('600ms cubic-bezier(0.35, 0, 0.25, 1)', 
          style({ opacity: 1, transform: 'translateY(0)' }))
      ])
    ]),
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('400ms ease-out', 
          style({ opacity: 1 }))
      ])
    ]),
    trigger('staggerIn', [
      transition('* => *', [
        query(':enter', [
          style({ opacity: 0, transform: 'translateX(-20px)' }),
          stagger('100ms', [
            animate('300ms ease-out', 
              style({ opacity: 1, transform: 'translateX(0)' }))
          ])
        ], { optional: true })
      ])
    ])
  ]
})
export class DashboardComponent implements AfterViewInit {
  transactions: Transaction[] = [
    {
      id: '1',
      note: 'Monthly salary',
      amount: 15000000,
      createdAt: '2023-05-01',
      reminderAt: null,
      type: 'income',
      category: 'Salary',
    },
    {
      id: '2',
      note: 'Lunch with friends at a nice restaurant',
      amount: -500000,
      createdAt: '2023-05-02',
      reminderAt: null,
      type: 'expense',
      category: 'Food & Beverage',
    },
    {
      id: '3',
      note: 'Car loan installment',
      amount: -2000000,
      createdAt: '2023-05-03',
      reminderAt: '2023-05-28',
      type: 'debt/loan',
      category: 'Loan Payment',
    },
    {
      id: '4',
      note: 'Freelance project payment from client XYZ',
      amount: 3000000,
      createdAt: '2023-05-10',
      reminderAt: null,
      type: 'income',
      category: 'Freelance Income',
    },
    {
      id: '5',
      note: 'New clothes shopping',
      amount: -1200000,
      createdAt: '2023-05-15',
      reminderAt: null,
      type: 'expense',
      category: 'Shopping',
    }
  ];

  displayedColumns: string[] = ['id', 'note', 'amount', 'date'];
  metrics = [
    { label: 'Avg. Monthly Growth', value: 38.33, currency: '' },
    { label: 'Avg. Monthly Income', value: 45332, currency: 'USD' },
    { label: 'Avg. Monthly Expense', value: -28300, currency: 'USD' }
  ];

  pieChartHoverValue: number | null = null;
  pieChartHoverLabel: string | null = null;

  pieChartData: ChartData<'pie', number[], string> = {
    labels: ['Expense', 'Income', 'Debt/Loan'],
    datasets: [{
      data: [0, 0, 0],
      backgroundColor: [
        'rgba(255, 99, 132, 0.8)',
        'rgba(54, 162, 235, 0.8)',
        'rgba(255, 206, 86, 0.8)'
      ],
      borderColor: [
        'rgba(255, 99, 132, 1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)'
      ],
      borderWidth: 1,
      hoverBackgroundColor: [
        'rgba(255, 99, 132, 1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)'
      ],
      hoverBorderColor: '#fff',
      hoverBorderWidth: 2
    }],
  };

  pieChartOptions: ChartOptions<'pie'> = {
    responsive: true,
    maintainAspectRatio: false,
    cutout: '70%',
    plugins: {
      legend: { 
        position: 'right',
        labels: {
          usePointStyle: true,
          pointStyle: 'circle',
          padding: 20,
          font: {
            family: 'Roboto, sans-serif',
            size: 12
          },
          color: '#666'
        }
      },
      tooltip: {
        enabled: false,
        external: (context) => {
          // Custom tooltip handling
        }
      }
    },
    animation: {
      animateScale: true,
      animateRotate: true
    },
    onHover: (event: ChartEvent, elements: ActiveElement[], chart: any) => {
      if (elements.length > 0) {
        const index = elements[0].index;
        const dataset = chart.data.datasets[0].data[index];
        const label = chart.data.labels[index];
        this.pieChartHoverValue = dataset;
        this.pieChartHoverLabel = label;
      } else {
        this.pieChartHoverValue = null;
        this.pieChartHoverLabel = null;
      }
    }
  };

  lineChartData: ChartData<'line'> = {
    labels: [],
    datasets: [
      {
        label: 'Expense',
        data: [],
        borderColor: 'rgba(255, 99, 132, 1)',
        backgroundColor: 'rgba(255, 99, 132, 0.2)',
        borderWidth: 2,
        tension: 0.4,
        fill: true,
        pointBackgroundColor: 'rgba(255, 99, 132, 1)',
        pointBorderColor: '#fff',
        pointHoverRadius: 6,
        pointHoverBackgroundColor: 'rgba(255, 99, 132, 1)',
        pointHoverBorderColor: '#fff',
        pointHoverBorderWidth: 2,
        pointRadius: 4,
        pointHitRadius: 10,
      },
      {
        label: 'Income',
        data: [],
        borderColor: 'rgba(54, 162, 235, 1)',
        backgroundColor: 'rgba(54, 162, 235, 0.2)',
        borderWidth: 2,
        tension: 0.4,
        fill: true,
        pointBackgroundColor: 'rgba(54, 162, 235, 1)',
        pointBorderColor: '#fff',
        pointHoverRadius: 6,
        pointHoverBackgroundColor: 'rgba(54, 162, 235, 1)',
        pointHoverBorderColor: '#fff',
        pointHoverBorderWidth: 2,
        pointRadius: 4,
        pointHitRadius: 10,
      },
    ],
  };

  lineChartOptions: ChartOptions<'line'> = {
    responsive: true,
    maintainAspectRatio: false,
    scales: {
      y: {
        beginAtZero: false,
        grid: {
          color: 'rgba(0, 0, 0, 0.05)',
        },
        ticks: {
          callback: (value) => this.currencyPipe.transform(value as number, 'USD', 'symbol', '1.0-0'),
          font: {
            family: 'Roboto, sans-serif'
          }
        }
      },
      x: {
        grid: {
          display: false,
        },
        ticks: {
          font: {
            family: 'Roboto, sans-serif'
          }
        }
      }
    },
    plugins: {
      legend: {
        position: 'top',
        labels: {
          usePointStyle: true,
          padding: 20,
          font: {
            family: 'Roboto, sans-serif',
            size: 12
          }
        }
      },
      tooltip: {
        backgroundColor: 'rgba(0, 0, 0, 0.8)',
        titleFont: {
          family: 'Roboto, sans-serif',
          size: 14,
          weight: 'bold'
        },
        bodyFont: {
          family: 'Roboto, sans-serif',
          size: 12
        },
        padding: 12,
        cornerRadius: 8,
        displayColors: true,
        callbacks: {
          label: (context) => {
            const label = context.dataset.label || '';
            const value = context.raw as number;
            return `${label}: ${this.currencyPipe.transform(value)}`;
          }
        }
      }
    },
    interaction: {
      intersect: false,
      mode: 'index',
    },
    animation: {
      duration: 1000,
      easing: 'easeOutQuart'
    }
  };

  budgetItems = [
    { name: 'travel', label: 'Travel Savings', amount: 20000000, total: 50000000, color: '#42A5F5' },
    { name: 'motorbike', label: 'Motorbike Purchase', amount: 30000000, total: 50000000, color: '#66BB6A' },
    { name: 'tuition', label: 'Tuition Fee', amount: 15000000, total: 20000000, color: '#FFA726' },
    { name: 'investment', label: 'Stock Investment', amount: 5000000, total: 10000000, color: '#AB47BC' },
  ];
  pieChartSummary: any;

  constructor(private currencyPipe: CurrencyPipe, private datePipe: DatePipe) {}

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.updateCharts();
    }, 300);
  }

  getPieTotal(): number {
    return this.pieChartSummary.reduce((sum: any, item: { value: any; }) => sum + item.value, 0);
  }

  onPieHover(event: any): void {
    if (event.active.length > 0) {
      const index = event.active[0].index;
      this.pieChartHoverValue = this.pieChartData.datasets[0].data[index] as number;
      this.pieChartHoverLabel = this.pieChartData.labels?.[index] as string;
    } else {
      this.pieChartHoverValue = null;
      this.pieChartHoverLabel = null;
    }
  }

  onPieClick(event: any): void {
    console.log('Pie chart clicked', event);
    // Add any custom click handling here
  }

  onLineHover(event: any): void {
    // Custom line chart hover handling
  }

  onRowClick(row: any): void {
    console.log('Row clicked', row);
    // Add navigation or detail view logic
  }

  updateCharts(): void {
    this.updatePieChart(this.transactions);
    this.updateLineChart(this.transactions);
    this.updateSummaryData();
  }

  updateSummaryData(): void {
    this.pieChartSummary = [
      { label: 'Income', value: 18000000, color: 'rgba(54, 162, 235, 0.8)' },
      { label: 'Expense', value: 3700000, color: 'rgba(255, 99, 132, 0.8)' },
      { label: 'Debt/Loan', value: 2000000, color: 'rgba(255, 206, 86, 0.8)' }
    ];
  }

  updatePieChart(transactions: Transaction[]): void {
    const aggregates = transactions.reduce(
      (acc, t) => {
        const type = this.deriveTransactionType(t.category);
        if (type === 'expense') {
          acc.expense += Math.abs(t.amount);
        } else if (type === 'income') {
          acc.income += t.amount;
        } else if (type === 'debt/loan') {
          acc.debtLoan += Math.abs(t.amount);
        }
        return acc;
      },
      { expense: 0, income: 0, debtLoan: 0 }
    );

    const hasData = aggregates.expense > 0 || aggregates.income > 0 || aggregates.debtLoan > 0;

    this.pieChartData = {
      labels: hasData ? ['Expense', 'Income', 'Debt/Loan'] : ['No Data'],
      datasets: [
        {
          ...this.pieChartData.datasets[0],
          data: hasData ? [aggregates.expense, aggregates.income, aggregates.debtLoan] : [1]
        }
      ]
    };
  }

  updateLineChart(transactions: Transaction[]): void {
    const months = Array.from({ length: 6 }, (_, i) => {
      const date = new Date();
      date.setMonth(date.getMonth() - i);
      return date.toLocaleString('default', { month: 'short', year: 'numeric' });
    }).reverse();

    const expensesByMonth: number[] = new Array(6).fill(0);
    const incomesByMonth: number[] = new Array(6).fill(0);

    transactions.forEach((t) => {
      const date = new Date(t.createdAt);
      const monthYear = date.toLocaleString('default', { month: 'short', year: 'numeric' });
      const index = months.indexOf(monthYear);
      if (index !== -1) {
        const type = this.deriveTransactionType(t.category);
        if (type === 'expense') {
          expensesByMonth[index] += Math.abs(t.amount);
        } else if (type === 'income') {
          incomesByMonth[index] += t.amount;
        }
      }
    });

    this.lineChartData = {
      labels: months,
      datasets: [
        {
          ...this.lineChartData.datasets[0],
          data: expensesByMonth
        },
        {
          ...this.lineChartData.datasets[1],
          data: incomesByMonth
        }
      ]
    };
  }

  deriveTransactionType(category: string): string {
    if (!category) return 'expense';
    const lowerCategory = category.toLowerCase();
    if (lowerCategory.includes('income')) {
      return 'income';
    } else if (lowerCategory.includes('debt') || lowerCategory.includes('loan')) {
      return 'debt/loan';
    } else {
      return 'expense';
    }
  }

  getProgress(amount: number, total: number): number {
    return (amount / total) * 100;
  }
}