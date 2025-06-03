import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { MatDialog } from '@angular/material/dialog';
import { CourseRepository } from '../../../repositories/course.repository';
import { AuthService } from '../../../services/auth.service';
import { Course, CourseItem } from '../../../models/course.model';
import { Observable, of } from 'rxjs';
import { TransactionDialogComponent } from '../../../components/transaction-dialog/transaction-dialog.component';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../services/material.service';
import { HelperService } from '../../../services/helper.service';

@Component({
  selector: 'app-course-detail',
  standalone: true,
  imports: [CommonModule, MaterialModule],
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css'],
})
export class CourseDetailComponent implements OnInit {
  course: Course | undefined;
  isPurchased: boolean = false; // Giữ biến này nếu cần cho logic mua khóa học
  courseItems: CourseItem[] = [];
  currentVideoUrl: SafeResourceUrl | undefined;
    


  constructor(
    private route: ActivatedRoute,
    private courseRepository: CourseRepository,
    private authService: AuthService,
    private helperService: HelperService,
    private sanitizer: DomSanitizer,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    // const courseId = this.route.snapshot.paramMap.get('id')!;
    // console.log('Course ID from route:', courseId); // Debug log
    // this.courseRepository.getCourseById(courseId).subscribe({
    //   next: (course) => {
    //     this.course = course;
    //     if (this.course) {
    //       // Chỉ kiểm tra trường lock để hiển thị danh sách video
    //       this.updateDisplayedVideos();
    //       this.setCurrentVideo(0);
    //     } else {
    //       console.error('Course not found for ID:', courseId);
    //     }
    //   },
    //   error: (error) => console.error('Error fetching course:', error),
    // });
  }

  updateDisplayedVideos(): void {
    if (this.course) {
      // Chỉ kiểm tra trường lock thay vì kiểm tra isPurchased
      if (!this.course.lock) {
        this.isPurchased = true;
        this.courseItems = [...this.course.courseList];
      } else {
        this.isPurchased = false;
        this.courseItems = [this.course.courseList[0]];
      }
    }
  }

  setCurrentVideo(index: number): void {
    if (this.course && this.courseItems[index]) {
      this.courseItems.forEach((item) => (item.isLearning = false));
      this.courseItems[index].isLearning = true;
      const videoId = this.getYouTubeVideoId(this.courseItems[index].videoUrl);
      const embedUrl = `https://www.youtube.com/embed/${videoId}?autoplay=0`;
      this.currentVideoUrl = this.sanitizer.bypassSecurityTrustResourceUrl(embedUrl);
    }
  }

  getYouTubeVideoId(url: string): string {
    const regex = /(?:v=)([a-zA-Z0-9_-]+)/;
    const match = url.match(regex);
    return match ? match[1] : '';
  }

  selectVideo(index: number): void {
    this.setCurrentVideo(index);
  }

  startLearning(): void {
    this.setCurrentVideo(0);
    if (this.courseItems[0]) {
      const videoId = this.getYouTubeVideoId(this.courseItems[0].videoUrl);
      const embedUrl = `https://www.youtube.com/embed/${videoId}?autoplay=1`;
      this.currentVideoUrl = this.sanitizer.bypassSecurityTrustResourceUrl(embedUrl);
    }
  }

  getFormattedPrice(price: number): Observable<string> {
    return this.helperService.getFormattedPrice(price);
  }

  generateStars(rating: number): string[] {
    return this.helperService.generateStars(rating);
  }
}