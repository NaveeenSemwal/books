import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { ToastrModule } from 'ngx-toastr';
import { SharedModule } from './_modules/shared.module';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { TextInputComponent } from './_customcontrols/text-input/text-input.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { DatePickerComponent } from './_customcontrols/date-picker/date-picker.component';
import { ElementDirective } from './_customdirective/element.directive';
import { ComponentDirective } from './_customdirective/component.directive';
import { TemplateRefDirective } from './_customdirective/templateref.directive';
import { ViewContainerRefDirective } from './_customdirective/viewcontainerref.directive';
import { DirectivedemoComponent } from './directivedemo/directivedemo.component';
import { ElementDirectiveByRenderer2 } from './_customdirective/renderer2.directive';
import { ElementHostBinding } from './_customdirective/hostbinding.directive';
import { CustomNgIfDirective } from './_customdirective/customngif.directive';
import { DirectiveWithClass } from './_customdirective/directivewithclass.directive';
import { AlertComponent } from './alert/alert.component';
import { DynamiccomponentComponent } from './dynamiccomponent/dynamiccomponent.component';
import { DynamiccomponentloaderComponent } from './dynamiccomponentloader/dynamiccomponentloader.component';
import { PlaceHolderDirective } from './_customdirective/placeholder.directive';
import { DynamicFormComponent } from './dynamicform/dynamicform.component';
import { DynamicFieldDirective } from './dynamicform/dynamic-field.directive';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { InputComponent } from './_customcontrols/input/input.component';
import { SelectInputComponent } from './_customcontrols/select-input/select-input.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailComponent,
    ListsComponent,
    MessagesComponent,
    MemberCardComponent,
    MemberEditComponent,
    TextInputComponent,
    DashboardComponent,
    DatePickerComponent,
    ElementDirective,
    ComponentDirective,
    TemplateRefDirective,
    ViewContainerRefDirective,
    DirectivedemoComponent,
    ElementDirectiveByRenderer2,
    ElementHostBinding,
    CustomNgIfDirective,
    DirectiveWithClass,
    AlertComponent,
    DynamiccomponentComponent,
    DynamiccomponentloaderComponent,

    PlaceHolderDirective,
    DynamicFormComponent,
    DynamicFieldDirective,
    InputComponent,
    SelectInputComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
