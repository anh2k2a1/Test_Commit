import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '../../services/material.service';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { TransactionCategories } from '../../models/transaction.model';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, MaterialModule, FormsModule,MatInputModule],
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
  animations: [
    trigger('starAnimation', [
      state('unselected', style({ transform: 'scale(1)', opacity: 0.7 })),
      state('selected', style({ transform: 'scale(1.2)', opacity: 1 })),
      transition('unselected => selected', [animate('200ms ease-out')]),
      transition('selected => unselected', [animate('200ms ease-in')]),
    ]),
  ],
})
export class SearchComponent {
  @Input() placeholder: string = 'Search...';
  @Output() searchTextChange = new EventEmitter<string>();
    searchText: string = '';
//lan1

  @Input() enableCourseFilters: boolean = false;
  @Input() enableTransactionFilters: boolean = false;
  @Input() availableTags: string[] = [];
  @Input() availableTransactionTypes: string[] = [];
  @Input() allTransactionCategories: string[] = [];
  @Output() searchCriteriaChange = new EventEmitter<{
    query: string;
    rating: number;
    priceRange: number[];
    selectedTags: string[];
    selectedTransactionTypes: string[];
    selectedTransactionCategories: string[];
    dateRange: { start: Date | null; end: Date | null };
  }>();

  searchQuery: string = '';
  selectedRating: number = 0;
  priceRange: number[] = [0, 500];
  selectedTags: string[] = [];
  selectedTransactionTypes: string[] = [];
  selectedTransactionCategories: string[] = [];
  dateRange = { start: null as Date | null, end: null as Date | null };
  filteredTransactionCategories: string[] = [];

  ngOnChanges() {
    this.updateFilteredCategories();
  }

  toggleSearch() {
    const searchElement = document.querySelector('.search');
    const filterElement = document.querySelector('.filter');
    const verticalDivider = document.querySelector('.vertical-divider');

    searchElement?.classList.toggle('active');
    verticalDivider?.classList.toggle('active');
    filterElement?.classList.toggle('active');

    this.emitSearchCriteria();
  }

  selectRating(rating: number) {
    this.selectedRating = this.selectedRating === rating ? 0 : rating;
    this.emitSearchCriteria();
  }

  onSearchQueryChange() {
    this.emitSearchCriteria();
  }

  onPriceRangeChange() {
    this.emitSearchCriteria();
  }

  onTagSelectionChange(tags: string[]) {
    this.selectedTags = tags;
    this.emitSearchCriteria();
  }

  onTypeSelectionChange(types: string[]) {
    this.selectedTransactionTypes = types;
    this.updateFilteredCategories();
    this.selectedTransactionCategories = this.selectedTransactionCategories.filter(
      (category) => this.filteredTransactionCategories.includes(category)
    );
    this.emitSearchCriteria();
  }

  onCategorySelectionChange(categories: string[]) {
    this.selectedTransactionCategories = categories;
    this.emitSearchCriteria();
  }

  onDateRangeChange() {
    this.emitSearchCriteria();
  }

  private updateFilteredCategories() {
    if (this.selectedTransactionTypes.length === 0) {
      this.filteredTransactionCategories = [...this.allTransactionCategories];
    } else {
      this.filteredTransactionCategories = this.selectedTransactionTypes
        .map((type) => TransactionCategories[type as keyof typeof TransactionCategories] || [])
        .flat()
        .filter((value, index, self) => self.indexOf(value) === index); // Remove duplicates
    }
  }

  private emitSearchCriteria() {
    this.searchCriteriaChange.emit({
      query: this.searchQuery,
      rating: this.enableCourseFilters ? this.selectedRating : 0,
      priceRange: this.enableCourseFilters ? this.priceRange : [0, Infinity],
      selectedTags: this.enableCourseFilters ? this.selectedTags : [],
      selectedTransactionTypes: this.enableTransactionFilters ? this.selectedTransactionTypes : [],
      selectedTransactionCategories: this.enableTransactionFilters ? this.selectedTransactionCategories : [],
      dateRange: this.enableTransactionFilters ? this.dateRange : { start: null, end: null },
    });
  }
}