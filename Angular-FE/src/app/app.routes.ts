import { Routes } from '@angular/router';
import { HomeComponent } from './views/home/home.component';
import { LoginComponent } from './views/auth/login/login.component';
import { TransactionComponent } from './views/main/transaction/transaction.component';
import { MainComponent } from './layouts/main/main.component';
import { AuthGuard } from './guards/auth-guard.guard';
import { ContactComponent } from './views/contact/contact.component';
import { RegisterComponent } from './views/auth/register/register.component';
import { DashboardComponent } from './views/main/dashboard/dashboard.component';
import { GoalComponent } from './views/main/goal/goal.component';
import { CourseComponent } from './views/main/course/course.component';
import { AccountComponent } from './views/main/account/account.component';
import path from 'path';
import { CourseDetailComponent } from './views/main/course-detail/course-detail.component';
import { ForgotPasswordComponent } from './views/auth/forgot-password/forgot-password.component';
import { AuthComponent } from './layouts/auth/auth.component';

// Contact/
import { ChangePasswordComponent } from './views/main/account/change-password/change-password.component';
import { NotificationSettingsComponent } from './views/main/account/notification-settings/notification-settings.component';
import { PersonalizationSettingsComponent } from './views/main/account/personalization-settings/personalization-settings.component';
import { SecurityPrivacySettingsComponent } from './views/main/account/security-privacy-settings/security-privacy-settings.component';
import { AccountInformationComponent } from './views/main/account/account-information/account-information.component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'contact', component: ContactComponent },
  {
    path: 'auth',
    component: AuthComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
    ],
  },

  {
    path: 'main',
    component: MainComponent,
    // canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent, pathMatch: 'full' },
      { path: 'transaction', component: TransactionComponent },
      {
        path: 'course',
        component: CourseComponent,
      },
      { path: 'course/:id', component: CourseDetailComponent },
      { path: 'goal', component: GoalComponent },
      {
        path: 'account',
        component: AccountComponent,
        children: [
          { path: '', redirectTo: 'account-information', pathMatch: 'full' },
          { path: 'account-information', component: AccountInformationComponent },
          { path: 'change-password', component: ChangePasswordComponent },
          { path: 'notification-settings', component: NotificationSettingsComponent },
          { path: 'personalization-settings', component: PersonalizationSettingsComponent },
          { path: 'security-privacy-settings', component: SecurityPrivacySettingsComponent }
        ]
      },
    ],
  },
];
