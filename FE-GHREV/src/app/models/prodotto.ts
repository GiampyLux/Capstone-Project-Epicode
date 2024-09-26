import { Categoria } from "./categoria";

export interface Prodotto {
  id: number;
  nome: string;
  descrizione: string;
  prezzo: number;
  iD_Categorie: number;
  immagini: string;
  categorie: Categoria;
}
