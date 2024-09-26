import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProdottiService } from '../../services/prodotti.service';
import { Prodotto } from '../../models/prodotto';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  prodotti: Prodotto[] = [];
  searchName: string | null = null;

  constructor(private route: ActivatedRoute, private prodottiService: ProdottiService) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      console.log('Query Params:', params);
      this.searchName = params['name'];

      if (this.searchName) {
        this.prodottiService.getProdottiByName(this.searchName).subscribe(data => {
          this.prodotti = data;
        });
      } else {
        console.warn('Il parametro "name" è undefined');
      }
    });
  }
}
