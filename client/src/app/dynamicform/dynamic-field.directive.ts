import { ComponentFactoryResolver, Directive, Input, OnInit, ViewContainerRef } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { getRandomValues } from "crypto";
import { FieldConfig } from "../shared/field-config";
import { InputComponent } from "../_customcontrols/input/input.component";


type StringKey = {
    [key: string]: any;
}

const componentMapper: StringKey =
{
    input: InputComponent
}

@Directive({
    selector: '[dynamicField]'
})
export class DynamicFieldDirective implements OnInit {

    @Input() field: FieldConfig | undefined;
    @Input() group: FormGroup | undefined;

    componentRef : any;

    constructor(private vwConatiner: ViewContainerRef, private factoryResolver: ComponentFactoryResolver) {

    }

    ngOnInit(): void {

        if (!this.field?.type) return;

        const factory = this.factoryResolver.resolveComponentFactory(componentMapper[this.field.type]);

        this.componentRef = this.vwConatiner.createComponent(factory);

        this.componentRef.instance.field = this.field;
        this.componentRef.instance.group = this.group;

    }
}
