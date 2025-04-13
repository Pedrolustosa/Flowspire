import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { NgIf, NgFor } from '@angular/common';
import { LoadingService } from '../services/loading.service';
import { User } from '../models/User';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, NgIf, NgFor],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profileForm!: FormGroup;
  user: User | null = null;

  roleOptions = [
    { id: 0, name: 'Administrator' },
    { id: 1, name: 'Financial Advisor' },
    { id: 2, name: 'Customer' }
  ];

  genderOptions = [
    { id: 0, name: 'Male' },
    { id: 1, name: 'Female' },
    { id: 2, name: 'NotSpecified' }
  ];

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toastr: ToastrService,
    private loadingService: LoadingService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadUserProfile();
  }

  private initializeForm(): void {
    this.profileForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      birthDate: ['', Validators.required],
      gender: [null, Validators.required],
      addressLine1: ['', Validators.required],
      addressLine2: [''],
      city: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required],
      postalCode: ['', Validators.required],
      roles: [[], Validators.required]
    });
  }

  loadUserProfile(): void {
    this.loadingService.setLoading(true);
    this.authService.getCurrentUser().subscribe({
      next: (user) => {
        this.user = user;
        this.profileForm.patchValue({
          firstName: user.firstName,
          lastName: user.lastName,
          birthDate: user.birthDate ? new Date(user.birthDate).toISOString().slice(0, 10) : '',
          gender: user.gender !== undefined ? user.gender : null,
          addressLine1: user.addressLine1,
          addressLine2: user.addressLine2,
          city: user.city,
          state: user.state,
          country: user.country,
          postalCode: user.postalCode,
          roles: Array.isArray(user.roles)
            ? user.roles.filter(role => this.roleOptions.some(option => option.id === role))
            : []
        });
        this.loadingService.setLoading(false);
      },
      error: (err) => {
        this.toastr.error('Error loading profile.', 'Error');
        this.loadingService.setLoading(false);
      }
    });
  }

  onSubmit(): void {
    if (this.profileForm.invalid) {
      this.toastr.error('Please fill all required fields.', 'Error');
      return;
    }
    const formValue = this.profileForm.value;
    const updateRequest = {
      FirstName: formValue.firstName,
      LastName: formValue.lastName,
      BirthDate: formValue.birthDate,
      Gender: formValue.gender,
      AddressLine1: formValue.addressLine1,
      AddressLine2: formValue.addressLine2,
      City: formValue.city,
      State: formValue.state,
      Country: formValue.country,
      PostalCode: formValue.postalCode,
      Roles: formValue.roles
    };
    const payload = { Request: updateRequest };
    
    this.loadingService.setLoading(true);
    this.authService.updateUser(payload as any).subscribe({
      next: (updatedUser) => {
        this.user = updatedUser;
        this.toastr.success('Profile updated successfully!', 'Success');
        this.loadingService.setLoading(false);
      },
      error: (err) => {
        this.toastr.error(err.error?.title || 'Error updating profile.', 'Error');
        this.loadingService.setLoading(false);
      }
    });
  }   

  isRoleSelected(roleId: number): boolean {
    const roles: number[] = this.profileForm.get('roles')?.value || [];
    return roles.includes(roleId);
  }

  toggleRole(roleId: number): void {
    let roles: number[] = this.profileForm.get('roles')?.value || [];
    if (roles.includes(roleId)) {
      roles = roles.filter(r => r !== roleId);
    } else {
      roles.push(roleId);
    }
    this.profileForm.get('roles')?.setValue(roles);
  }

  resetForm(): void {
    if (this.user) {
      this.profileForm.patchValue({
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        birthDate: this.user.birthDate ? new Date(this.user.birthDate).toISOString().slice(0, 10) : '',
        gender: this.user.gender !== undefined ? this.user.gender : null,
        addressLine1: this.user.addressLine1,
        addressLine2: this.user.addressLine2,
        city: this.user.city,
        state: this.user.state,
        country: this.user.country,
        postalCode: this.user.postalCode,
        roles: Array.isArray(this.user.roles) ? [...this.user.roles] : []
      });
    }
  }
}