import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../services/category.service';
import { AuthService } from '../services/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { CategoryDTO } from '../models/Transaction';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categoryForm!: FormGroup;
  categories: CategoryDTO[] = [];
  isEditing: boolean = false;
  selectedCategoryId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private authService: AuthService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadCategories();
    const user = this.authService.getCurrentUserValue();
    if (user && user.id) {
      this.categoryForm.patchValue({ userId: user.id });
    } else {
      this.authService.getCurrentUser().subscribe({
        next: (user) => this.categoryForm.patchValue({ userId: user.id }),
        error: () => this.toastr.error('Erro ao buscar usuário atual.', 'Erro')
      });
    }
  }

  private initializeForm(): void {
    this.categoryForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
      isDefault: [false],
      sortOrder: [0],
      userId: ['', Validators.required]
    });
  }

  loadCategories(): void {
    this.spinner.show();
    this.categoryService.getUserCategories().subscribe({
      next: (data: CategoryDTO[]) => {
        this.categories = data;
        this.spinner.hide();
      },
      error: (err: any) => {
        this.toastr.error(err.error?.Error || 'Erro ao carregar categorias.', 'Erro');
        this.spinner.hide();
      }
    });
  }

  addOrUpdateCategory(): void {
    if (this.categoryForm.invalid) {
      this.toastr.error('Preencha todos os campos obrigatórios.', 'Erro');
      return;
    }
    this.spinner.show();
    const payload = this.categoryForm.value;
    if (this.isEditing && this.selectedCategoryId !== null) {
      this.categoryService.updateCategory(this.selectedCategoryId, payload).subscribe({
        next: () => {
          this.toastr.success('Categoria atualizada com sucesso!', 'Sucesso');
          this.resetForm();
          this.loadCategories();
          this.spinner.hide();
        },
        error: (err: any) => {
          this.toastr.error(err.error?.Error || 'Erro ao atualizar categoria.', 'Erro');
          this.spinner.hide();
        }
      });
    } else {
      this.categoryService.createCategory(payload).subscribe({
        next: (response: CategoryDTO) => {
          this.toastr.success('Categoria adicionada com sucesso!', 'Sucesso');
          this.resetForm();
          this.loadCategories();
          this.spinner.hide();
        },
        error: (err: any) => {
          this.toastr.error(err.error?.Error || 'Erro ao adicionar categoria.', 'Erro');
          this.spinner.hide();
        }
      });
    }
  }

  editCategory(category: CategoryDTO): void {
    this.isEditing = true;
    this.selectedCategoryId = category.id!;
    this.categoryForm.patchValue({
      name: category.name,
      description: category.description,
      isDefault: category.isDefault,
      sortOrder: category.sortOrder,
      userId: category.userId
    });
  }

  resetForm(): void {
    this.categoryForm.reset();
    const userId = this.authService.getCurrentUserValue()?.id || '';
    this.categoryForm.patchValue({ userId });
    this.isEditing = false;
    this.selectedCategoryId = null;
  }
}