import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs'
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/user';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/internal/operators';

@Injectable({
    providedIn: 'root'
})

export class MemberListResolver implements Resolve<User[]>{
    constructor(private userService: UserService,
        private router: Router, private alertify: AlertifyService){}

        resolve(route: ActivatedRouteSnapshot): Observable<User[]>{
            return this.userService.getUsers().pipe(
                catchError(error => {
                    this.alertify.error('Problem retrieving data List');
                    this.router.navigate(['/home']);
                    return of(null);
                })
            );
        }
        
}