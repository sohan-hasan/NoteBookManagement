import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ResponseCode } from 'src/app/enums/responseCode';
import { ReminderNoteModel } from 'src/app/models/note-management-models/reminder-note-model';
import { ResponseModel } from 'src/app/models/responseModel';
import { ReminderNoteService } from 'src/app/services/note-management-services/reminder-note.service';

@Component({
  selector: 'app-reminder-note',
  templateUrl: './reminder-note.component.html',
  styles: [
  ]
})
export class ReminderNoteComponent implements OnInit {

  public itemList: ReminderNoteModel[] = [];
  public formSubmitAttempt: boolean= false;
  @ViewChild('closebutton') closebutton: any;
  constructor(private formBuilder: FormBuilder, private toastr: ToastrService, private router: Router, private reminderServices:ReminderNoteService,
    private datePipe:DatePipe) { }

  ngOnInit(): void {
    this.clearForm();
    this.getAllByUserId();
  }

  getAllByUserId() {
    this.reminderServices.getAllByUserId().subscribe((data: any) => {
      this.itemList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })

  } 
  public reminderNoteForm = this.formBuilder.group({
    reminderNoteId: [0],
    note: ['', [Validators.required, Validators.maxLength(100)]],
    reminderDate: ['', [Validators.required]],
    reminderTime: ['', [Validators.required]],
  })

  get note() { return this.reminderNoteForm.get("note") };
  get reminderDate() { return this.reminderNoteForm.get("reminderDate") };
  get reminderTime() { return this.reminderNoteForm.get("reminderTime") };

  pupulateForm(selectedRecord: ReminderNoteModel) {
    var dateTime = selectedRecord.reminderDateTime || "";
    var a = dateTime.split("T");
    var date = a[0];
    var time = a[1];
    this.reminderNoteForm.patchValue({
      reminderNoteId: selectedRecord.reminderNoteId,
      note: selectedRecord.note,
      reminderDate: date,
      reminderTime: time,
    });
  }

  onSubmit() {
    if (this.reminderNoteForm.valid){
      this.reminderServices.reminderNoteModel.reminderNoteId = this.reminderNoteForm.value.reminderNoteId || 0;
      this.reminderServices.reminderNoteModel.note = this.reminderNoteForm.value.note;
      this.reminderServices.reminderNoteModel.reminderDate = this.reminderNoteForm.value.reminderDate;
      this.reminderServices.reminderNoteModel.reminderTime = this.reminderNoteForm.value.reminderTime;
      if (this.reminderNoteForm.value.reminderNoteId == 0 || this.reminderNoteForm.value.reminderNoteId == null) {
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
    this.reminderServices.insert().subscribe((data: ResponseModel) => {
      if (data.responseCode == ResponseCode.OK) {
        this.clearForm();
        this.toastr.success("Test Info Save successfully");
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
    this.reminderServices.update().subscribe((data: ResponseModel) => {
      if (data.responseCode == ResponseCode.OK) {

        this.getAllByUserId();
        this.toastr.success("Test Info Updated successfully");
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
      this.reminderServices.delete(id).subscribe(
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
    this.reminderNoteForm.reset();
    this.formSubmitAttempt=false;
    this.reminderNoteForm.get('reminderNoteId')?.setValue('');
    this.reminderNoteForm.get('note')?.setValue('');
    this.reminderNoteForm.get('reminderDateTime')?.setValue('');
    
  }

}
