import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { BookmarkComponent } from './bookmark/bookmark.component';
import { RegularNoteComponent } from './regular-note/regular-note.component';
import { ReminderNoteComponent } from './reminder-note/reminder-note.component';
import { TodoNoteComponent } from './todo-note/todo-note.component';



@NgModule({
  declarations: [
    BookmarkComponent,
    RegularNoteComponent,
    ReminderNoteComponent,
    TodoNoteComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  exports:[
    BookmarkComponent,
    RegularNoteComponent,
    ReminderNoteComponent,
    TodoNoteComponent
  ]
})
export class NoteManagementModule { }
