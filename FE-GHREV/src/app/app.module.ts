import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './components/navbar/navbar.component';
import { CarrelloComponent } from './pages/carrello/carrello.component';
import { ChiSiamoComponent } from './pages/chi-siamo/chi-siamo.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { ErrorPageComponent } from './pages/error-page/error-page.component';
import { DettagliComponent } from './components/dettagli/dettagli.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SearchComponent } from './pages/search/search.component';
import { CheckoutComponent } from './pages/checkout/checkout.component';



@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CarrelloComponent,
    ChiSiamoComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    ErrorPageComponent,
    DettagliComponent,
    SearchComponent,
    CheckoutComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule


  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
