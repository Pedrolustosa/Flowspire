import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
  ValidationErrors,
  ReactiveFormsModule
} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { RegisterCustomerRequest } from '../../../shared/models/RegisterCustomerRequest';
import { NgIf } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, BsDatepickerModule, NgIf],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  private initializeForm(): void {
    this.registerForm = this.fb.group(
      {
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        phoneNumber: ['', Validators.required],
        birthDate: [null, Validators.required],
        gender: ['', Validators.required],
        addressLine1: ['', Validators.required],
        addressLine2: [''],
        city: ['', Validators.required],
        state: ['', Validators.required],
        country: ['', Validators.required],
        postalCode: ['', Validators.required],
        password: ['', Validators.required],
        confirmPassword: ['', Validators.required]
      },
      { validators: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;
    if (password !== confirmPassword) {
      control.get('confirmPassword')?.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    } else {
      const errors = control.get('confirmPassword')?.errors;
      if (errors) {
        delete errors['passwordMismatch'];
        if (!Object.keys(errors).length) {
          control.get('confirmPassword')?.setErrors(null);
        }
      }
      return null;
    }
  }

  onSubmit(): void {
    this.registerForm.markAllAsTouched();
    if (this.registerForm.invalid) {
      return;
    }

    const { confirmPassword, ...payload } = this.registerForm.value;
    const birthDateFormatted = this.convertDateToIsoString(payload.birthDate);

    const registerRequest: RegisterCustomerRequest = {
      ...payload,
      birthDate: birthDateFormatted,
      gender: this.convertGender(payload.gender)
    };

    this.authService.register(registerRequest).subscribe({
      next: (response) => {
        if (response.success) {
          this.toastr.success(response.message);
          this.router.navigate(['/login']);
        } else if (response.errors && Array.isArray(response.errors)) {
          response.errors.forEach((error: string | undefined) => this.toastr.error(error));
        }
      },
      error: (err) => {
        if (err.error?.errors && Array.isArray(err.error.errors)) {
          err.error.errors.forEach((error: string) => this.toastr.error(error));
        }
      }
    });
  }
  navigateToLogin(): void {
    this.router.navigate(['/login']);
  }

  private convertGender(gender: string): number {
    switch (gender) {
      case 'Male':
        return 0;
      case 'Female':
        return 1;
      case 'NotSpecified':
        return 2;
      default:
        return 2;
    }
  }

  private convertDateToIsoString(date: Date): string {
    if (!date) return '';

    const year = date.getFullYear().toString().padStart(4, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');

    return `${year}-${month}-${day}`;
  }
}