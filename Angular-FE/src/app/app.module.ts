// src/app/app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { routes } from './app.routes';
import { AppComponent } from './app.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { MaterialModule } from './services/material.service';



import { GoalComponent } from './views/main/goal/goal.component';
import { AccountComponent } from './views/main/account/account.component';
import { ChangePasswordComponent } from './views/main/account/change-password/change-password.component';
import { NotificationSettingsComponent } from './views/main/account/notification-settings/notification-settings.component';
import { PersonalizationSettingsComponent } from './views/main/account/personalization-settings/personalization-settings.component';
import { SecurityPrivacySettingsComponent } from './views/main/account/security-privacy-settings/security-privacy-settings.component';



// Hàm khởi tạo TranslateHttpLoader
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './images/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    ChangePasswordComponent,
    NotificationSettingsComponent,
    PersonalizationSettingsComponent,
    SecurityPrivacySettingsComponent
    ],


  imports: [
    GoalComponent,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes),
    MaterialModule,
    TranslateModule.forRoot({
      defaultLanguage: 'vi',
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
  ],
  
    exports: [GoalComponent],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
