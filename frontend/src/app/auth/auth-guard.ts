import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { Auth } from './auth';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const auth = inject(Auth);

  if (!auth.currentUser)
    router.navigate(['/register-passenger']);
  return true;
};
