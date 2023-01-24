import { Directive, ElementRef, OnInit, Renderer2, HostListener, HostBinding, Input } from '@angular/core';



@Directive({
    selector: '[appDirectiveWithClass]'
})
export class DirectiveWithClass implements OnInit {



    @HostBinding('class.button') toggle: boolean = false;


    @HostListener("click") onMouseEnter() {

        this.toggle = !this.toggle;
    }

   




    ngOnInit(): void {

        

    }
}
