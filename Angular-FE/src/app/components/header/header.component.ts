import { Component, OnInit, OnDestroy, Inject, Input, Output, EventEmitter, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser, CommonModule } from '@angular/common';
import { Router, NavigationEnd, RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { AuthService } from '../../services/auth.service';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { MaterialModule } from '../../services/material.service';
import { BudgetService } from '../../services/budget.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, MaterialModule, RouterModule, TranslateModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  @Input() drawerOpened: boolean = true;
  @Output() toggleDrawer = new EventEmitter<void>();
  isMobile: boolean = false;
  currentLang = 'vi';
  isMobileMenuOpen = false;
  isHeaderVisible: boolean = true;
  currentRoute: string = '';
  budget: number = 0;
  isHomeOrContact: boolean = false;
  private lastScrollTop: number = 0;
  private routeSub: Subscription | null = null;
  private budgetSub: Subscription | null = null;
  private isBrowser: boolean;
  private boundHandleScroll: () => void;
private ticking = false;

  constructor(
    private translate: TranslateService,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object,
    private authService: AuthService,
    private budgetService: BudgetService
  ) {
    this.isBrowser = isPlatformBrowser(this.platformId);
    this.translate.addLangs(['en', 'vi']);
    this.translate.setDefaultLang('vi');
    const savedLang = localStorage.getItem('lang') || 'vi';
    this.currentLang = savedLang;
    this.translate.use(this.currentLang);
    this.boundHandleScroll = this.handleScroll.bind(this);
      this.detectScreenSize = this.detectScreenSize.bind(this);

  }

  ngOnInit() {
    this.currentRoute = this.router.url;
    this.isHomeOrContact = ['/home', '/contact'].includes(this.currentRoute);
      window.addEventListener('resize', this.detectScreenSize);

    this.detectScreenSize();
    window.addEventListener('resize', this.detectScreenSize.bind(this));

  this.routeSub = this.router.events
  .pipe(filter(event => event instanceof NavigationEnd))
  .subscribe((event) => {
    const navEnd = event as NavigationEnd;
    this.currentRoute = navEnd.urlAfterRedirects;
    this.isHomeOrContact = ['/home', '/contact'].includes(this.currentRoute);
    this.isMobileMenuOpen = false; // Đóng menu khi route thay đổi
  });

    if (this.isBrowser) {
      window.addEventListener('scroll', this.boundHandleScroll);
    }

    this.updateBudget();

    this.budgetSub = this.budgetService.budgetUpdated$.subscribe(() => {
      this.updateBudget();
    });
  }

  ngOnDestroy() {
    if (this.isBrowser) {
      window.removeEventListener('scroll', this.boundHandleScroll);
    }
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
    if (this.budgetSub) {
      this.budgetSub.unsubscribe();
    }
  window.removeEventListener('resize', this.detectScreenSize);
  }

  toggleMobileMenu() {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
  }

  toggleLanguage() {
    this.currentLang = this.currentLang === 'vi' ? 'en' : 'vi';
    this.translate.use(this.currentLang);
    localStorage.setItem('lang', this.currentLang);
  }

  private detectScreenSize() {
    this.isMobile = window.innerWidth <= 800;
  }

private handleScroll(): void {
  if (!this.ticking) {
    window.requestAnimationFrame(() => {
      const st = window.pageYOffset || document.documentElement.scrollTop;
      this.isHeaderVisible = st <= this.lastScrollTop || st <= 0;
      this.lastScrollTop = st <= 0 ? 0 : st;
      this.ticking = false;
    });
    this.ticking = true;
  }
}


  private async updateBudget() {
    this.budget = await this.budgetService.getBudget();
  }

  toggleDrawerEmit() {
    this.toggleDrawer.emit();
  }

  navigateToHome(): void {
    this.router.navigate(['home']);
  }

  logout(): void {
    this.authService.logout();
  }

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }
}