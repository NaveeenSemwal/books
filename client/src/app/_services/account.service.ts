import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Console } from 'console';
import { userInfo } from 'os';
import { map, Observable, ReplaySubject } from 'rxjs';
import { RegisterUser } from '../_models/register';
import { User } from '../_models/user';



@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = "https://localhost:5001/api/account/";

  private currentUserSource = new ReplaySubject<User>(1);

  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any): Observable<User> {

    return this.http.post(this.baseUrl + "login", model)
      .pipe(
        map((res: any) => {

          const userentity = res.data.user;

          var data = new User(
            userentity.name,
            userentity.userName,
            userentity.email,
            userentity.emailConfirmed,
            res.data.token
          );

          localStorage.setItem("user", JSON.stringify(data));

          this.currentUserSource.next(data);

          return data;
        })
      )
  }

  register(model: RegisterUser): Observable<any> {

    return this.http.post(this.baseUrl + "register", model);
  }

  setCurrentUser(res: any) {
    this.currentUserSource.next(res);
  }


  logout() {
    localStorage.removeItem("user");
    this.currentUserSource.next(null!);
  }
}
