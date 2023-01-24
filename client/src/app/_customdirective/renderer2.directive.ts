import { Directive, ElementRef, OnInit, Renderer2, HostListener } from '@angular/core';



@Directive({
    selector: '[appRenderer2]'
})
export class ElementDirectiveByRenderer2 implements OnInit {

    @HostListener("mouseenter") onMouseEnter() {
        this.renderer.setStyle(this.element.nativeElement, "color", 'red');
    }

    @HostListener("mouseleave") onMouseLeave() {
        this.renderer.setStyle(this.element.nativeElement, "color", 'black');
    }



    constructor(private element: ElementRef, private renderer: Renderer2) {

    }

    ngOnInit(): void {

    }
}
