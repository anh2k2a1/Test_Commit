import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TranslateService } from '@ngx-translate/core';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet,CommonModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
})
export class AppComponent {
  constructor(private translate: TranslateService) {
  translate.addLangs(['en', 'vi']);
  translate.setDefaultLang('vi');
  const browserLang = translate.getBrowserLang();
  // translate.use(browserLang.match(/en|vi/) ? browserLang : 'vi');
}
}
