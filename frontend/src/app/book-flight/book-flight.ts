import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FlightService } from './../api/services/flight.service';
import { BookDto, FlightRm } from '../api/models';
import { CommonModule } from '@angular/common';
import { from } from 'rxjs';
import { signal } from '@angular/core';
import { Auth } from '../auth/auth';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-book-flight',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './book-flight.html',
  styleUrl: './book-flight.scss'
})
export class BookFlight implements OnInit {
  constructor(private route: ActivatedRoute,
    private router: Router,
    private flightService: FlightService,
    private auth: Auth,
    private fb: FormBuilder
  ) { } // called before properties are set

  flightId: string = 'not loaded';
  //flight: FlightRm = {};
  form!: FormGroup;

  private _flight = signal<FlightRm>({});

  get flight(): FlightRm {
    return this._flight();
  }

  ngOnInit(): void { // called after everything is set up
    this.route.paramMap
      .subscribe(p => this.findFlight(p.get("flightId")));

    this.form = this.fb.group({
      number: [1, Validators.required, Validators.min(1), Validators.max(254)]
    });
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

  private handleError(err: any) {
    if (err.status == 404) {
      alert("Flight not found!");
      this.router.navigate(['/search-flights']);
    }

    if (err.status == 409) {
      console.log("err: " + err);
      alert(JSON.parse(err.error).message);
    }

    console.log("Response Error. Status: ", err.status);
    console.log("Response Error. Status Text: ", err.statusText);
    console.error('API error:', err)
  }

  book() {
    if (this.form.invalid) {
      return;
    }

    console.log(`Booking ${this.form.get('number')?.value} passengers for the flight: ${this.flight.id}`);

    const booking: BookDto = {
      flightId: this.flight.id,
      passengerEmail: this.auth.currentUser?.email,
      numberOfSeats: this.form.get('number')?.value,
    };

    from(this.flightService.bookFlight({ body: booking }))
      .subscribe({
        next: _ => this.router.navigate(['/my-bookings']),
        error: err => this.handleError(err)
      });
  }

  get number() {
    return this.form.controls['number'];
  }
}
