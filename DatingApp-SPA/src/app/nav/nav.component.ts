import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model:any = {};
photoUrl:string;
  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit(): void {
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl)
  }
login(){
  this.authService.login(this.model).subscribe(next=> {
  this.alertify.success('logged in successfully');
  }, error => {
    this.alertify.error('Failed to log');
  });
}

loggedIn(){
  return this.authService.loggedIn();
}

logout(){
  localStorage.removeItem('token');
  localStorage.removeItem('user');
  this.authService.decodedToken = null;
  this.authService.currentUser = null;
  this.alertify.message('logged out');
}

}
