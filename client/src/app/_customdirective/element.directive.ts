import { Directive, ElementRef, TemplateRef, ViewContainerRef } from '@angular/core';



@Directive({
  selector: '[appElement]',
  host: {
    '(mouseenter)': 'onMouseHover()',
    '(mouseleave)': 'onMouseLeave()'
  }
})
export class ElementDirective {

  constructor(private element: ElementRef) {

    // el.nativeElement.style.color = "red";
    // el.nativeElement.style.backgroundColor = "yellow";

    //console.log(el);

    // this.container.createEmbeddedView(this.template, {

    //   $implicit: "India",
    //   name: "naveen",
    //   location: "Gurgaon"

    // });

  }

  onMouseHover() {
    this.element.nativeElement.style.color = 'blue';
  }
  onMouseLeave() {
    this.element.nativeElement.style.color = 'black';
  }

}

export class ElementContext {

  $implicit = "India";
  name = "naveen";
  location = "Gurgaon"
}
