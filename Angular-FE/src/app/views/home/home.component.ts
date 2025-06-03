import { Component } from '@angular/core';
import { HeaderComponent } from "../../components/header/header.component";
import { FooterComponent } from "../../components/footer/footer.component";
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../../services/material.service';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, FooterComponent, RouterModule, MaterialModule, TranslateModule,CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  features = [
    { icon: 'devices', title: 'HOME.FEATURE_DEVICES', desc: 'HOME.FEATURE_DEVICES_DESC' },
    { icon: 'account_balance', title: 'HOME.FEATURE_RECURRING', desc: 'HOME.FEATURE_RECURRING_DESC' },
    { icon: 'airplanemode_active', title: 'HOME.FEATURE_TRAVEL', desc: 'HOME.FEATURE_TRAVEL_DESC' },
    { icon: 'attach_money', title: 'HOME.FEATURE_SAVING', desc: 'HOME.FEATURE_SAVING_DESC' },
    { icon: 'money_off', title: 'HOME.FEATURE_DEBT', desc: 'HOME.FEATURE_DEBT_DESC' },
    { icon: 'smartphone', title: 'HOME.FEATURE_EASY', desc: 'HOME.FEATURE_EASY_DESC' }
  ];

  comments = [
    { text: 'HOME.COMMENT_1', user: 'Tran Van A' },
    { text: 'HOME.COMMENT_2', user: 'Tran Van B' },
    { text: 'HOME.COMMENT_3', user: 'Tran Van C' },
  ];
}
