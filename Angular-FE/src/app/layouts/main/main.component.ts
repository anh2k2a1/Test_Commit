import { Component } from '@angular/core';
import { MaterialModule } from '../../services/material.service';
import { HeaderComponent } from '../../components/header/header.component';
import { RouterOutlet } from '@angular/router';
import { MatDrawer } from '@angular/material/sidenav';
import { SideNavComponent } from '../../components/side-nav/side-nav.component';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [MaterialModule, HeaderComponent, SideNavComponent, RouterOutlet],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {
  drawerOpened = true;

  toggleDrawer(drawer: MatDrawer) {
    drawer.toggle();
    this.drawerOpened = !this.drawerOpened;
  }
}
