import { Directive, TemplateRef } from '@angular/core';

@Directive({
    selector: '[appTemplateRef]'
})
export class TemplateRefDirective {



    // TemplateRef : You will get template refwerence only with ng-template or structural directive

    constructor(private el: TemplateRef<any>) {

        console.log(el);

    }

}