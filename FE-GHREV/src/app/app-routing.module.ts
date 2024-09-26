import { DettagliComponent } from './components/dettagli/dettagli.component';
import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarrelloComponent } from './pages/carrello/carrello.component';
import { ChiSiamoComponent } from './pages/chi-siamo/chi-siamo.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { ErrorPageComponent } from './pages/error-page/error-page.component';
import { HomeComponent } from './pages/home/home.component';
import { AuthGuard } from './auth/auth.guard';
import { SearchComponent } from './pages/search/search.component';
import { CheckoutComponent } from './pages/checkout/checkout.component';

const routes: Routes = [
{
  path: '',
  component: HomeComponent
},

{
  path: 'Carrello',
  component: CarrelloComponent, canActivate:[AuthGuard]
},

{
  path: 'Chi-siamo',
  component: ChiSiamoComponent
},

{
  path: 'login',
  component: LoginComponent

},

{
  path: 'register',
 component: RegisterComponent
},
{ path: 'dettagli/:id', component: DettagliComponent },

{
  path: 'search',
  component: SearchComponent
},
{
  path: 'checkout',
  component: CheckoutComponent, canActivate:[AuthGuard]
},

{
  path: '**',
  component: ErrorPageComponent
}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
