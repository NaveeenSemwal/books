import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';

@Directive({
    selector: '[appCustomNgIf]'
})
export class CustomNgIfDirective {

    // Note : I want to display a UI when below compare property's value is TRUE like *ngIf

    // Like ElementRef give reference of Element, 
    // TemplateRef gives access of Template like ng-template
    // ViewContainerRef specifies where we need to render that template
    // * in structural directive gets converted to ng-template and using property binding it render template
    constructor(private template: TemplateRef<any>, private container: ViewContainerRef) {

    }

    //  How to dedect and react when an input property value changes

    //  1. ngOnChnages Life cycle hook
    //  2. Using property setter

    // Note : property name should be same as of selector name (appCustomNgIf)
    @Input() set appCustomNgIf(condition: boolean) {

        if (condition) {

            this.container.createEmbeddedView(this.template);

        } else {
            this.container.clear();
        }
    }
}
