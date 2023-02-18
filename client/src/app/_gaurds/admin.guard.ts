import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  //  Note : RouteGuards automatically subscribe and unsubscribe On our behalf. No need to do that.
  constructor(private accountService: AccountService, private toastrService: ToastrService) {


  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {

    return this.accountService.currentUser$.pipe(map(user => {

      if (!user) return false;

      if (user.roles.includes("Admin") || user.roles.includes("Moderator")) {
        return true;

      } else {

        this.toastrService.error("unauthorized access");
        return false;
      }
    }))

  }

}
