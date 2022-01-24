import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ResponseCode } from 'src/app/enums/responseCode';
import { BookmarkModel } from 'src/app/models/note-management-models/bookmark-model';
import { ResponseModel } from 'src/app/models/responseModel';
import { BookmarkService } from 'src/app/services/note-management-services/bookmark.service';

@Component({
  selector: 'app-bookmark',
  templateUrl: './bookmark.component.html',
  styles: [
  ]
})
export class BookmarkComponent implements OnInit {

  public itemList: BookmarkModel[] = [];
  public formSubmitAttempt: boolean= false;
  @ViewChild('closebutton') closebutton: any;
  constructor(private formBuilder: FormBuilder, private toastr: ToastrService, private router: Router, private bookmarkServices:BookmarkService) { }

  ngOnInit(): void {
    this.clearForm();
    this.getAllByUserId();
  }

  getAllByUserId() {
    this.bookmarkServices.getAllByUserId().subscribe((data: any) => {
      this.itemList = data;
      console.log(data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })

  } 
  public bookmarkForm = this.formBuilder.group({
    bookmarkId: [0],
    siteName: ['', [Validators.required, Validators.maxLength(50)]],
    bookmarkUrl: ['', [Validators.required, Validators.maxLength(100), Validators.pattern("(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?")]],
  })

  get siteName() { return this.bookmarkForm.get("siteName") };
  get bookmarkUrl() { return this.bookmarkForm.get("bookmarkUrl") };

  pupulateForm(selectedRecord: BookmarkModel) {
    this.bookmarkForm.patchValue({
      bookmarkId: selectedRecord.bookmarkId,
      siteName: selectedRecord.siteName,
      bookmarkUrl: selectedRecord.bookmarkUrl,
    });
  }
  onSubmit() {
    if (this.bookmarkForm.valid){
      this.bookmarkServices.bookmarkModel.bookmarkId = this.bookmarkForm.value.bookmarkId || 0;
      this.bookmarkServices.bookmarkModel.siteName = this.bookmarkForm.value.siteName;
      this.bookmarkServices.bookmarkModel.bookmarkUrl = this.bookmarkForm.value.bookmarkUrl;
      if (this.bookmarkForm.value.bookmarkId == 0 || this.bookmarkForm.value.bookmarkId == null) {
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
    this.bookmarkServices.insert().subscribe((data: ResponseModel) => {
      if (data.responseCode == ResponseCode.OK) {
        this.clearForm();
        this.toastr.success("Bookmark Save successfully");
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
    this.bookmarkServices.update().subscribe((data: ResponseModel) => {
      if (data.responseCode == ResponseCode.OK) {

        this.getAllByUserId();
        this.toastr.success("Bookmark Updated successfully");
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
      this.bookmarkServices.delete(id).subscribe(
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
    this.bookmarkForm.reset();
    this.formSubmitAttempt=false;
    
    this.bookmarkForm.get('bookmarkId')?.setValue('');
    this.bookmarkForm.get('siteName')?.setValue('');
    this.bookmarkForm.get('bookmarkUrl')?.setValue('');
    
  }

}
