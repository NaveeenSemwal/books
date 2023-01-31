import { User } from "./user";


export class UserParams {

    gender: string;
    minAge = 18
    maxAge = 99;
    pageNumber = 1
    pageSize = 3;
    orderBy = 'lastActive';

    constructor(user: User) {

        // This is just to match the both values for Gender DDL default selection
        this.gender = user.gender.toLowerCase();
    }
}