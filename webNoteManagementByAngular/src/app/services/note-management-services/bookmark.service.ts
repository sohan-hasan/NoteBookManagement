import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Constants } from 'src/app/Helper/constants';
import { BookmarkModel } from 'src/app/models/note-management-models/bookmark-model';
import { ResponseModel } from 'src/app/models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class BookmarkService {
  
  constructor(private httpClient:HttpClient) { }
  public bookmarkModel: BookmarkModel = new BookmarkModel();
  private readonly bookmarkUrl: string = Constants.API_KEY+"api/Bookmark/";
  public insert() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    this.bookmarkModel.userId=user.userId;
    return this.httpClient.post<ResponseModel>(this.bookmarkUrl + "Insert", this.bookmarkModel,{headers:headers});
  }

  public update() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    this.bookmarkModel.userId=user.userId;
    return this.httpClient.put<ResponseModel>(this.bookmarkUrl + "Update", this.bookmarkModel,{headers:headers});
  }

  public delete(id: number) {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    this.bookmarkModel.userId=user.userId;
    return this.httpClient.delete(this.bookmarkUrl + "Delete?id="+id,{headers:headers});
  }
  public getById(id:number) {
    return this.httpClient.get<ResponseModel>(this.bookmarkUrl+"GetById?id="+id);
  }
  public getAllByUserId() {
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    const headers=new HttpHeaders({
      'Authorization':`Bearer ${user?.token }`
          });
    return this.httpClient.get<ResponseModel>(this.bookmarkUrl+"GetAllByUserId?userId="+user.userId,{headers:headers});
  }
}
