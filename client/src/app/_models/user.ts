export class User 
{
    constructor(
        public name: string,
        public userName: string,
        public email: string,
        public emailConfirmed: boolean,
        public token: string,
      ){}
}