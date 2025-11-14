import { Component, OnInit } from '@angular/core';
import { BookingRm } from '../api/models';
import { BookingService } from '../api/services';
import { from } from 'rxjs';
import { Auth } from '../auth/auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-my-bookings',
  imports: [CommonModule],
  templateUrl: './my-bookings.html',
  styleUrl: './my-bookings.scss'
})
export class MyBookings implements OnInit {
  bookings!: BookingRm[];

  constructor(private bookingService: BookingService,
    private auth: Auth
  ) {
  }

  ngOnInit(): void {
    from(this.bookingService.listBooking({ email: this.auth.currentUser?.email ?? '' }))
      .subscribe({
        next: r => this.bookings = r,
        error: err => this.handleError(err)
      });
  }

  private handleError(err: any) {
    console.log("Response Error, status:", err.status);
    console.log("Response Error, status text:", err.statusText);
    console.log(err);
  }
}
