import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { VerifyUser } from '../models/verifyUser';
import { User } from '../models/user';
import { UserStorage } from '../models/userStorage';
import { UserToken } from '../models/userToken';

@Component({
  selector: 'app-customer-form',
  templateUrl: './autorization-page.component.html'
})

export class AutorizationPageComponent implements OnInit {

  user = new UserStorage("", "", "");
  verifyUser : VerifyUser;
  
  isLoggedIn: Boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: UserService
  ) {}

  ngOnInit() {
    this.isLoggedIn = false;
  }

  navigateToArticles() {
    this.router.navigate(['/articles']);
  }

  onCancel() {
    this.navigateToArticles();
  }

  logIn(customer: User) {
    this.verifyUser = new VerifyUser(customer.email, customer.password);
    this.customerService.getUser(this.verifyUser).subscribe(c => this.setToStorage(c));
  }

  setToStorage(c: UserStorage){
    this.user = c;
    localStorage.setItem("registrate", JSON.stringify(this.user));
    this.navigateToArticles();
  }
}
