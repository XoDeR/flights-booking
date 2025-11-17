import { Routes } from '@angular/router';
import { SearchFlights } from './search-flights/search-flights';
import { BookFlight } from './book-flight/book-flight';
import { RegisterPassenger } from './register-passenger/register-passenger';
import { MyBookings } from './my-bookings/my-bookings';
import { authGuard } from './auth/auth-guard';


export const routes: Routes = [
  { path: '', redirectTo: 'search-flights', pathMatch: 'full' },
  { path: 'search-flights', component: SearchFlights },
  { path: 'book-flight/:flightId', component: BookFlight, canActivate: [authGuard] },
  { path: 'register-passenger', component: RegisterPassenger },
  { path: 'my-bookings', component: MyBookings },
];


