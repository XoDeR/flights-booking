import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FlightService } from './../api/services/flight.service';
import { FlightRm } from '../api/models';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-flight',
  imports: [CommonModule],
  templateUrl: './book-flight.html',
  styleUrl: './book-flight.scss'
})
export class BookFlight implements OnInit {
  constructor(private route: ActivatedRoute,
    private flightService: FlightService
  ) { } // called before properties are set

  flightId: string = 'not loaded';
  flight: FlightRm = {};

  ngOnInit(): void { // called after everything is set up
    this.route.paramMap
      .subscribe(p => this.findFlight(p.get("flightId")));
  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed';

    this.flightService.findFlight({ id: this.flightId })
      .then(f => { this.flight = f; console.log(this.flight); })
      .catch(err => console.error('API error:', err));
  }
}
