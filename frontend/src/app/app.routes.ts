import { Routes } from '@angular/router';
import { SearchFlights } from './search-flights/search-flights';
import { BookFlight } from './book-flight/book-flight';


export const routes: Routes = [
  { path: '', redirectTo: 'search-flights', pathMatch: 'full' },
  { path: 'search-flights', component: SearchFlights },
  { path: 'book-flight', component: BookFlight },
];


