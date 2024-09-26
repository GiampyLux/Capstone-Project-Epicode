import { ProdottiService } from './../../services/prodotti.service';
import { Component, OnInit } from '@angular/core';
import { CarrelloService } from '../../services/carrello.service';
import { Carrello } from '../../models/carrello';
import { Prodotto } from '../../models/prodotto';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';


@Component({
  selector: 'dettagliComponent',
  templateUrl: './dettagli.component.html',
  styleUrls: ['./dettagli.component.scss']
})
export class DettagliComponent implements OnInit {
  prodotto: Prodotto | undefined; // Il prodotto attualmente selezionat

  constructor(
    private carrelloService: CarrelloService,
    private prodottiService: ProdottiService,
    private route: ActivatedRoute,
    public authService: AuthService
  ) {
  }

  idUtente = this.authService.getUserIdFromToken();

  ngOnInit(): void {
    this.caricaProdotto(); // Carica il prodotto all'avvio
  }

  // Funzione per caricare il prodotto
  caricaProdotto(): void {
    const idProdotto = +this.route.snapshot.paramMap.get('id')!; // Recupera l'ID dalla rotta
    this.prodottiService.getProdottoById(idProdotto).subscribe(response => {
      this.prodotto = response; // Assegna il prodotto caricato
      console.log('Prodotto caricato:', this.prodotto);
    }, error => {
      console.error('Errore nel caricamento del prodotto:', error);
    });
  }
  // Funzione per aggiungere al carrello
 // Funzione per aggiungere al carrello
aggiungiAlCarrello(): void {
  const idUtente = this.authService.getUserIdFromToken(); // Recupera l'ID utente dal token
  if (this.prodotto && this.idUtente != null && typeof this.idUtente === 'number'){ // Controlla anche idUtente
    const carrello: Carrello = {
      iD_Utenti: this.idUtente,
      iD_Prodotti: this.prodotto.id,
      quantita: 1,
      carrelli: [], // Aggiungi questo campo se necessario
      utenti: {
        id: this.idUtente,
        email: 'giampaolo@gmail.com',
        password: '',
        nome: 'Nome Utente',
        cognome: 'Cognome Utente',
        tel: '1234567890'
      },
      prodotti: {
        id: this.prodotto.id,
        nome: this.prodotto.nome,
        descrizione: this.prodotto.descrizione,
        prezzo: this.prodotto.prezzo,
        iD_Categorie: this.prodotto.iD_Categorie,
        immagini: this.prodotto.immagini,
        categorie: {
          id: this.prodotto.categorie.id,
          nome: this.prodotto.categorie.nome
        }
      }
    };
    console.log('Carrello da inviare:', carrello);

    this.carrelloService.aggiungiProdottoAlCarrello(carrello)
      .subscribe(response => {
        console.log('Prodotto aggiunto al carrello:', response);
        alert('Prodotto aggiunto al carrello!');
      }, error => {
        console.error('Errore durante l\'aggiunta del prodotto al carrello', error);
        alert('Errore durante l\'aggiunta al carrello. Riprova più tardi.');
      });
  } else {
    console.error('Prodotto non definito, impossibile aggiungere al carrello.');
  }
}
}
