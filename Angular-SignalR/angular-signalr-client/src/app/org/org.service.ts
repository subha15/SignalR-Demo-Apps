import { Injectable } from '@angular/core';
import { SignalRMessage } from '../../Models/SignalRMessage';
import { Subject } from 'rxjs';
declare var $: any;  
@Injectable({
  providedIn: 'root'
})
export class OrgService {
  connection: any;
  proxy: any;
  orgId: string;
  signalRData: Subject<SignalRMessage>;
  isConnected: boolean;
  constructor() {
    this.signalRData = new Subject<SignalRMessage>();
  }

  initilizeSignalRSetUp(orgId) {
    debugger
    this.orgId = orgId;
    if (!this.isConnected) {
      let signalRServerEndPoint = 'http://localhost:51707/signalr';
      this.connection = $.hubConnection(signalRServerEndPoint);
      this.proxy = this.connection.createHubProxy('DemoHub');

      this.proxy.on('RouteClient', (signalRMsg: SignalRMessage) => {
        this.signalrHandler(signalRMsg);
      });

      this.connection.start().done((data) => {
        this.proxy.invoke('JoinGroup', this.orgId);
      });

      this.isConnected = true;
    }
    else {
      this.proxy.invoke('JoinGroup', this.orgId);
    }
  }

  runJob() {
    debugger
    this.proxy.invoke('RouteServer', new SignalRMessage("JobService", JSON.stringify({ GroupName: this.orgId })));
  }

  public signalrHandler(signalRMsg: SignalRMessage) {
    debugger
    this.signalRData.next(signalRMsg);
  }

  public disconnectSignalR() {
    if(this.isConnected === true){
      this.connection.stop();
      this.isConnected = false;
    }
  }

}
