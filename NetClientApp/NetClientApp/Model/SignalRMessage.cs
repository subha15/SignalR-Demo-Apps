using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRDemo.Models
{
    public class SignalRMessage
    {
        public string Message { get; set; }
        public string SerializedData { get; set; }
    }
}