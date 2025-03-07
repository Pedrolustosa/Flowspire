import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { RegisterCustomerRequest } from '../models/RegisterCustomerRequest';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerRequest: RegisterCustomerRequest = { email: '', fullName: '', password: '' };
  confirmPassword: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    if (this.registerRequest.password !== this.confirmPassword) {
      alert('As senhas não coincidem.');
      return;
    }
    this.authService.register(this.registerRequest).subscribe({
      next: (response) => {
        alert(response.Message);
        this.router.navigate(['/login']);
      },
      error: (err) => {
        alert('Erro no registro: ' + (err.error?.Error || 'Tente novamente'));
      }
    });
  }
}