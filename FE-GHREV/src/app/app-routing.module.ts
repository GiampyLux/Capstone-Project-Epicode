import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarrelloComponent } from './pages/carrello/carrello.component';
import { ChiSiamoComponent } from './pages/chi-siamo/chi-siamo.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { ErrorPageComponent } from './pages/error-page/error-page.component';
import { HomeComponent } from './pages/home/home.component';

const routes: Routes = [
{
  path: '',
  component: HomeComponent
},

{
  path: 'Carrello',
  component: CarrelloComponent
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
