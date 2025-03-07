import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { LoginRequest } from '../models/LoginRequest';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginRequest: LoginRequest = { email: '', password: '' };

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService.login(this.loginRequest).subscribe({
      next: (response) => {
        if (response && response.accessToken) {
          this.router.navigate(['/dashboard']).then(success => {
          }).catch(err => {
            console.error('Erro ao navegar para /dashboard:', err);
          });
        } else {
          console.error('AccessToken não encontrado na resposta:', response);
        }
      },
      error: (err) => {
        console.error('Erro no login:', err);
      }
    });
  }
}