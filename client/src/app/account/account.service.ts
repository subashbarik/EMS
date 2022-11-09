import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUser, Roles } from '../shared/models/user';
import { selectToken, selectUser } from '../state/account/account.selectors';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  constructor(private httpClient: HttpClient, private store: Store) {}

  loadCurrentUser(token: string) {
    if (token === null) {
      return of(null);
    }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.httpClient.get(this.baseUrl + 'account', { headers });
  }

  login(values: any) {
    return this.httpClient.post(this.baseUrl + 'account/login', values);
  }

  register(values: any) {
    return this.httpClient.post(this.baseUrl + 'account/register', values);
  }
  logout() {
    localStorage.removeItem('token');
    return of(true);
  }
  checkEmailExists(email: string) {
    return this.httpClient.get(
      this.baseUrl + 'account/emailexists?email=' + email
    );
  }
  setJwtTokenInLocalStorage(token: string) {
    localStorage.setItem('token', token);
  }
  getJwtToken(): string {
    let token = localStorage.getItem('token');
    if (token === null) {
      this.store.select(selectToken).subscribe({
        next: (response) => {
          token = response;
        },
      });
    }
    return token;
  }
}
