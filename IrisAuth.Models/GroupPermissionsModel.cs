using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisAuth.Models
{
    public class GroupPermissionsModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int? LoginTimeout { get; set; }
        public int? LoginType { get; set; }

        public bool Permission1 { get; set; }
        public bool Permission2 { get; set; }
        public bool Permission3 { get; set; }
        public bool Permission4 { get; set; }
        public bool Permission5 { get; set; }
        public bool Permission6 { get; set; }

        public bool IsActive { get; set; }

        // ================= DISPLAY HELPERS =================

        public string LoginTypeText =>
            LoginType == 1 ? "Off" :
            LoginType == 2 ? "Absolute" :
            LoginType == 3 ? "Inactive" :
            "-";

        // Convert seconds → minutes for UI
        public string LoginTimeoutMinutes =>
            (LoginType == 2 || LoginType == 3) && LoginTimeout.HasValue
                ? (LoginTimeout.Value / 60).ToString()
                : "-";
    }

}
