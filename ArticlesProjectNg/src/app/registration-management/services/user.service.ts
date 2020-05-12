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
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      alert('An error occurred:'+ error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      alert(error.error);
      // console.error(
      //   `Backend returned code ${error.status}, ` +
      //   `body was: ${error.error}`);
    }
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
