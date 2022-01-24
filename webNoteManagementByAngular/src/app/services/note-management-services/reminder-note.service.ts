import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Constants } from 'src/app/Helper/constants';
import { ReminderNoteModel } from 'src/app/models/note-management-models/reminder-note-model';
import { ResponseModel } from 'src/app/models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class ReminderNoteService {

  constructor(private httpClient:HttpClient) { }
  public reminderNoteModel: ReminderNoteModel = new ReminderNoteModel();
  private readonly reminderNoteUrl: string = Constants.API_KEY+"api/Reminder/";
  private readonly sendNotificationUrl: string = Constants.API_KEY+"api/Message/";
  public insert() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    this.reminderNoteModel.userId=user.userId;
    return this.httpClient.post<ResponseModel>(this.reminderNoteUrl + "Insert", this.reminderNoteModel,{headers:headers});
  }

  public update() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    this.reminderNoteModel.userId=user.userId;
    return this.httpClient.put<ResponseModel>(this.reminderNoteUrl + "Update", this.reminderNoteModel,{headers:headers});
  }

  public delete(id: number) {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    this.reminderNoteModel.userId=user.userId;
    return this.httpClient.delete(this.reminderNoteUrl + "Delete?id="+id,{headers:headers});
  }
  public getById(id:number) {
    return this.httpClient.get<ResponseModel>(this.reminderNoteUrl+"GetById?id="+id);
  }
  public getAllByUserId() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    return this.httpClient.get<ResponseModel>(this.reminderNoteUrl+"GetAllByUserId?userId="+user.userId,{headers:headers});
  }
  public getTodayReminderList() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    return this.httpClient.get<ResponseModel>(this.reminderNoteUrl+"GetTodayReminderList?userId="+user.userId,{headers:headers});
  }
  public getWeeklyReminderList() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    return this.httpClient.get<ResponseModel>(this.reminderNoteUrl+"GetWeeklyReminderList?userId="+user.userId,{headers:headers});
  }
  public getMonthlyReminderList() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    return this.httpClient.get<ResponseModel>(this.reminderNoteUrl+"GetMonthlyReminderList?userId="+user.userId,{headers:headers});
  }
  public sendRequestForDo() {
    return this.httpClient.get<ResponseModel>(this.sendNotificationUrl+"GetRequestForDo");
  }
  
}
