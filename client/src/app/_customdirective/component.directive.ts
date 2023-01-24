import { Directive } from "@angular/core";
import { ListsComponent } from "../lists/lists.component";


// Custom component directive
@Directive({
    selector: '[appComponent]'
})
export class ComponentDirective {

    constructor(private cmp: ListsComponent) {

        this.cmp.message = "This is updated message from Component directive";

    }
}