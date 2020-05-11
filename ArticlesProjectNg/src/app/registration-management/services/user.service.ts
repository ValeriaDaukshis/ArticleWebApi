import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable, onErrorResumeNext } from 'rxjs';
import { User } from '../models/user';
import { VerifyUser } from '../models/verifyUser';
import { retry, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { UserToken } from '../models/userToken';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url = environment.apiUrl + 'api/Users/';

  constructor(private http: HttpClient,
              private router: Router,
    ) { }

  getUser(user: VerifyUser): Observable<UserToken> {
    return this.http.post<UserToken>(`${this.url}${user.email}`, user);
  }

  addCustomer(customer: User) : Observable<UserToken> {
    return this.http.post<UserToken>(`${this.url}`, customer);
  }

  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // server-side error
      errorMessage = `Error ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(error);
  }

  logOut(){
    localStorage.removeItem("registrate");
    this.navigateToArticles();
  }

  navigateToArticles() {
    this.router.navigate(['/articles']);
  }
}
