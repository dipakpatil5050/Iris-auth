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
    public class UserAccountRepository : RepositoryBase
    {
        // =========================
        // CREATE USER
        // =========================
        public void CreateUser(string username, string passwordHash, int groupId)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand("usp_CreateUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                cmd.Parameters.AddWithValue("@GroupId", groupId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =========================
        // READ USERS
        // =========================
        public List<UserAccountModel> GetUsers()
        {
            var users = new List<UserAccountModel>();

            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT  
            u.UserId,
            u.Username,
            u.GroupId,
            g.GroupName,
            u.IsBlocked,
            u.FailedAttempts,
            u.IsBiometricEnabled,
            u.LastLogin,
            g.Permission1,
            g.Permission2,
            g.Permission3,
            g.Permission4,
            g.Permission5,
            g.Permission6
        FROM UserAccounts u
        INNER JOIN GroupPermissions g ON u.GroupId = g.GroupId
        WHERE u.Is_Active = 0 AND g.Is_Active = 0
        ORDER BY u.Username
    ", conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        users.Add(new UserAccountModel
                        {
                            UserId = r.GetInt32(0),
                            Username = r.GetString(1),
                            GroupId = r.GetInt32(2),
                            GroupName = r.GetString(3),

                            IsBlocked = r.GetInt32(4) == 1,
                            FailedAttempts = r.GetInt32(5),
                            IsBiometricEnabled = r.GetBoolean(6),
                            LastLogin = r.IsDBNull(7)
                                ? (DateTime?)null
                                : r.GetDateTime(7),

                            Permission1 = r.GetBoolean(8),
                            Permission2 = r.GetBoolean(9),
                            Permission3 = r.GetBoolean(10),
                            Permission4 = r.GetBoolean(11),
                            Permission5 = r.GetBoolean(12),
                            Permission6 = r.GetBoolean(13),
                        });
                    }
                }
            }

            return users;
        }

        // =========================
        // UPDATE USER GROUP
        // =========================
        public void UpdateUserGroup(int userId, int groupId)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand("usp_UpdateUserGroup", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@GroupId", groupId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =========================
        // BLOCK USER
        // =========================
        public void BlockUser(int userId)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand("usp_BlockUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =========================
        // UNBLOCK USER
        // =========================
        public void UnblockUser(int userId)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand("usp_UnblockUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<GroupPermissionsModel> GetGroups()
        {
            var list = new List<GroupPermissionsModel>();

            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(
                "SELECT GroupId, GroupName FROM GroupPermissions WHERE Is_Active = 0", conn))
            {
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new GroupPermissionsModel
                        {
                            GroupId = rdr.GetInt32(0),
                            GroupName = rdr.GetString(1)
                        });
                    }
                }
            }
            return list;
        }
        public void UpdateUser(int userId, int groupId, bool biometric)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(@"
        UPDATE UserAccounts
        SET GroupId = @GroupId,
            IsBiometricEnabled = @Bio,
            UpdatedAt = SYSDATETIME()
        WHERE UserId = @UserId", conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@GroupId", groupId);
                cmd.Parameters.AddWithValue("@Bio", biometric);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
            public void SaveBiometric(
                int userId,
                string leftBase64,
                string rightBase64,
                int leftQuality,
                int rightQuality)
            {
                byte[] leftBytes = Convert.FromBase64String(leftBase64);
                byte[] rightBytes = Convert.FromBase64String(rightBase64);

                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(@"
            UPDATE UserAccounts
            SET
                IrisLeftBase64 = @LeftIris,
                IrisRightBase64 = @RightIris,
                IrisLeftQuality = @LeftQuality,
                IrisRightQuality = @RightQuality,
                IsBiometricEnabled = 1,
                UpdatedAt = SYSDATETIME()
            WHERE UserId = @UserId", conn))
                {
                    cmd.Parameters.Add("@LeftIris", SqlDbType.VarBinary).Value = leftBytes;
                    cmd.Parameters.Add("@RightIris", SqlDbType.VarBinary).Value = rightBytes;
                    cmd.Parameters.Add("@LeftQuality", SqlDbType.Int).Value = leftQuality;
                    cmd.Parameters.Add("@RightQuality", SqlDbType.Int).Value = rightQuality;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
    }
}
