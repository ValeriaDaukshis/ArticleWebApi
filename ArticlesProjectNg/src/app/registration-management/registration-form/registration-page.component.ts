import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { User } from '../models/user';

@Component({
  selector: 'app-customer-form',
  templateUrl: './registration-page.component.html'
})

export class RegistrationPageComponent implements OnInit {

  user = new User("", "", "", "", "");

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
    this.router.navigate(['autorization']);
  }

  onCancel() {
    this.navigateToCustomers();
  }
  
  onRegistrate(user: User) {
    console.log(user);
    this.customerService.addCustomer(user).subscribe(c => this.navigateToAutorize());
  }

  handleFileInput(files: FileList) {
    const fileToUpload = files.item(0);
    var reader = new FileReader();
    reader.onload =this.handleFile.bind(this);
    reader.readAsBinaryString(fileToUpload);
  }

  handleFile(event) {
    console.log("in event");
    var binaryString = event.target.result;
    this.user.photo= btoa(binaryString);
   }
}
