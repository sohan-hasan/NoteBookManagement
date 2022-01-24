import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ResponseCode } from 'src/app/enums/responseCode';
import { TodoNoteModel } from 'src/app/models/note-management-models/todo-note-model';
import { ResponseModel } from 'src/app/models/responseModel';
import { TodoNoteService } from 'src/app/services/note-management-services/todo-note.service';

@Component({
  selector: 'app-todo-note',
  templateUrl: './todo-note.component.html',
  styles: [
  ]
})
export class TodoNoteComponent implements OnInit {

  public itemList: TodoNoteModel[] = [];
  public formSubmitAttempt: boolean= false;
  @ViewChild('closebutton') closebutton: any;
  constructor(private formBuilder: FormBuilder, private toastr: ToastrService, private router: Router, private todoNoteServices:TodoNoteService, private datePipe:DatePipe) { }

  ngOnInit(): void {
    this.clearForm();
    this.getAllByUserId();
  }

  getAllByUserId() {
    this.todoNoteServices.getAllByUserId().subscribe((data: any) => {
      this.itemList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })

  } 
  public todoNoteForm = this.formBuilder.group({
    todoNoteId: [0],
    note: ['', [Validators.required, Validators.maxLength(100)]],
    todoDate: ['', [Validators.required]],
    todoTime: ['', [Validators.required]],
    isComplete: [0, [Validators.required]],
  })

  get note() { return this.todoNoteForm.get("note") };
  get todoDate() { return this.todoNoteForm.get("todoDate") };
  get todoTime() { return this.todoNoteForm.get("todoTime") };
  get isComplete() { return this.todoNoteForm.get("isComplete") };

  pupulateForm(selectedRecord: TodoNoteModel) {
    var dateTime = selectedRecord.todoDateTime || "";
    var a = dateTime.split("T");
    var date = a[0];
    var time = a[1];
    this.todoNoteForm.patchValue({
      todoNoteId: selectedRecord.todoNoteId,
      note: selectedRecord.note,
      todoDate: date,
      todoTime: time,
      isComplete: selectedRecord.isComplete,
    });
  }
  onSubmit() {
    if (this.todoNoteForm.valid){
      this.todoNoteServices.todoNoteModel.todoNoteId = this.todoNoteForm.value.todoNoteId || 0;
      this.todoNoteServices.todoNoteModel.note = this.todoNoteForm.value.note;
      this.todoNoteServices.todoNoteModel.todoDate = this.todoNoteForm.value.todoDate;
      this.todoNoteServices.todoNoteModel.todoTime = this.todoNoteForm.value.todoTime;
      this.todoNoteServices.todoNoteModel.isComplete = this.todoNoteForm.value.isComplete;
      if (this.todoNoteForm.value.todoNoteId == 0 || this.todoNoteForm.value.todoNoteId == null) {
        this.insert();
      }
      else {
        this.update();
      }
    }else{
      this.formSubmitAttempt=true;
    }
    
  };

  insert() {
    this.todoNoteServices.insert().subscribe((data: ResponseModel) => {
      if (data.responseCode == ResponseCode.OK) {
        this.clearForm();
        this.toastr.success("Todo note save successfully");
        this.getAllByUserId();
        this.closebutton.nativeElement.click();
      }
      else {
        this.toastr.error("Invalid Entry", data.responseMessage);
      }
      console.log("response", data)
    }, error => {
      console.log("error", error);
      this.toastr.error("Try Again");
    })
  }

  update() {
    this.todoNoteServices.update().subscribe((data: ResponseModel) => {
      if (data.responseCode == ResponseCode.OK) {

        this.getAllByUserId();
        this.toastr.success("Todo note Updated successfully");
        this.clearForm();
        this.closebutton.nativeElement.click();
      }
      else {
        this.toastr.error("Invalid Entry", data.responseMessage);
      }
      console.log("response", data)
    }, error => {
      console.log("error", error);
      this.toastr.error("Something is wrong. Please Try Again");
    })
  }

  onDelete(id: any) {
    if (confirm("Are u sure to delete this recored ?")) {
      this.todoNoteServices.delete(id).subscribe(
        res => {
          this.toastr.success("Delete successfully")
          this.getAllByUserId();
        },
        err => {
          this.toastr.error("Delete Failed")
          console.log(err)
        }
      )
    }
  }
  clearForm() {
    this.todoNoteForm.reset();
    this.formSubmitAttempt=false;
    
    this.todoNoteForm.get('todoNoteId')?.setValue(0);
    this.todoNoteForm.get('note')?.setValue('');
    this.todoNoteForm.get('todoDateTime')?.setValue('');
    this.todoNoteForm.get('isComplete')?.setValue(0);
    
  }

}
