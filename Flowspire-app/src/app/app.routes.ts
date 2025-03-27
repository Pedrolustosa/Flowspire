import { Routes } from '@angular/router';
import { authGuard } from './services/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {
    path: 'login',
    loadComponent: () => import('./login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'register',
    loadComponent: () => import('./register/register.component').then(m => m.RegisterComponent)
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./dashboard/dashboard.component').then(m => m.DashboardComponent),
    canActivate: [authGuard]
  },
  {
    path: 'transactions',
    loadComponent: () => import('./transactions/transactions.component').then(m => m.TransactionComponent),
    canActivate: [authGuard]
  },
  {
    path: 'profile',
    loadComponent: () => import('./profile/profile.component').then(m => m.ProfileComponent),
    canActivate: [authGuard]
  },
  {
    path: 'category',
    loadComponent: () => import('./category/category.component').then(m => m.CategoryComponent),
    canActivate: [authGuard]
  },
  {
    path: 'chat',
    loadComponent: () => import('./chat/chat.component').then(m => m.ChatComponent),
    canActivate: [authGuard]
  }
];