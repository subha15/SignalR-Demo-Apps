using Microsoft.AspNet.SignalR;
using SignalRDemo.Models;
using SignalRDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRDemo.Hubs
{
    public class DemoHub : Hub
    {
        public readonly JobService _jobService;

        public DemoHub()
        {
            this._jobService = JobService.Instance;
        }

        public void JoinGroup(string groupName)
        {
            Groups.Add(Context.ConnectionId,groupName);
        }

        public void RemoveGroup(string groupName)
        {
            Groups.Remove(Context.ConnectionId, groupName);
        }

        public void RouteServer(SignalRMessage message)
        {
            switch (message.Message)
            {
                case "JobService":
                    _jobService.RunService(message);
                    break;
                
            }
        }
    }
}