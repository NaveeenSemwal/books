import { Component, ComponentFactoryResolver, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ComponentLoaderFactory } from 'ngx-bootstrap/component-loader';
import { Subscription } from 'rxjs';
import { AlertComponent } from '../alert/alert.component';
import { FieldConfig } from '../shared/field-config';
import { PlaceHolderDirective } from '../_customdirective/placeholder.directive';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-dynamiccomponentloader',
  templateUrl: './dynamiccomponentloader.component.html',
  styleUrls: ['./dynamiccomponentloader.component.css']
})
export class DynamiccomponentloaderComponent implements OnDestroy {

  model: any = {};
  isAdmin: boolean = false;

  errorMessage = "";

  regConfig: FieldConfig[] = [{

    "type": "input",
    "label": "Incident Reference",
    "name": "Incident"
  }];

  @ViewChild(PlaceHolderDirective) alertHost: PlaceHolderDirective | undefined;

  private closeSub: Subscription | undefined;

  // public means it can be accessed in Template also.
  constructor(public accountService: AccountService, private componentFactoryResolver: ComponentFactoryResolver) {


  }
  ngOnDestroy(): void {

    if (this.closeSub) {

      this.closeSub.unsubscribe();

    }

  }


  submit(value: any) {

    alert("Parent component");
  }


  ngOnInit(): void {

  }

  login() {

    this.accountService.login(this.model).subscribe({

      next: (user) => {
        alert("sucess");

      },
      error: issue => {
        this.errorMessage = (issue.error.errorMessages[0]);
        this.showErrorAlert(issue.error.errorMessages[0]);
      }
    });
  }

  onHandleError() {
    this.errorMessage = "";
    this.model = {};
  }

  showErrorAlert(message: string) {

    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(AlertComponent);

    let viewContainerRef = this.alertHost?.container;

    viewContainerRef?.clear();

    const componentRef = viewContainerRef?.createComponent(componentFactory);

    if (componentRef?.instance) {

      componentRef.instance.message = message;

      this.closeSub = componentRef.instance.close.subscribe(x => {


        this.closeSub?.unsubscribe();
        viewContainerRef?.clear();

      })

    }




  }

}
