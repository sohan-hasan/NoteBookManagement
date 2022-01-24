import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from './guards/auth.service';
import { LoggedInAuthGuard } from './guards/loggedInAuth.service';
import { HomeComponent } from './modules/deshboard/home/home.component';
import { PageNotFoundComponent } from './modules/deshboard/page-not-found/page-not-found.component';
import { BookmarkComponent } from './modules/note-management/bookmark/bookmark.component';
import { RegularNoteComponent } from './modules/note-management/regular-note/regular-note.component';
import { ReminderNoteComponent } from './modules/note-management/reminder-note/reminder-note.component';
import { TodoNoteComponent } from './modules/note-management/todo-note/todo-note.component';
import { SignInComponent } from './modules/user-auth/sign-in/sign-in.component';
import { SignUpComponent } from './modules/user-auth/sign-up/sign-up.component';

const routes: Routes = [
  {path: "sign-up", component: SignUpComponent, canActivate:[LoggedInAuthGuard]},
  {path: "sign-in", component: SignInComponent, canActivate:[LoggedInAuthGuard]},

  {path: "", component: HomeComponent, canActivate:[AuthGuardService]},
  {path: "bookmark", component: BookmarkComponent, canActivate:[AuthGuardService]},
  {path: "regular-note", component: RegularNoteComponent, canActivate:[AuthGuardService]},
  {path: "todo-note", component: TodoNoteComponent, canActivate:[AuthGuardService]},
  {path: "reminder-note", component: ReminderNoteComponent, canActivate:[AuthGuardService]},

 { path: '**', pathMatch: 'full', component: PageNotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
