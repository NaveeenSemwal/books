import { Directive, TemplateRef, ViewContainerRef } from '@angular/core';

@Directive({
    selector: '[appViewContainerRef]'
})
export class ViewContainerRefDirective {


    constructor(private container: ViewContainerRef, private templateRef: TemplateRef<any>) {

        this.container.createEmbeddedView(this.templateRef);

    }

}