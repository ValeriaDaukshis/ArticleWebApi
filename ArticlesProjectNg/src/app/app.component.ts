import { Component } from '@angular/core';
import { User } from './registration-management/models/user';
import { UserService } from './registration-management/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  constructor(
    private service : UserService
  ){}

  logout() {
    this.service.logOut();
}
}
