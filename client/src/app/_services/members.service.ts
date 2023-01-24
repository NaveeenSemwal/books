import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.baseUrl;

  members: Member[] = [];



  constructor(private http: HttpClient) { }

  getMembers(userParams: UserParams) {
    // of method convert it to Observable
    // if (this.members.length > 0) return of(this.members);

    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);

    return this.getpaginatedResult<Member[]>(this.baseUrl + "v1/users/", params)
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

    const member = this.members.find(member => member.name === username);
    if (member) return of(member);

    return this.http.get<Member>(this.baseUrl + "v1/users/" + username)
  }

  updateMember(model: Member) {

    return this.http.put(this.baseUrl + "v1/users", model);
  }

}
