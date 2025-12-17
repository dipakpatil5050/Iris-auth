using IrisAuth.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisAuth.Repositories
{
    public class GroupRepository : RepositoryBase
    {
        public List<GroupPermissionsModel> GetGroups()
        {
            var list = new List<GroupPermissionsModel>();

            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(@"
                SELECT GroupId, GroupName,
                       Permission1, Permission2, Permission3,
                       Permission4, Permission5, Permission6
                FROM GroupPermissions
                WHERE Is_Active = 0", conn))
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
                            Permission1 = r.GetBoolean(2),
                            Permission2 = r.GetBoolean(3),
                            Permission3 = r.GetBoolean(4),
                            Permission4 = r.GetBoolean(5),
                            Permission5 = r.GetBoolean(6),
                            Permission6 = r.GetBoolean(7)
                        });
                    }
                }
            }
            return list;
        }

        public void CreateGroup(GroupPermissionsModel g)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(@"
                INSERT INTO GroupPermissions
                (GroupName, Permission1, Permission2, Permission3,
                 Permission4, Permission5, Permission6)
                VALUES
                (@Name,@P1,@P2,@P3,@P4,@P5,@P6)", conn))
            {
                cmd.Parameters.AddWithValue("@Name", g.GroupName);
                cmd.Parameters.AddWithValue("@P1", g.Permission1);
                cmd.Parameters.AddWithValue("@P2", g.Permission2);
                cmd.Parameters.AddWithValue("@P3", g.Permission3);
                cmd.Parameters.AddWithValue("@P4", g.Permission4);
                cmd.Parameters.AddWithValue("@P5", g.Permission5);
                cmd.Parameters.AddWithValue("@P6", g.Permission6);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateGroup(GroupPermissionsModel g)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(@"
                UPDATE GroupPermissions SET
                    GroupName=@Name,
                    Permission1=@P1,
                    Permission2=@P2,
                    Permission3=@P3,
                    Permission4=@P4,
                    Permission5=@P5,
                    Permission6=@P6
                WHERE GroupId=@Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", g.GroupId);
                cmd.Parameters.AddWithValue("@Name", g.GroupName);
                cmd.Parameters.AddWithValue("@P1", g.Permission1);
                cmd.Parameters.AddWithValue("@P2", g.Permission2);
                cmd.Parameters.AddWithValue("@P3", g.Permission3);
                cmd.Parameters.AddWithValue("@P4", g.Permission4);
                cmd.Parameters.AddWithValue("@P5", g.Permission5);
                cmd.Parameters.AddWithValue("@P6", g.Permission6);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteGroup(int groupId)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(
                "UPDATE GroupPermissions SET Is_Active=1 WHERE GroupId=@Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", groupId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
