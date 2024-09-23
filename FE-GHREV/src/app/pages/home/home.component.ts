import { Component, OnInit } from '@angular/core';
import { ProdottiService } from '../../services/prodotti.service';
import { Prodotto } from '../../models/prodotto';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  prodotti: Prodotto[] = [];

  constructor(private prodottiService: ProdottiService) { }

  ngOnInit(): void {
    this.prodottiService.getProdotti().subscribe(data => {
      this.prodotti = data;
    });
  }
}
