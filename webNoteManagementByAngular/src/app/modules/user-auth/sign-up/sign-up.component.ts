import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ResponseCode } from 'src/app/enums/responseCode';
import { ResponseModel } from 'src/app/models/responseModel';
import { UserService } from 'src/app/services/user-auth-services/user.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  public formSubmitAttempt: boolean= false;
  constructor(
    private formBuilder: FormBuilder, private router: Router, private userService: UserService, private toastr: ToastrService
  ) { }

  ngOnInit(): void {
  }
  public signUpForm = this.formBuilder.group({
    id: [0,],
    name: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
    email: ['', [Validators.required, Validators.email]],
    dateOfBirth: ['', [Validators.required]],
    password: ['', [Validators.minLength(6), Validators.required]],
  })

  get name() { return this.signUpForm.get('name') };
  get email() { return this.signUpForm.get('email') };
  get password() { return this.signUpForm.get('password') };
  get dateOfBirth() { return this.signUpForm.get('dateOfBirth') };

  onSubmit() {
    if(this.signUpForm.valid){
      this.userService.userModel.id=this.signUpForm.value.id || 0;
      this.userService.userModel.name=this.signUpForm.value.name;
      this.userService.userModel.email=this.signUpForm.value.email;
      this.userService.userModel.dateOfBirth=this.signUpForm.value.dateOfBirth;
      this.userService.userModel.password=this.signUpForm.value.password;
      if (this.signUpForm.value.id == 0||this.signUpForm.value.id == null) {
        this.insert();
      }
    }else{
      this.formSubmitAttempt=true;
    }
    
  }
  insert() {
    this.userService.register().subscribe((data: ResponseModel) => {
      if (data.responseCode == ResponseCode.OK) {
        this.toastr.success("You have created account please login");
        this.router.navigate(["sign-in"]);
      } else {
        this.toastr.error(data.responseMessage);
      }
      console.log("response", data);
    }, error => {
      console.log("error", error)
      this.toastr.error("Something went wrong please try again later");
    })
  }
}
