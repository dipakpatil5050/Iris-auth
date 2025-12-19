using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using IrisAuth.Helpers;
namespace IrisAuth.Services
{

    public class WindowsUserService
    {
        public void CreateLocalUser(string username, string groupName, string password)
        {
            if (!WindowsSecurityConfig.EnableUserCreation)
                return;

            var finalUser =
                $"{WindowsSecurityConfig.UserPrefix}{username}".Trim();

            var finalGroup =
                $"{WindowsSecurityConfig.GroupPrefix}{groupName}".Trim();

            using (var ctx = WindowsPrincipalContextFactory.Create())
            {
                var user = UserPrincipal.FindByIdentity(ctx, finalUser);

                if (user == null)
                {
                    user = new UserPrincipal(ctx)
                    {
                        SamAccountName = finalUser,
                        Name = finalUser,
                        Description = WindowsSecurityConfig.UserDescription
                    };

                    //user.SetPassword(WindowsSecurityConfig.DefaultUserPassword);
                    // ✅ USE USER ENTERED PASSWORD
                    user.SetPassword(password);

                    if (WindowsSecurityConfig.ForcePasswordChange)
                        user.ExpirePasswordNow();

                    user.Save();
                }

                if (WindowsSecurityConfig.AutoAddUserToGroup)
                {
                    var group = GroupPrincipal.FindByIdentity(ctx, finalGroup);
                    group?.Members.Add(user);
                    group?.Save();
                }
            }
        }

        public void SyncUserGroup(string username, string oldGroup, string newGroup)
        {
            using (var ctx = WindowsPrincipalContextFactory.Create())
            {
                var user = UserPrincipal.FindByIdentity(ctx, username);
                if (user == null) return;

                if (!string.IsNullOrWhiteSpace(oldGroup))
                {
                    var oldGrp = GroupPrincipal.FindByIdentity(ctx, oldGroup);
                    oldGrp?.Members.Remove(user);
                    oldGrp?.Save();
                }

                if (!string.IsNullOrWhiteSpace(newGroup))
                {
                    var newGrp = GroupPrincipal.FindByIdentity(ctx, newGroup);
                    newGrp?.Members.Add(user);
                    newGrp?.Save();
                }
            }
        }
        public void DeleteLocalUser(string username)
        {
            if (!WindowsSecurityConfig.EnableUserCreation)
                return;

            using (var ctx = WindowsPrincipalContextFactory.Create())
            {
                var user = UserPrincipal.FindByIdentity(ctx, username);
                if (user == null)
                    return;

                user.Delete();
            }
        }
    }
}
