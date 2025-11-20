import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // includes ngClass, ngIf, etc.
import { RouterModule } from '@angular/router';
import { FlightService } from './../api/services/flight.service';
import { FlightRm } from '../api/models';
import { FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-search-flights',
  imports: [CommonModule, RouterModule],
  templateUrl: './search-flights.html',
  styleUrl: './search-flights.scss'
})
export class SearchFlights {
  searchResult: FlightRm[] = []

  constructor(private flightService: FlightService,
    private fb: FormBuilder
  ) { }

  search() {
    this.flightService.searchFlight()
      .then(r => this.searchResult = r)
      .catch(err => this.handleError(err));
  }

  private handleError(err: any) {
    console.log("Response Error. Status: ", err.status);
    console.log("Response Error. Status Text: ", err.statusText);
    console.error('API error:', err)
  }
}

/*
// old read models
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
*/
