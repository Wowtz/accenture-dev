import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  items: MenuItem[] | undefined;
  activeItem: MenuItem | undefined;

  constructor(private router: Router) {}

  ngOnInit() {
    this.items = [
      { label: 'Empresas', icon: 'pi pi-building', routerLink: '/empresas' },
      { label: 'Fornecedores', icon: 'pi pi-user', routerLink: '/fornecedores' },
    ];

    this.activeItem = this.items.find(item => this.router.isActive(item.routerLink, true));  }
}
