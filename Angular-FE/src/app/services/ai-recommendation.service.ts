import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { delay, map } from 'rxjs/operators';
import { Course } from '../models/course.model';

@Injectable({
  providedIn: 'root',
})
export class AIRecommendationService {
  constructor() {}

  getRecommendedCourses(userId: string, allCourses: Course[]): Observable<Course[]> {
    // Giả lập logic AI gợi ý theo tag và rating
    const topTags = ['Investing', 'Saving', 'Budgeting'];
    return of(allCourses).pipe(
      delay(1000), // giả lập delay gọi API AI
      map(courses =>
        courses
          .filter(course => course.rating >= 4)
          .sort((a, b) => {
            const aScore = a.tags.filter(tag => topTags.includes(tag)).length;
            const bScore = b.tags.filter(tag => topTags.includes(tag)).length;
            return bScore - aScore;
          })
          .slice(0, 4)
      )
    );
  }
}
