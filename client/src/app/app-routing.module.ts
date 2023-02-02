import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { DirectivedemoComponent } from './directivedemo/directivedemo.component';
import { DynamiccomponentComponent } from './dynamiccomponent/dynamiccomponent.component';
import { DynamiccomponentloaderComponent } from './dynamiccomponentloader/dynamiccomponentloader.component';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AdminGuard } from './_gaurds/admin.guard';
import { AuthGuard } from './_gaurds/auth.guard';
import { PreventUnsavedChangesGuard } from './_gaurds/prevent-unsaved-changes.guard';

const routes: Routes = [

  { path: '', component: HomeComponent },
  { path: 'directive-demo', component: DirectivedemoComponent },
  { path: 'dynamic-component-ngif-demo', component: DynamiccomponentComponent },
  { path: 'dynamic-component-loader-demo', component: DynamiccomponentloaderComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children:
      [
        { path: 'members', component: MemberListComponent },
        { path: 'members/:username', component: MemberDetailComponent },
        { path: 'member/edit', component: MemberEditComponent, canDeactivate: [PreventUnsavedChangesGuard] },
        { path: 'lists', component: ListsComponent },
        { path: 'messages', component: MessagesComponent },
        { path: 'admin', component: AdminPanelComponent ,canActivate: [AdminGuard ]},
        { path: 'admin/dashboard', component: DashboardComponent,canActivate: [AdminGuard ] }

      ]
  },
  { path: '**', component: HomeComponent, pathMatch: 'full' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
