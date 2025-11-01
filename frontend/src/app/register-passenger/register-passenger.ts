import { Component } from '@angular/core';
import { PassengerService } from '../api/services/passenger.service';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Auth } from '../auth/auth';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-passenger',
  imports: [ReactiveFormsModule],
  templateUrl: './register-passenger.html',
  styleUrl: './register-passenger.scss'
})
export class RegisterPassenger {
  form!: FormGroup;

  constructor(private passengerService: PassengerService,
    private fb: FormBuilder,
    private auth: Auth,
    private router: Router
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      email: [''],
      firstName: [''],
      lastName: [''],
      isFemale: [true],
    });
  }

  checkPassenger(): void {
    const params = { email: this.form.get('email')?.value };

    this.passengerService
      .findPassenger(params)
      .then(_ => {
        console.log("Passenger exists. Logging in now.");
        this.login();
      })
      .catch(err => {
        if (err.status != 404) {
          console.error('API error:', err)
        }
      }
      );
  }

  register() {
    console.log("Form fields: ", this.form.value);
    this.passengerService.registerPassenger({ body: this.form.value })
      .then(_ => this.login())
      .catch(err => console.error('API error:', err));
  }

  private login = () => {
    this.auth.loginUser({ email: this.form.get('email')?.value });
    this.router.navigate(['/search-flights']);
  }
}
