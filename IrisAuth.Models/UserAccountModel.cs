using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisAuth.Models
{
    public class UserAccountModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public int GroupId { get; set; }
        public string GroupName { get; set; }   // 🔑 from JOIN

        public bool IsBlocked { get; set; }
        public int FailedAttempts { get; set; }

        public bool IsBiometricEnabled { get; set; }
        public DateTime? LastLogin { get; set; }

        public bool Permission1 { get; set; }
        public bool Permission2 { get; set; }
        public bool Permission3 { get; set; }
        public bool Permission4 { get; set; }
        public bool Permission5 { get; set; }
        public bool Permission6 { get; set; }
    }
}
