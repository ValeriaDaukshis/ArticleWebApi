import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../user.service';
import { VerifyUser } from '../verifyUser';
import { User } from '../user';

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

  navigateToCustomers() {
    this.router.navigate(['/articles']);
  }

  onCancel() {
    this.navigateToCustomers();
  }

  onAutorize(customer: User) {
    this.verifyUser = new VerifyUser(customer.email, customer.password);
    this.customerService.getCustomer(this.verifyUser).subscribe(c => this.navigateToCustomers);
    localStorage.setItem("registrate", JSON.stringify(this.user));
  }

  singOut(){
    localStorage.removeItem("registrate");
  }
}
