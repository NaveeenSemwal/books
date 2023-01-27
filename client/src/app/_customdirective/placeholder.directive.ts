import { Directive, TemplateRef, ViewContainerRef } from '@angular/core';

@Directive({
    selector: '[appPlaceHolder]'
})
export class PlaceHolderDirective {


    constructor(public container: ViewContainerRef, private templateRef: TemplateRef<any>) {

        // this.container.createEmbeddedView(this.templateRef);

    }

}