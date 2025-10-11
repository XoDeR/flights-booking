import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // includes ngClass, ngIf, etc.

@Component({
  selector: 'app-search-flights',
  imports: [CommonModule],
  templateUrl: './search-flights.html',
  styleUrl: './search-flights.scss'
})
export class SearchFlights {

  // test data
  searchResultsV01: any = [
    "American Airlines",
    "British Airways",
    "Lufthansa",
  ];

  searchResults: FlightRm[] = [
    {
      airline: "American Airlines",
      remainingNumberOfSeats: 500,
      departure: { time: Date.now().toString(), place: "Los-Angeles" },
      arrival: { time: Date.now().toString(), place: "Istanbul" },
      price: "350",
    },
    {
      airline: "Deutsche BA",
      remainingNumberOfSeats: 60,
      departure: { time: Date.now().toString(), place: "Munchen" },
      arrival: { time: Date.now().toString(), place: "Schiphol" },
      price: "600",
    },
    {
      airline: "British Airways",
      remainingNumberOfSeats: 50,
      departure: { time: Date.now().toString(), place: "London" },
      arrival: { time: Date.now().toString(), place: "Valencia" },
      price: "700",
    }
  ];


}

// rm -- read module, just a convention for interfaces' names
export interface FlightRm {
  airline: string;
  arrival: TimePlaceRm;
  departure: TimePlaceRm;
  price: string;
  remainingNumberOfSeats: number;
}

export interface TimePlaceRm {
  place: string;
  time: string;
}
