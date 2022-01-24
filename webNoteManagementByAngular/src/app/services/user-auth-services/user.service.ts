import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Constants } from 'src/app/Helper/constants';
import { ResponseModel } from 'src/app/models/responseModel';
import { UserModel } from 'src/app/Models/user-auth-models/user-model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
 private readonly baseURL:string=Constants.API_KEY+"api/account/"
 public userModel:UserModel=new UserModel();
 constructor(private httpClient:HttpClient,private toastr: ToastrService) { }
  public singIn(email:string , password:string)
  {
    const body={
      Email:email,
      Password:password,
    }
   return this.httpClient.post<ResponseModel>(this.baseURL+"SignIn",body);
  }

  public register()
  {
   return this.httpClient.post<ResponseModel>(this.baseURL+"RegisterUser", this.userModel);
  }

}
