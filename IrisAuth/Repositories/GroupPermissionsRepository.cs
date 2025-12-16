using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IrisAuth.Models;
namespace IrisAuth.Repositories
{
    public class GroupPermissionsRepository : RepositoryBase
    {
        // =========================
        // CREATE GROUP
        // =========================
        public int CreateGroup(GroupPermissionsModel model)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(@"
                INSERT INTO GroupPermissions
                (
                    GroupName,
                    LoginTimeout,
                    LoginType,
                    Permission1,
                    Permission2,
                    Permission3,
                    Permission4,
                    Permission5,
                    Permission6,
                    Is_Active,
                    CreatedAt,
                    UpdatedAt
                )
                OUTPUT INSERTED.GroupId
                VALUES
                (
                    @GroupName,
                    @LoginTimeout,
                    @LoginType,
                    @P1,@P2,@P3,@P4,@P5,@P6,
                    0,
                    SYSDATETIME(),
                    SYSDATETIME()
                )", conn))
            {
                cmd.Parameters.AddWithValue("@GroupName", model.GroupName);
                cmd.Parameters.AddWithValue("@LoginTimeout", (object)model.LoginTimeout ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoginType", (object)model.LoginType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@P1", model.Permission1);
                cmd.Parameters.AddWithValue("@P2", model.Permission2);
                cmd.Parameters.AddWithValue("@P3", model.Permission3);
                cmd.Parameters.AddWithValue("@P4", model.Permission4);
                cmd.Parameters.AddWithValue("@P5", model.Permission5);
                cmd.Parameters.AddWithValue("@P6", model.Permission6);

                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        // =========================
        // READ GROUPS
        // =========================
        public List<GroupPermissionsModel> GetGroups()
        {
            var list = new List<GroupPermissionsModel>();

            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(@"
                SELECT
                    GroupId,
                    GroupName,
                    LoginTimeout,
                    LoginType,
                    Permission1,
                    Permission2,
                    Permission3,
                    Permission4,
                    Permission5,
                    Permission6,
                    Is_Active
                FROM GroupPermissions
                WHERE Is_Active = 0
                ORDER BY GroupName", conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new GroupPermissionsModel
                        {
                            GroupId = r.GetInt32(0),
                            GroupName = r.GetString(1),
                            LoginTimeout = r.IsDBNull(2) ? (int?)null : r.GetInt32(2),
                            LoginType = r.IsDBNull(3) ? (int?)null : r.GetInt32(3),
                            Permission1 = r.GetBoolean(4),
                            Permission2 = r.GetBoolean(5),
                            Permission3 = r.GetBoolean(6),
                            Permission4 = r.GetBoolean(7),
                            Permission5 = r.GetBoolean(8),
                            Permission6 = r.GetBoolean(9),
                            IsActive = r.GetInt32(10) == 0
                        });
                    }
                }
            }

            return list;
        }

        // =========================
        // UPDATE GROUP
        // =========================
        public void UpdateGroup(GroupPermissionsModel model)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(@"
                UPDATE GroupPermissions
                SET
                    LoginTimeout = @LoginTimeout,
                    LoginType = @LoginType,
                    Permission1 = @P1,
                    Permission2 = @P2,
                    Permission3 = @P3,
                    Permission4 = @P4,
                    Permission5 = @P5,
                    Permission6 = @P6,
                    UpdatedAt = SYSDATETIME()
                WHERE GroupId = @GroupId", conn))
            {
                cmd.Parameters.AddWithValue("@GroupId", model.GroupId);
                cmd.Parameters.AddWithValue("@LoginTimeout", (object)model.LoginTimeout ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoginType", (object)model.LoginType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@P1", model.Permission1);
                cmd.Parameters.AddWithValue("@P2", model.Permission2);
                cmd.Parameters.AddWithValue("@P3", model.Permission3);
                cmd.Parameters.AddWithValue("@P4", model.Permission4);
                cmd.Parameters.AddWithValue("@P5", model.Permission5);
                cmd.Parameters.AddWithValue("@P6", model.Permission6);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =========================
        // SOFT DELETE GROUP
        // =========================
        public void DeactivateGroup(int groupId)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(@"
                UPDATE GroupPermissions
                SET Is_Active = 1, UpdatedAt = SYSDATETIME()
                WHERE GroupId = @GroupId", conn))
            {
                cmd.Parameters.AddWithValue("@GroupId", groupId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
