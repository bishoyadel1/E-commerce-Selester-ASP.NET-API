import { CanActivateFn } from '@angular/router';
import { Irole } from '../Interfaces/irole';

export const authGenricGuard: CanActivateFn = (route, state) => {

  const token = localStorage.getItem('token');
  const roleclaim: string = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
  const blockedPaths: string[] = [
    '/products/home',

  ];

  // Check if the current state.url is in the blockedPaths array
  if (blockedPaths.some(path => state.url === path)) {
    if (token) {
      const json = JSON.parse(window.atob(token.split('.')[1]));

      if (Array.isArray(json[roleclaim]) && json[roleclaim].some(e => e === 'Admin' || 'Seller' || 'Delivery')) {
        return true;
      } else {
        if (json[roleclaim] == 'Admin' || 'Seller' || 'Delivery') {
          return true;
        }
        else {
          return false
        }
      }

    }
    return false;
  }



  return true
};
