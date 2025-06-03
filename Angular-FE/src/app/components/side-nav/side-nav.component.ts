import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../services/material.service';
import { RouterModule } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [
    CommonModule, 
    MaterialModule, 
    RouterModule,
    MatExpansionModule,
    MatListModule
  ],
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent implements OnInit {
  navs = [
    { name: 'Dashboard', route: '/main/dashboard' },
    { name: 'Transaction', route: '/main/transaction' },
    { name: 'Goal', route: '/main/goal' },
    { name: 'Course', route: '/main/course' },
    { 
    name: 'Setting', 
    route: '/main/account',
    isExpanded: false,
    subItems: [
      { name: 'Account', route: '/main/account/account-information' },
      { name: 'ChangePassword', route: '/main/account/change-password' },
      { name: 'Notification', route: '/main/account/notification-settings' },
      { name: 'Security', route: '/main/account/security-privacy-settings' },
      { name: 'Appearance', route: '/main/account/personalization-settings' }
    ]
  },
  ];
  
  username: string = 'Guest';
  email: string = 'No email';
  avatarSrc: string = 'assets/images/default-avatar.png';
  avatarBase64: string | null = null;

  constructor() {}

  ngOnInit() {
    this.loadUserInfo();
  }

  toggleSetting(index: number) {
    this.navs[index].isExpanded = !this.navs[index].isExpanded;
  }

  private loadUserInfo() {
    const jwtHelper = new JwtHelperService();
    const token = localStorage.getItem('jwt_token');
    
    if (!token) {
      console.warn('No JWT token found in localStorage');
      return;
    }

    try {
      const decodedToken = jwtHelper.decodeToken(token);
      this.username = decodedToken.unique_name || 'User';
      this.email = decodedToken.email || 'No email';
    } catch (e) {
      console.error('Error decoding token:', e);
    }
  }

  onImageChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files?.length) {
      const file = input.files[0];
      const reader = new FileReader();

      reader.onload = (e: ProgressEvent<FileReader>) => {
        if (e.target?.result) {
          this.avatarSrc = e.target.result as string;
          this.avatarBase64 = e.target.result as string;
        }
      };

      reader.onerror = () => console.error('Error reading file');
      reader.readAsDataURL(file);
    }
  }
}