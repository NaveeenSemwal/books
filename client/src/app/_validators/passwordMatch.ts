import { AbstractControl } from "@angular/forms";

 export function passwordMatch(password: string, confirmpassword: string) {

    return (form: AbstractControl) => {

        const passwordValue = form.get(password)?.value;
        const confirmpasswordValue = form.get(confirmpassword)?.value;

        return passwordValue === confirmpasswordValue ? null : { passwordMisMatchError: true }
    }
}