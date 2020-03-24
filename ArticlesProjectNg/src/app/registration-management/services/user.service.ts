import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable, onErrorResumeNext } from 'rxjs';
import { User } from '../models/user';
import { VerifyUser } from '../models/verifyUser';
import { retry, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url = environment.apiUrl + 'api/Users/';

  constructor(private http: HttpClient) { }

  getUser(user: VerifyUser): Observable<User> {
    return this.http.post<User>(`${this.url}${user.email}`, user)
    .pipe(
      catchError(this.handleError)
    );
  }

  addCustomer(customer: User) : Observable<User> {
    return this.http.post<User>(`${this.url}`, customer);
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
  }
}
