import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { Carrello } from '../models/carrello';  // Usa la nuova interfaccia aggiornata

@Injectable({
  providedIn: 'root'
})
export class CarrelloService {
  private apiUrl = 'https://localhost:7133/api/Carrelli';
  private carrelloAggiornato = new Subject<void>(); // Crea un subject per notificare aggiornamenti

  constructor(private http: HttpClient) {}

  // Metodo per aggiungere un prodotto al carrello
  aggiungiProdottoAlCarrello(carrello: Carrello): Observable<Carrello> {
    return this.http.post<Carrello>(this.apiUrl, carrello);
  }
  getCarrelloUtente(idUtente: number): Observable<Carrello[]> {
    return this.http.get<Carrello[]>(`${this.apiUrl}/utente/${idUtente}`);
  }
  // Metodo per ottenere il subject
  getCarrelloAggiornato(): Observable<void> {
    return this.carrelloAggiornato.asObservable();
  }

  rimuoviProdottoDalCarrello(idUtente: number, idProdotto: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/utente/${idUtente}/prodotto/${idProdotto}`);
  }

}
