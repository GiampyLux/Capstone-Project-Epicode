import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, of, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7133/api/auth';
  private userNameSubject = new BehaviorSubject<string | null>(null); // Comportamento per il nome dell'utente

  constructor(private http: HttpClient, private router: Router) {
    // Controlla se il nome è già presente in localStorage
    const storedName = localStorage.getItem('nome');
    this.userNameSubject.next(storedName); // Imposta il nome nel BehaviorSubject
  }

  // Registrazione
  register(user: any) {
    return this.http.post(`${this.apiUrl}/register`, user).pipe(
      catchError(error => {
        console.error('Registration failed', error);
        return of(null);
      })
    );
  }

  // Login
  login(credentials: any) {
    return this.http.post<{ token: string, message: string, nome: string }>(`${this.apiUrl}/login`, credentials).pipe(
      catchError(error => {
        console.error('Login failed', error.error.message || 'Login failed. Please try again.');
        return of(null);
      })
    ).subscribe(response => {
      if (response) {
        localStorage.setItem('token', response.token); // Salva il token nel localStorage
        localStorage.setItem('nome', response.nome); // Salva il nome dell'utente
        this.userNameSubject.next(response.nome); // Notifica il nuovo nome
        this.router.navigate(['/']); // Reindirizza l'utente al carrello
      }
    });
  }

  // Ottieni le informazioni dell'utente (se necessario)
  getUserInfo() {
    const token = localStorage.getItem('token');
    return this.http.get<{ nome: string }>(`${this.apiUrl}/user`, {
      headers: { Authorization: `Bearer ${token}` }
    }).pipe(
      catchError(error => {
        console.error('Failed to fetch user info', error);
        return of({ nome: null });
      })
    ).subscribe(response => {
      if (response && response.nome) {
        localStorage.setItem('nome', response.nome);
        this.userNameSubject.next(response.nome); // Notifica il nuovo nome
      }
    });
  }

  // Verifica se l'utente è loggato
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  // Logout
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('nome');
    this.userNameSubject.next(null); // Notifica che l'utente ha effettuato il logout
    this.router.navigate(['/']);
  }

  // Ottieni il token
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Ottieni il nome dell'utente come un Observable
  getUserNameObservable() {
    return this.userNameSubject.asObservable();
  }
}
