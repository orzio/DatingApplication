import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs'
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/user';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/internal/operators';
import { AuthService } from '../_services/auth.service';

@Injectable({
    providedIn: 'root'
})

export class MemberEditResolver implements Resolve<User>{
    constructor(private userService: UserService, private authService: AuthService,
        private router: Router, private alertify: AlertifyService){}

        resolve(route: ActivatedRouteSnapshot): Observable<User>{
            return this.userService.getUser(this.authService.decodedToken.nameid).pipe(
                catchError(error => {
                    this.alertify.error('Problem retrieving data');
                    this.router.navigate(['/members']);
                    return of(null);
                })
            );
        }
        
}