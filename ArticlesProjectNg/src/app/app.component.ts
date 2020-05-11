import { Component, OnInit } from '@angular/core';
import { UserService } from './registration-management/services/user.service';
import { Router } from '@angular/router';
import { UserToken } from './registration-management/models/userToken';
import { UserStorage } from './registration-management/models/userStorage';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'app';
  constructor(
    private service : UserService,
    private router: Router,
  ){}

  user: UserStorage;
  existed = false;

  ngOnInit() {
    let item = localStorage.getItem("registrate");
    if ( item !== null) {
        this.user = JSON.parse(item);
        this.existed = true;
        return;
      }
    this.existed = false;
  }

  logout() {
    this.service.logOut();
    alert("you logOut");
  }

  cabinet(){
    let item = localStorage.getItem("registrate");
    if ( item !== null) {
        this.user = JSON.parse(item);
        this.existed = true;
        this.navigateToCabinet();
        return;
      }
    alert("authorize");
    this.existed = false;
  }

  navigateToCabinet() {
    this.router.navigate(['/cabinet/{{user.id}}']);
  }
}
