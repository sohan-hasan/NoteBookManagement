import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ResponseCode } from 'src/app/enums/responseCode';
import { RegularNoteModel } from 'src/app/models/note-management-models/regular-note-model';
import { ResponseModel } from 'src/app/models/responseModel';
import { RegularNoteService } from 'src/app/services/note-management-services/regular-note.service';

@Component({
  selector: 'app-regular-note',
  templateUrl: './regular-note.component.html',
  styles: [
  ]
})
export class RegularNoteComponent implements OnInit {

  public itemList: RegularNoteModel[] = [];
  public formSubmitAttempt: boolean= false;
  @ViewChild('closebutton') closebutton: any;
  constructor(private formBuilder: FormBuilder, private toastr: ToastrService, private router: Router, private regularNoteService:RegularNoteService) { }

  ngOnInit(): void {
    this.clearForm();
    this.getAllByUserId();
  }

  getAllByUserId() {
    this.regularNoteService.getAllByUserId().subscribe((data: any) => {
      this.itemList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })

  } 
  public regularNoteForm = this.formBuilder.group({
    regularNoteId: [0],
    note: ['', [Validators.required, Validators.maxLength(100)]],
  })

  get note() { return this.regularNoteForm.get("note") };

  pupulateForm(selectedRecord: RegularNoteModel) {
    this.regularNoteForm.patchValue({
      regularNoteId: selectedRecord.regularNoteId,
      note: selectedRecord.note,
    });
  }

  onSubmit() {
    if (this.regularNoteForm.valid){
      this.regularNoteService.regularNoteModel.regularNoteId = this.regularNoteForm.value.regularNoteId || 0;
      this.regularNoteService.regularNoteModel.note = this.regularNoteForm.value.note;
      if (this.regularNoteForm.value.regularNoteId == 0 || this.regularNoteForm.value.regularNoteId == null) {
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
    this.regularNoteService.insert().subscribe((data: ResponseModel) => {
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
    this.regularNoteService.update().subscribe((data: ResponseModel) => {
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
      this.regularNoteService.delete(id).subscribe(
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
    this.regularNoteForm.reset();
    this.formSubmitAttempt=false;
    
    this.regularNoteForm.get('regularNoteId')?.setValue('');
    this.regularNoteForm.get('note')?.setValue('');
    
  }

}
