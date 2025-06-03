import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Course } from '../models/course.model';
import { ApiUrls } from '../constants/api_url';

@Injectable({
  providedIn: 'root',
})
export class CourseRepository {
  constructor(private http: HttpClient) {}

  // Get all courses
  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(ApiUrls.course).pipe(
      map((courses) => courses || []),
      catchError(this.handleError)
    );
  }

  // Get a course by ID
  getCourseById(id: string): Observable<Course | undefined> {
    return this.http.get<Course>(`${ApiUrls.course}/${id}`).pipe(
      map((course) => course || undefined),
      catchError(this.handleError)
    );
  }

  private handleError(error: any): Observable<never> {
    console.error('API Error:', error);
    const errorMessage =
      error.error?.message || 'An error occurred; please try again later.';
    return throwError(() => new Error(errorMessage));
  }
}