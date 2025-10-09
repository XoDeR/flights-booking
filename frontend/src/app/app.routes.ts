import { Routes } from '@angular/router';
import { SearchFlights } from './search-flights/search-flights';


export const routes: Routes = [
  { path: '', redirectTo: 'search', pathMatch: 'full' },
  { path: 'search', component: SearchFlights },
];


