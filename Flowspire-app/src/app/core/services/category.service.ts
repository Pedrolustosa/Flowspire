import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable, map } from 'rxjs';
import { CategoryDTO } from '../../shared/models/Transaction';
import { ApiResponse } from '../../shared/models/api-response';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = `${environment.apiUrl}/category`;

  constructor(private http: HttpClient) {}

  createCategory(category: CategoryDTO): Observable<CategoryDTO> {
    return this.http.post<ApiResponse<CategoryDTO>>(`${this.apiUrl}`, category).pipe(
      map(res => res.data)
    );
  }

  getCategoryById(id: number): Observable<CategoryDTO> {
    return this.http.get<ApiResponse<CategoryDTO>>(`${this.apiUrl}/${id}`).pipe(
      map(res => res.data)
    );
  }

  getUserCategories(): Observable<CategoryDTO[]> {
    return this.http.get<ApiResponse<CategoryDTO[]>>(`${this.apiUrl}/user`).pipe(
      map(res => res.data)
    );
  }

  updateCategory(id: number, category: CategoryDTO): Observable<void> {
    return this.http.put<ApiResponse<null>>(`${this.apiUrl}/${id}`, category).pipe(
      map(() => void 0)
    );
  }

  deleteCategory(id: number): Observable<void> {
    return this.http.delete<ApiResponse<null>>(`${this.apiUrl}/${id}`).pipe(
      map(() => void 0)
    );
  }
}