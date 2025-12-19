using IrisAuth.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IrisAuth.Repositories
{
    public class AuditTrailRepository : RepositoryBase
    {
        public void Insert(AuditTrailModel audit)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
INSERT INTO AuditTrail
(Username, Action, Details, IpAddress, HostName, OldValue, NewValue, Reason)
VALUES
(@Username, @Action, @Details, @IpAddress, @HostName, @OldValue, @NewValue, @Reason)", conn))
            {
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = audit.Username;
                cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 50).Value = audit.Action;
                cmd.Parameters.Add("@Details", SqlDbType.NVarChar, 255).Value =
                    (object)audit.Details ?? DBNull.Value;
                cmd.Parameters.Add("@IpAddress", SqlDbType.NVarChar, 50).Value =
                    (object)audit.IpAddress ?? DBNull.Value;
                cmd.Parameters.Add("@HostName", SqlDbType.NVarChar, 100).Value =
                    (object)audit.HostName ?? DBNull.Value;
                cmd.Parameters.Add("@OldValue", SqlDbType.NVarChar).Value =
                    (object)audit.OldValue ?? DBNull.Value;
                cmd.Parameters.Add("@NewValue", SqlDbType.NVarChar).Value =
                    (object)audit.NewValue ?? DBNull.Value;
                cmd.Parameters.Add("@Reason", SqlDbType.NVarChar, 500).Value =
                    (object)audit.Reason ?? DBNull.Value;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // ================= READ (AUDIT REPORT) =================
        public List<AuditTrailModel> GetAuditReport(
            DateTime? fromDate,
            DateTime? toDate,
            string username,
            string action)
        {
            var list = new List<AuditTrailModel>();

            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
SELECT
    AuditId,
    EventTime,
    Username,
    Action,
    Details,
    OldValue,
    NewValue,
    Reason,
    IpAddress,
    HostName
FROM AuditTrail
WHERE
    (@FromDate IS NULL OR EventTime >= @FromDate)
AND (@ToDate IS NULL OR EventTime <= @ToDate)
AND (@Username IS NULL OR Username = @Username)
AND (@Action IS NULL OR Action = @Action)
ORDER BY EventTime DESC", conn))
            {
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime2).Value =
                    fromDate.HasValue ? (object)fromDate.Value : DBNull.Value;

                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime2).Value =
                    toDate.HasValue ? (object)toDate.Value : DBNull.Value;

                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value =
                    string.IsNullOrWhiteSpace(username)
                        ? (object)DBNull.Value
                        : (object)username;

                cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 50).Value =
                    string.IsNullOrWhiteSpace(action)
                        ? (object)DBNull.Value
                        : (object)action;

                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new AuditTrailModel
                        {
                            EventTime = r.GetDateTime(r.GetOrdinal("EventTime")),
                            Username = r.GetString(r.GetOrdinal("Username")),
                            Action = r.GetString(r.GetOrdinal("Action")),
                            Details = r["Details"] as string,
                            OldValue = r["OldValue"] as string,
                            NewValue = r["NewValue"] as string,
                            Reason = r["Reason"] as string,
                            IpAddress = r["IpAddress"] as string,
                            HostName = r["HostName"] as string
                        });
                    }
                }
            }

            return list;
        }
    }
}
