import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // includes ngClass, ngIf, etc.

@Component({
  selector: 'app-search-flights',
  imports: [],
  templateUrl: './search-flights.html',
  styleUrl: './search-flights.scss'
})
export class SearchFlights {

  // test data
  searchResults: any = [
    "American Airlines",
    "British Airways",
    "Lufthansa",
  ];
}
