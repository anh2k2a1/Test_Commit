import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../services/material.service';
import { SearchComponent } from '../../../components/search/search.component';
import { CourseRepository } from '../../../repositories/course.repository';
import { AuthService } from '../../../services/auth.service';
import { RouterLink } from '@angular/router';
import { Course, Tag, TransactionType } from '../../../models/course.model';
import { Observable } from 'rxjs';
import { HelperService } from '../../../services/helper.service';
import { AIRecommendationService } from '../../../services/ai-recommendation.service';

@Component({
  selector: 'app-course',
  standalone: true,
  imports: [CommonModule, MaterialModule, SearchComponent, RouterLink],
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css'],
})
export class CourseComponent implements OnInit {
  availableTransactionTypes: TransactionType[] = ['Online', 'Offline', 'Hybrid'];

  courses: Course[] = [];
  filteredCourses: Course[] = [];
  recommendedCourses: Course[] = [];

  availableTags: Tag[] = [
    'Investing',
    'Budgeting',
    'Personal Finance',
    'Debt Management',
    'Saving',
    'Retirement Planning',
    'Stock Market',
    'Cryptocurrency',
    'Financial Literacy',
    'Tax Planning',
    'Real Estate',
    'Wealth Building',
    'Insurance',
    'Beginner',
    'Advanced',
  ];

  constructor(
    private courseRepository: CourseRepository,
    private authService: AuthService,
    private helperService: HelperService,
    private aiRecommendationService: AIRecommendationService

  ) { }

  ngOnInit(): void {

    const types: TransactionType[] = ['Online', 'Offline', 'Hybrid'];
    this.courses = this.courses.map((course, index) => ({
      ...course,
      transactionType: types[index % types.length],
    }));


    this.courseRepository.getCourses().subscribe({
      next: (courses) => {
        this.courses = courses;
        this.filteredCourses = [...courses];
        const userId = this.authService.getUserId();
      },
      error: (error) => console.error('Error fetching courses:', error),
    });
  }
  loadRecommendations() {
    const userId = this.authService.getUserId(); // ✅ Khai báo đúng vị trí
    if (userId) {
      this.aiRecommendationService.getRecommendedCourses(userId, this.courses).subscribe({
        next: (recommended) => (this.recommendedCourses = recommended),
        error: (err) => console.error('AI recommendation error:', err),
      });
    } else {
      console.error('User ID is null, cannot get recommendations.');
    }
  }


  onSearchChanged(criteria: {
    query: string;
    rating: number;
    priceRange: number[];
    selectedTags: string[];
    selectedTransactionTypes: string[];
    selectedTransactionCategories: string[]; // Not used for courses
    dateRange: { start: Date | null; end: Date | null };
  }) {
    this.filteredCourses = this.courses.filter((course) => {
      const matchesQuery =
        !criteria.query ||
        course.title.toLowerCase().includes(criteria.query.toLowerCase()) ||
        course.desc.toLowerCase().includes(criteria.query.toLowerCase());

      const matchesRating = criteria.rating === 0 || course.rating >= criteria.rating;

      const matchesPrice =
        course.price >= criteria.priceRange[0] &&
        course.price <= criteria.priceRange[1];

      const matchesTags =
        criteria.selectedTags.length === 0 ||
        criteria.selectedTags.some((tag) => course.tags.includes(tag as Tag));

      const matchesTransactionType =
        criteria.selectedTransactionTypes.length === 0 ||
        criteria.selectedTransactionTypes.includes(course.transactionType ?? '')


      return (
        matchesQuery &&
        matchesRating &&
        matchesPrice &&
        matchesTags &&
        matchesTransactionType
      );
    });
  }



  getFormattedPrice(price: number): Observable<string> {
    return this.helperService.getFormattedPrice(price);
  }

  generateStars(rating: number): string[] {
    return this.helperService.generateStars(rating);
  }

}
