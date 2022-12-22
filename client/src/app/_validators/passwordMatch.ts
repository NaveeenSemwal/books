import { AbstractControl, ValidatorFn } from "@angular/forms";

// Here control variable refered to the control who declared that like confirmpassword here. 
//  Parent control is above of AbstractControl in HTML structure (confirmpassword)
export function passwordMatch(matchTo: string) : ValidatorFn {

    return (control: AbstractControl) => {
        return control.value === control.parent?.get(matchTo)?.value ? null : { passwordMisMatchError: true };
    }
}