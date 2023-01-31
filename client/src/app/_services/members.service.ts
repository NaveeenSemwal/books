import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { User } from '../_models/user';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.baseUrl;


  members: Member[] = [];
  userParams: UserParams | undefined;

  user: User | undefined;

  // A javascript Map hold key value pairs like dictonary and remembers it. So will use this for caching.
  memberCache = new Map();

  constructor(private http: HttpClient, private accountService: AccountService) {

    // take(1) : It is from rxjs. Any Http call we subscribe,  we need to unsubscribe also. But using take(1) we are completeing this call. 
    // So no need of unsubscribe
    this.accountService.currentUser$.pipe(take(1)).subscribe({

      next: user => {
        if (user) {
          this.userParams = new UserParams(user);
          this.user = user;
        }
      }
    });

  }

  // get and set method
  getUserParams() {
    return this.userParams;
  }

  setUserParams(userParams: UserParams) {
    return this.userParams = userParams;
  }

  resetUserParams() {

    if (this.user) {
      this.userParams = new UserParams(this.user);
      return this.userParams;
    }
    return;
  }

  getMembers(userParams: UserParams) {
    // of method convert it to Observable
    // if (this.members.length > 0) return of(this.members);

    // How to to get the key from object. 18-99-1-3-lastActive-male is the key.
    // console.log(Object.values(userParams).join("-"));

    // If we find any cached value for this key then we will return it from here.
    const response = this.memberCache.get(Object.values(userParams).join("-"));
    if (response) {
      return of(response);
    }

    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);

    return this.getpaginatedResult<Member[]>(this.baseUrl + "v1/users/", params).pipe(
      map(response => {

        this.memberCache.set(Object.values(userParams).join('-'), response);
        return response;
      }))
  }

  private getpaginatedResult<T>(url: string, sparams: HttpParams) {

    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();

    return this.http.get<T>(url, { observe: "response", params: sparams }).pipe(
      map(response => {

        if (response.body) {
          paginatedResult.result = response.body;
        }
        let pagination = response.headers.get("Pagination");

        if (pagination) {
          paginatedResult.pagination = JSON.parse(pagination);
        }
        return paginatedResult;

      }));
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number) {

    let sparams = new HttpParams();

    sparams = sparams.append('pageNumber', pageNumber);
    sparams = sparams.append('pageSize', pageSize);

    return sparams;
  }

  getMember(username: string) {

    // get all values from memberCache
    let spreadMemberCache = [...this.memberCache.values()];

    // https://blog.greenroots.info/5-ways-to-merge-arrays-in-javascript-and-their-differences
    const members = spreadMemberCache.reduce((prevArr, currentElement) => prevArr.concat(currentElement.result), []);

    // Find member from array and return if found.
    let member = members.find((x: Member) => x.name === username);
    if (member) return of(member);

    return this.http.get<Member>(this.baseUrl + "v1/users/" + username)
  }

  updateMember(model: Member) {

    return this.http.put(this.baseUrl + "v1/users", model).pipe(
      map(() => {

        const index = this.members.indexOf(model);
        //  Update the members array with incoming member using array destructring and spread operator {}
        this.members[index] = { ...this.members[index], ...model };

      }))
  }

}
