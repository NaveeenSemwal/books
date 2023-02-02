import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { RegisterUser } from '../_models/register';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.baseUrl + "v1/";

  private currentUserSource = new BehaviorSubject<User | null>(null);

  currentUser$ = this.currentUserSource.asObservable();

  tokenResponse: any;

  constructor(private http: HttpClient) { }

  login(model: any): Observable<User> {

    return this.http.post(this.baseUrl + "account/login", model).pipe(
      map((response: any) => {

        const loginResponse = response.data;

        const user: User = {

          userName: loginResponse.user.name,
          token: loginResponse.token,
          gender: loginResponse.user.gender,
          knownAs: loginResponse.user.knownAs,
          photoUrl: loginResponse.user.photoUrl,
          roles: new Array<string>(),

        };

        if (user) {

          this.setCurrentUser(user);
        }

        return user;
      }));


  }

  register(model: any): Observable<any> {

    return this.http.post(this.baseUrl + "account/register", model).pipe(
      map((response: any) => {

        const loginResponse = response.data;

        const user: User = {

          userName: loginResponse.user.name,
          token: loginResponse.token,
          gender: loginResponse.user.gender,
          knownAs: loginResponse.user.knownAs,
          photoUrl: loginResponse.user.photoUrl,
          roles: new Array<string>(),

        };

        if (user) {

          this.setCurrentUser(user);
        }

      }))
  }

  setCurrentUser(user: User) {

    const roles = this.getDecodedToken(user.token).role;

    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);

    localStorage.setItem("user", JSON.stringify(user));

    this.currentUserSource.next(user);
  }


  logout() {
    localStorage.removeItem("user");
    this.currentUserSource.next(null!);
  }

  getDecodedToken(token: any) {

    let _token = token.split('.')[1]; // This is the middle part of token
    return JSON.parse(atob(_token));

  }

}
