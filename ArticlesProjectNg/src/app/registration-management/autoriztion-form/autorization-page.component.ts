import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { VerifyUser } from '../models/verifyUser';
import { User } from '../models/user';

@Component({
  selector: 'app-customer-form',
  templateUrl: './autorization-page.component.html'
})

export class AutorizationPageComponent implements OnInit {

  user = new User("", "", "", "");
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

  setToStorage(c: User){
    this.user = c;
    localStorage.setItem("registrate", JSON.stringify(this.user));
    this.navigateToArticles();
  }
}
