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

  baseUrl = environment.baseUrl;
     
  private currentUserSource = new BehaviorSubject<User | null>(null);

  currentUser$ = this.currentUserSource.asObservable();

  tokenResponse : any;

  constructor(private http: HttpClient) { }

  login(model: any): Observable<User> {

    return this.http.post(this.baseUrl + "account/login", model)
      .pipe(
        map((res: any) => {

          const userentity = res.data.user;

          var data: User = {
            name: userentity.name,
            userName: userentity.userName,
            email: userentity.email,
            emailConfirmed: userentity.emailConfirmed,
            token: res.data.token
          }

          localStorage.setItem("token", JSON.stringify(data.token));

          this.currentUserSource.next(data);

          return data;
        })
      )
  }

  register(model: RegisterUser): Observable<any> {

    return this.http.post(this.baseUrl + "account/register", model);
  }

  setCurrentUser(res: any) {
    this.currentUserSource.next(res);
  }    


  logout() {
    localStorage.removeItem("user");
    this.currentUserSource.next(null!);
  }

  getUserByToken(token: any) {
      
    let _token = token.split('.')[1];

     this.tokenResponse = JSON.parse(atob(_token));
     return this.tokenResponse;

  }

}
