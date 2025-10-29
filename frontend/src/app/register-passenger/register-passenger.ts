import { Component } from '@angular/core';
import { PassengerService } from '../api/services/passenger.service';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-register-passenger',
  imports: [ReactiveFormsModule],
  templateUrl: './register-passenger.html',
  styleUrl: './register-passenger.scss'
})
export class RegisterPassenger {
  form!: FormGroup;

  constructor(private passengerService: PassengerService,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      email: [''],
      firstName: [''],
      lastName: [''],
      isFemale: [true],
    });
  }

  register() {
    console.log("Form fields: ", this.form.value);
    this.passengerService.registerPassenger();
  }
}
