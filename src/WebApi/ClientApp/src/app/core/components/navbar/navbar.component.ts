import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.sass'],
})
export class NavbarComponent {
  public menuIsOpen = false;
  public configIsOpen = false;

  public toogleMenu(): void {
    this.menuIsOpen = !this.menuIsOpen;
  }

  public configToogle(): void {
    this.configIsOpen = !this.configIsOpen;
  }
}
