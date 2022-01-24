import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@aspnet/signalr';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { ToastrService } from 'ngx-toastr';
import { Subscription, timer } from 'rxjs';
import { map } from 'rxjs/operators';
import { Constants } from 'src/app/Helper/constants';
import { ReminderNoteService } from 'src/app/services/note-management-services/reminder-note.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private router: Router,private toastr: ToastrService, private httpClient:HttpClient, private reminderService:ReminderNoteService) { }
  private _hubConnection?: HubConnection;
  signaldata: any[]=[];
  timerSubscription?: Subscription; 
  ngOnInit(): void {
    this._hubConnection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Debug)
    .withUrl(Constants.API_KEY+'notify', {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets
    })
    .build();
    this._hubConnection.start()
    .then(()=>
    console.log('connection start'))
    .catch(err=>{
      console.log('Error while establishing the connection')
    })
    let user = JSON.parse(localStorage.getItem(Constants.USER_KEY) || '{}');
    this._hubConnection.on('BroadcastMessage', (message)=>{
      if(message.userId==user.userId){
        this.signaldata.push(message);
      }
    })
    this.timerSubscription = timer(0, 60000).pipe( 
      map(() => { 
        this.sendRequestForDo(); // load data contains the http request 
      }) 
    ).subscribe();
  }
  sendRequestForDo() {
    this.reminderService.sendRequestForDo().subscribe((data: any) => {
    }, error => {
    })
  } 
  onLogout() {
    localStorage.removeItem(Constants.USER_KEY);
    this.router.navigate(["/sign-in"]);
    this.toastr.success("Logout");
  }
}
