using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using SignalRDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

            using (var hubConnection = new HubConnection("http://localhost:51707/signalr"))
            {
                IHubProxy demoHubProxy = hubConnection.CreateHubProxy("DemoHub");
                demoHubProxy.On<SignalRMessage>("RouteClient", ob =>
                {
                    Console.WriteLine(ob.Message);
                });
                await hubConnection.Start();
                await demoHubProxy.Invoke("JoinGroup", "6000031");
                await demoHubProxy.Invoke("RouteServer", new SignalRMessage { Message = "JobService", SerializedData = JsonConvert.SerializeObject(new { GroupName = "6000031" })});
                Console.ReadKey();
            }
           
        }
    }
}
