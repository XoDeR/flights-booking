import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FlightService } from './../api/services/flight.service';
import { FlightRm } from '../api/models';
import { CommonModule } from '@angular/common';
import { from, Observable } from 'rxjs';
import { signal } from '@angular/core';

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
  //flight: FlightRm = {};

  private _flight = signal<FlightRm>({});

  get flight(): FlightRm {
    return this._flight();
  }

  ngOnInit(): void { // called after everything is set up
    this.route.paramMap
      .subscribe(p => this.findFlight(p.get("flightId")));
  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed';

    from(this.flightService.findFlight({ id: this.flightId }))
      .subscribe({
        next: flight => this._flight.set(flight),
        error: err => this.handleError(err)
      });

    // this.flightService.findFlight({ id: this.flightId })
    //   .then(f => { this.flight = f; console.log(this.flight); })
    //   .catch(err => console.error('API error:', err));
  }

  private handleError(err: unknown): void {
    console.error('Failed to load flight:', err);
  }
}
