import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgModule } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss'
})
export class CheckoutComponent {
  checkoutForm!: FormGroup;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.checkoutForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      cardNumber: ['', [Validators.required, Validators.minLength(16), Validators.maxLength(16), Validators.pattern('\\d+')]],
      expiry: ['', [Validators.required, Validators.pattern('^(0[1-9]|1[0-2])\\/\\d{2}$')]],
      cvc: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(3), Validators.pattern('\\d+')]]
    });
  }

  onSubmit(): void {
    if (this.checkoutForm.valid) {
      // Estrai i valori dal modulo
      const { firstName, lastName, cardNumber, expiry, cvc } = this.checkoutForm.value;

      // Mostra un alert di pagamento effettuato
      alert("Pagamento effettuato con successo!");
    } else {
      this.errorMessage = 'Dati non compilati correttamente. Assicurati che tutti i campi siano riempiti correttamente.';
    }
  }
}

