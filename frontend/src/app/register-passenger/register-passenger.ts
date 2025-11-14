import { Component } from '@angular/core';
import { PassengerService } from '../api/services/passenger.service';
import { FormGroup, ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Auth } from '../auth/auth';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register-passenger',
  imports: [ReactiveFormsModule, CommonModule],
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
      email: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
      firstName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(40)])],
      lastName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(40)])],
      isFemale: [true, Validators.required],
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
    if (this.form.invalid) {
      return;
    }

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
