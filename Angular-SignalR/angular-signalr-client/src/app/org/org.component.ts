import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { OrgService } from './org.service';
import { SignalRMessage } from 'src/Models/SignalRMessage';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-org',
  templateUrl: './org.component.html',
  styleUrls: ['./org.component.css']
})
export class OrgComponent implements OnInit,OnDestroy {

  constructor(private route : ActivatedRoute,private orgService : OrgService) { }
  @Input() orgId : string
  message : String

  ngOnInit() {
    this.route.params.subscribe(param => {
      this.orgId = param["orgId"];
      this.message = '';
      this.orgService.disconnectSignalR();
      this.orgService.initilizeSignalRSetUp(this.orgId);

      this.orgService.signalRData.subscribe((val : SignalRMessage) => {
            this.message = val.Message;
      })
    });
  }

  runJob() {
    this.orgService.runJob();
  }

  ngOnDestroy() {
    debugger
    this.orgService.disconnectSignalR();
  }

}
