// register.component.ts
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  registerForm: FormGroup;
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
      nome: ['', Validators.required],
      cognome: ['', Validators.required],
      tel: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      if (this.registerForm.value.password === this.registerForm.value.confirmPassword) {
        // Aggiungi nome, cognome, e tel ai dati da inviare
        const userData = {
          email: this.registerForm.value.email,
          password: this.registerForm.value.password,
          nome: this.registerForm.value.nome,
          cognome: this.registerForm.value.cognome,
          tel: this.registerForm.value.tel
        };

        this.authService.register(userData).subscribe({
          next: () => this.router.navigate(['/login']),
          error: (err) => this.errorMessage = 'Registration failed: ' + (err.error.message || 'Unknown error')
        });
      } else {
        this.errorMessage = 'Passwords do not match';
      }
    }
  }
}

