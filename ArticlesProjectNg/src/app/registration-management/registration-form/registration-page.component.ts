import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { User } from '../models/user';

@Component({
  selector: 'app-customer-form',
  templateUrl: './registration-page.component.html'
})

export class RegistrationPageComponent implements OnInit {

  user = new User("", "", "", "");

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: UserService
  ) {}

  ngOnInit() {
  }

  navigateToCustomers() {
    this.router.navigate(['/articles']);
  }

  navigateToAutorize() {
    this.router.navigate(['/autorization']);
  }

  onCancel() {
    this.navigateToCustomers();
  }
  
  onRegistrate(user: User) {
       this.customerService.addCustomer(user).subscribe(c => this.navigateToAutorize);
  }
}
