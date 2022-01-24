import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BookmarkModel } from 'src/app/models/note-management-models/bookmark-model';
import { RegularNoteModel } from 'src/app/models/note-management-models/regular-note-model';
import { ReminderNoteModel } from 'src/app/models/note-management-models/reminder-note-model';
import { TodoNoteModel } from 'src/app/models/note-management-models/todo-note-model';
import { BookmarkService } from 'src/app/services/note-management-services/bookmark.service';
import { RegularNoteService } from 'src/app/services/note-management-services/regular-note.service';
import { ReminderNoteService } from 'src/app/services/note-management-services/reminder-note.service';
import { TodoNoteService } from 'src/app/services/note-management-services/todo-note.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public remindereList: ReminderNoteModel[] = [];
  public regularNoteList: RegularNoteModel[] = [];
  public bookmarkList: BookmarkModel[] = [];
  public todoNoteList: TodoNoteModel[] = [];
  constructor(private reminderServices:ReminderNoteService, private toastr: ToastrService,
    private bookmarkServices:BookmarkService, private regularNoteService:RegularNoteService,
    private todoNoteServices:TodoNoteService
    ) { }

  ngOnInit(): void {
    this.getAllReminderNoteByUserId();
    this.getAllBookmarkByUserId();
    this.getAllRegularNoteByUserId();
    this.getAllTodoNoteByUserId();
  }
  getAllReminderNoteByUserId() {
    this.reminderServices.getAllByUserId().subscribe((data: any) => {
      this.remindereList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })

  } 
  getAllBookmarkByUserId() {
    this.bookmarkServices.getAllByUserId().subscribe((data: any) => {
      this.bookmarkList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  } 
  getAllRegularNoteByUserId() {
    this.regularNoteService.getAllByUserId().subscribe((data: any) => {
      this.regularNoteList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  } 
  getAllTodoNoteByUserId() {
    this.todoNoteServices.getAllByUserId().subscribe((data: any) => {
      this.todoNoteList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })

  } 
  getReminderList(sortingType:string){
    if(sortingType=="today"){
      this.getTodayReminderList();
    }else if(sortingType=="weekly"){
      this.getWeeklyReminderList();
    }else if(sortingType=="monthly"){
      this.getMonthlyReminderList();
    }else{
    this.getAllReminderNoteByUserId();
    }
  }
  getTodayReminderList(){
    this.reminderServices.getTodayReminderList().subscribe((data: any) => {
      this.remindereList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  } 
  getWeeklyReminderList(){
    this.reminderServices.getWeeklyReminderList().subscribe((data: any) => {
      this.remindereList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  }
  getMonthlyReminderList(){
    this.reminderServices.getMonthlyReminderList().subscribe((data: any) => {
      this.remindereList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  }  
  getTodoList(sortingType:string){
    if(sortingType=="today"){
      this.getTodayTodoList();
    }else if(sortingType=="weekly"){
      this.getWeeklyTodoList();
    }else if(sortingType=="monthly"){
      this.getMonthlyTodoList();
    }else{
    this.getAllTodoNoteByUserId();
    }
  }
  getTodayTodoList(){
    this.todoNoteServices.getTodayTodoList().subscribe((data: any) => {
      this.todoNoteList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  } 
  getWeeklyTodoList(){
    this.todoNoteServices.getWeeklyTodoList().subscribe((data: any) => {
      this.todoNoteList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  }
  getMonthlyTodoList(){
    this.todoNoteServices.getMonthlyTodoList().subscribe((data: any) => {
      this.todoNoteList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  }  
}
