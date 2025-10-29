import { Component } from '@angular/core';
import { PassengerService } from '../api/services/passenger.service';

@Component({
  selector: 'app-register-passenger',
  imports: [],
  templateUrl: './register-passenger.html',
  styleUrl: './register-passenger.scss'
})
export class RegisterPassenger {
  constructor(private passengerService: PassengerService) { }

  register() {
    this.passengerService.registerPassenger();
  }
}
