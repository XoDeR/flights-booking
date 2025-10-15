import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-book-flight',
  imports: [],
  templateUrl: './book-flight.html',
  styleUrl: './book-flight.scss'
})
export class BookFlight implements OnInit {
  constructor(private route: ActivatedRoute) { } // called before properties are set

  flightId: string = 'not loaded';

  ngOnInit(): void { // called after everything is set up
    this.route.paramMap
      .subscribe(p => this.flightId = p.get("flightId") ?? 'not passed');
  }
}
