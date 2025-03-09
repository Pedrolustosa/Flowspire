import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { UpdateRequest } from '../models/UpdateRequest';
import { User } from '../models/User';
import { NgFor, NgIf } from '@angular/common';
import { LoadingService } from '../services/loading.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [FormsModule, RouterModule, NgIf, NgFor],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: User | null = null;
  updateRequest: UpdateRequest = { fullName: '', roles: [] };

  roleOptions = [
    { id: 0, name: 'Administrador' },
    { id: 1, name: 'Assessor Financeiro' },
    { id: 2, name: 'Cliente' }
  ];

  constructor(
    private authService: AuthService,
    private toastr: ToastrService,
    private loadingService: LoadingService
  ) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.loadingService.setLoading(true);
    this.authService.getCurrentUser().subscribe({
      next: (user) => {
        this.user = user;
        this.updateRequest.fullName = user.fullName;
        this.updateRequest.roles = Array.isArray(user.roles)
          ? user.roles.filter(role => this.roleOptions.some(option => option.id === role))
          : [];
        this.loadingService.setLoading(false);
      },
      error: (err) => {
        this.toastr.error('Erro ao carregar o perfil.', 'Erro');
        this.loadingService.setLoading(false);
      }
    });
  }

  onSubmit(): void {
    if (!this.updateRequest.fullName.trim()) {
      this.toastr.error('O nome completo não pode estar vazio.', 'Erro');
      return;
    }

    this.loadingService.setLoading(true);
    this.authService.updateUser(this.updateRequest).subscribe({
      next: (updatedUser) => {
        this.user = updatedUser;
        this.loadUserProfile();
        this.toastr.success('Perfil atualizado com sucesso!', 'Sucesso');
        this.loadingService.setLoading(false);
      },
      error: (err) => {
        this.toastr.error(err.error?.title || 'Erro ao atualizar o perfil.', 'Erro');
        this.loadingService.setLoading(false);
      }
    });
  }

  // Método para verificar se uma role está selecionada
  isRoleSelected(roleId: number): boolean {
    return this.updateRequest.roles.includes(roleId);
  }

  // Método para alternar a seleção de uma role
  toggleRole(roleId: number): void {
    if (this.isRoleSelected(roleId)) {
      this.updateRequest.roles = this.updateRequest.roles.filter(id => id !== roleId);
    } else {
      this.updateRequest.roles.push(roleId);
    }
  }
}