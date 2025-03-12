import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';
import { CategoryDTO } from '../models/Transaction';
import { CategoryService } from '../services/category.service';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categories: CategoryDTO[] = [];
  newCategory: CategoryDTO = { id: 0, name: '', userId: '' };
  selectedCategory: CategoryDTO | null = null;
  isEditing: boolean = false;

  constructor(
    private categoryService: CategoryService,
    private authService: AuthService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) {
    const user = this.authService.getCurrentUserValue();
    if (user && user.id) {
      this.newCategory.userId = user.id; // Definir userId do usuário autenticado
    } else {
      console.warn('User not available, fetching current user...');
      this.authService.getCurrentUser().subscribe({
        next: (user) => {
          this.newCategory.userId = user.id;
          console.log('User ID set to:', user.id);
        },
        error: (err) => {
          this.toastr.error('Erro ao buscar usuário atual.', 'Erro');
        }
      });
    }
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.spinner.show();
    this.categoryService.getCategories().subscribe({
      next: (data) => {
        this.categories = data;
        this.spinner.hide();
      },
      error: (err) => {
        this.toastr.error(err.error?.Error || 'Erro ao carregar categorias.', 'Erro');
        this.spinner.hide();
      }
    });
  }

  addOrUpdateCategory(): void {
    if (!this.newCategory.name || this.newCategory.name.trim() === '') {
      this.toastr.error('O nome da categoria é obrigatório.', 'Erro');
      return;
    }

    this.spinner.show();
    if (this.isEditing && this.selectedCategory) {
      this.selectedCategory.name = this.newCategory.name;
      this.selectedCategory.userId = this.newCategory.userId; // Garantir que o userId seja consistente
      this.categoryService.updateCategory(this.selectedCategory).subscribe({
        next: (response) => {
          this.toastr.success('Categoria atualizada com sucesso!', 'Sucesso');
          this.resetForm();
          this.loadCategories();
          this.spinner.hide();
        },
        error: (err) => {
          this.toastr.error(err.error?.Error || 'Erro ao atualizar categoria.', 'Erro');
          console.error('Update error:', err); // Log para depuração
          this.spinner.hide();
        }
      });
    } else {
      this.categoryService.addCategory(this.newCategory).subscribe({
        next: (response) => {
          this.toastr.success('Categoria adicionada com sucesso!', 'Sucesso');
          this.resetForm();
          this.loadCategories();
          this.spinner.hide();
        },
        error: (err) => {
          this.toastr.error(err.error?.Error || 'Erro ao adicionar categoria.', 'Erro');
          console.error('Add error:', err); // Log para depuração
          this.spinner.hide();
        }
      });
    }
  }

  editCategory(category: CategoryDTO): void {
    this.selectedCategory = { ...category };
    this.newCategory = { ...category };
    this.isEditing = true;
  }

  resetForm(): void {
    const userId = this.authService.getCurrentUserValue()?.id || '';
    this.newCategory = { id: 0, name: '', userId };
    this.selectedCategory = null;
    this.isEditing = false;
  }
}