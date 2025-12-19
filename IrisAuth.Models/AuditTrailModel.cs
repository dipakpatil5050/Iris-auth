using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisAuth.Models
{
    public class AuditTrailModel
    {
        public DateTime EventTime { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public string IpAddress { get; set; }
        public string HostName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Reason { get; set; }
    }
}
