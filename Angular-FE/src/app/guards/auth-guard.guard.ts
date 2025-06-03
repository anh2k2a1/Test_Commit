import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.authService.isAuthenticated()) {
      return true; // Cho phép truy cập nếu đã xác thực
    } else {
      // Chuyển hướng đến /login với returnUrl
      this.router.navigate(['/auth/login'], { queryParams: { returnUrl: state.url } });
      return false; // Từ chối truy cập
    }
  }
}
