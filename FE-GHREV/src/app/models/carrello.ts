import { Prodotto } from "./prodotto";
import { Utente } from "./utente";

export interface Carrello {
  iD_Utenti: number; // Nome esatto come nel backend
  iD_Prodotti: number;
  quantita: number;
  carrelli: Carrello[]; // Aggiungi questo campo se necessario
  utenti: Utente;
  prodotti: Prodotto; // Assicurati che questo non sia un array ma un singolo prodotto
}
