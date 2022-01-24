import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Constants } from 'src/app/Helper/constants';
import { TodoNoteModel } from 'src/app/models/note-management-models/todo-note-model';
import { ResponseModel } from 'src/app/models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class TodoNoteService {

  constructor(private httpClient:HttpClient) { }
  public todoNoteModel: TodoNoteModel = new TodoNoteModel();
  private readonly todoNoteUrl: string = Constants.API_KEY+"api/Todo/";
  public insert() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    this.todoNoteModel.userId=user.userId;
    return this.httpClient.post<ResponseModel>(this.todoNoteUrl + "Insert", this.todoNoteModel,{headers:headers});
  }

  public update() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    this.todoNoteModel.userId=user.userId;
    return this.httpClient.put<ResponseModel>(this.todoNoteUrl + "Update", this.todoNoteModel,{headers:headers});
  }

  public delete(id: number) {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    this.todoNoteModel.userId=user.userId;
    return this.httpClient.delete(this.todoNoteUrl + "Delete?id="+id,{headers:headers});
  }
  public getById(id:number) {
    return this.httpClient.get<ResponseModel>(this.todoNoteUrl+"GetById?id="+id);
  }
  public getAllByUserId() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    return this.httpClient.get<ResponseModel>(this.todoNoteUrl+"GetAllByUserId?userId="+user.userId,{headers:headers});
  }
  public getTodayTodoList() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    return this.httpClient.get<ResponseModel>(this.todoNoteUrl+"GetTodayTodoList?userId="+user.userId,{headers:headers});
  }
  public getWeeklyTodoList() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    return this.httpClient.get<ResponseModel>(this.todoNoteUrl+"GetWeeklyTodoList?userId="+user.userId,{headers:headers});
  }
  public getMonthlyTodoList() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    return this.httpClient.get<ResponseModel>(this.todoNoteUrl+"GetMonthlyTodoList?userId="+user.userId,{headers:headers});
  }
}
