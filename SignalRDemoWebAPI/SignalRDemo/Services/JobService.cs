using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using SignalRDemo.Hubs;
using SignalRDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace SignalRDemo.Services
{
    public class JobService
    {
        public readonly static Lazy<JobService> _instance = new Lazy<JobService>(() => new JobService(GlobalHost.ConnectionManager.GetHubContext<DemoHub>()));
        private IHubContext hubContext = null;
        public JobService(IHubContext hubContext)
        {
            this.hubContext = hubContext;
        }

        public static JobService Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public void RunService(SignalRMessage data)
        {
            dynamic obj = JsonConvert.DeserializeObject(data.SerializedData);
            var groupName = Convert.ToString(obj.GroupName);
            hubContext.Clients.Group(groupName).RouteClient(new SignalRMessage { Message = $"Run Service Invoked from {groupName}",SerializedData = ""});
            Thread.Sleep(2000);
            hubContext.Clients.Group(groupName).RouteClient(new SignalRMessage { Message = $"Job Processed for 2 seconds from {groupName}" ,SerializedData = ""});
        }
    }
}