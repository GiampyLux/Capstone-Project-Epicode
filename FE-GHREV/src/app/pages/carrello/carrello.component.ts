import { AuthService } from './../../services/auth.service';
import { Carrello } from './../../models/carrello';
import { Component, OnInit } from '@angular/core';
import { CarrelloService } from '../../services/carrello.service';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';



@Component({
  selector: 'CarrelloComponent',
  templateUrl: './carrello.component.html',
  styleUrls: ['./carrello.component.scss']
})
export class CarrelloComponent implements OnInit {
  carrelli: Carrello[] = []; // Array for cart items
  selectedShipping: string = 'standard'; // Default shipping option



  constructor(private carrelloService: CarrelloService, private authService: AuthService, private router: Router ) {}

  ngOnInit(): void {

    if (this.authService.isLoggedIn()) {
      this.caricaCarrello(); // Carica il carrello solo se l'utente è loggato
    } else {
      console.log('Utente non loggato');
      // Mostra un messaggio o reindirizza a una pagina di login
      // ad esempio: this.router.navigate(['/login']);
    }
    // Aggiungi questo codice per ricaricare il carrello quando viene notificato un aggiornamento
    this.carrelloService.getCarrelloAggiornato().subscribe(() => {
      this.caricaCarrello(); // Ricarica il carrello quando viene notificato un aggiornamento
    });
  }

  // Function to load cart items for the user
  caricaCarrello(): void {
    const idUtente = this.authService.getUserIdFromToken(); // Assuming user ID is statically set for now

    if (idUtente) {
      this.carrelloService.getCarrelloUtente(idUtente).subscribe(response => {
        this.carrelli = response;
        console.log('Carrello caricato:', this.carrelli);
      }, error => {
        this.carrelli = []
        console.error('Errore nel caricamento del carrello:', error);
      });
    } else {
      console.error('Utente non loggato');
    }
  }
  andareAlCheckout() {
    this.router.navigate(['/checkout']); // Reindirizza alla pagina di checkout
  }

  eliminaArticolo(idProdotto: number): void {
    const idUtente = this.authService.getUserIdFromToken(); // Assuming user ID is statically set for now

    if (idUtente) {
      this.carrelloService.rimuoviProdottoDalCarrello(idUtente, idProdotto).subscribe(() => {
        this.caricaCarrello();
      });
    }  else {
      console.error('Utente non loggato');
    }
  }
    // Function to calculate total price
    calcolaTotale(): number {
      return this.carrelli.reduce((total, prodotto) => {
        return total + (prodotto.prodotti.prezzo * prodotto.quantita);
      }, 0);
    }

    // Function to handle purchase
    compra(): void {
      alert(`Hai acquistato ${this.carrelli.length} articolo/i per un totale di ${this.calcolaTotale()} €!`);
    }
  }


