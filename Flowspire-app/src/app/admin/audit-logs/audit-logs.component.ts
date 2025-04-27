import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuditLog, AuditLogService } from '../../services/audit-log.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-audit-logs',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './audit-logs.component.html',
  styleUrls: ['./audit-logs.component.scss']
})
export class AuditLogsComponent implements OnInit {
  auditLogs: AuditLog[] = [];
  loading = false;
  currentPage = 1;
  pageSize = 10;
  totalCount = 0;

  constructor(
    private auditLogService: AuditLogService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.fetchAuditLogs();
  }

  fetchAuditLogs(): void {
    this.loading = true;

    this.auditLogService.getAuditLogs(this.currentPage, this.pageSize).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.auditLogs = response.data.data;
          this.totalCount = response.data.totalCount;

          if (response.message) {
            this.toastr.success(response.message, 'Sucesso');
          }
        } else if (response.errors && response.errors.length > 0) {
          response.errors.forEach((error: string) => {
            this.toastr.error(error, 'Erro');
          });
        }
        this.loading = false;
      },
      error: (err) => {
        if (err.error?.errors && Array.isArray(err.error.errors)) {
          err.error.errors.forEach((error: string) => {
            this.toastr.error(error, 'Erro');
          });
        } else if (err.error?.message) {
          this.toastr.error(err.error.message, 'Erro');
        } else {
          this.toastr.error('Erro desconhecido.', 'Erro');
        }
        this.loading = false;
      }
    });
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.fetchAuditLogs();
  }

  get totalPages(): number {
    return Math.ceil(this.totalCount / this.pageSize);
  }

  getDisplayedPages(): number[] {
    const pagesToShow = 5;
    const pages: number[] = [];
  
    let startPage = Math.max(this.currentPage - Math.floor(pagesToShow / 2), 1);
    let endPage = startPage + pagesToShow - 1;
  
    if (endPage > this.totalPages) {
      endPage = this.totalPages;
      startPage = Math.max(endPage - pagesToShow + 1, 1);
    }
  
    for (let page = startPage; page <= endPage; page++) {
      pages.push(page);
    }
  
    return pages;
  }  
}
