import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Constants } from 'src/app/Helper/constants';
import { RegularNoteModel } from 'src/app/models/note-management-models/regular-note-model';
import { ResponseModel } from 'src/app/models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class RegularNoteService {

  constructor(private httpClient:HttpClient) { }
  public regularNoteModel: RegularNoteModel = new RegularNoteModel();
  private readonly regularNoteUrl: string = Constants.API_KEY+"api/RegularNote/";
  public insert() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    this.regularNoteModel.userId=user.userId;
    return this.httpClient.post<ResponseModel>(this.regularNoteUrl + "Insert", this.regularNoteModel,{headers:headers});
  }

  public update() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    this.regularNoteModel.userId=user.userId;
    return this.httpClient.put<ResponseModel>(this.regularNoteUrl + "Update", this.regularNoteModel,{headers:headers});
  }

  public delete(id: number) {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    this.regularNoteModel.userId=user.userId;
    return this.httpClient.delete(this.regularNoteUrl + "Delete?id="+id,{headers:headers});
  }
  public getById(id:number) {
    return this.httpClient.get<ResponseModel>(this.regularNoteUrl+"GetById?id="+id);
  }
  public getAllByUserId() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    return this.httpClient.get<ResponseModel>(this.regularNoteUrl+"GetAllByUserId?userId="+user.userId,{headers:headers});
  }
}
