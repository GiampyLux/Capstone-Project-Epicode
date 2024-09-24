import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProdottiService } from '../../services/prodotti.service';
import { Prodotto } from '../../models/prodotto';

@Component({
  selector: 'app-dettagli',
  templateUrl: './dettagli.component.html',
  styleUrls: ['./dettagli.component.scss']  // Nota: qui è "styleUrls" al plurale
})
export class DettagliComponent implements OnInit {
  prodotto: Prodotto | undefined;

  constructor(private route: ActivatedRoute, private prodottiService: ProdottiService) { }

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;  // Ottiene l'ID dall'URL
    this.prodottiService.getProdottoById(id).subscribe(data => {
      this.prodotto = data;
    });
  }
}
