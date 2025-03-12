import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CategoryDTO } from '../models/Transaction';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = `${environment.apiUrl}/category`;

  constructor(private http: HttpClient) {}

  addCategory(categoryDto: CategoryDTO): Observable<any> {
    return this.http.post(`${this.apiUrl}`, categoryDto);
  }

  getCategories(): Observable<CategoryDTO[]> {
    return this.http.get<CategoryDTO[]>(`${this.apiUrl}/user`);
  }

  updateCategory(categoryDto: CategoryDTO): Observable<any> {
    return this.http.put(`${this.apiUrl}`, categoryDto);
  }
}