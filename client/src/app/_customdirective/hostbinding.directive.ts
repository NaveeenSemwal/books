import { Directive, ElementRef, OnInit, Renderer2, HostListener, HostBinding, Input } from '@angular/core';



@Directive({
    selector: '[appHostBinding]'
})
export class ElementHostBinding implements OnInit {

    @Input() defaultColor: string | undefined;
    @Input() HighlightColor: string | undefined;

    @HostBinding('style.color') color :string | undefined ;


    @HostListener("mouseenter") onMouseEnter() {

        this.color = this.HighlightColor;
    }

    @HostListener("mouseleave") onMouseLeave() {

        this.color = this.defaultColor;
    }





    ngOnInit(): void {

        this.color = this.defaultColor;

    }
}
