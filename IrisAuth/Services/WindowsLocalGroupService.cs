using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
namespace IrisAuth.Services
{
    public class WindowsLocalGroupService
    {
        private readonly PrincipalContext _context =
            new PrincipalContext(ContextType.Machine);

        // -------------------------
        // Ensure local group exists
        // -------------------------
        public void EnsureGroup(string groupName)
        {
            var group = GroupPrincipal.FindByIdentity(_context, groupName);
            if (group != null)
                return;

            group = new GroupPrincipal(_context)
            {
                Name = groupName,
                Description = "Created by IrisAuth"
            };
            group.Save();
        }

        // -------------------------
        // Create local user
        // -------------------------
        public void CreateUser(string username, string password)
        {
            using (var context = new PrincipalContext(ContextType.Machine))
            {
                var user = UserPrincipal.FindByIdentity(context, username);
                if (user != null)
                    return; // user already exists

                user = new UserPrincipal(context);
                user.Name = username;
                user.PasswordNeverExpires = true;
                user.Enabled = true;

                // ✅ Set password using METHOD
                user.SetPassword(password);

                // Save user
                user.Save();
            }
        }


        // -------------------------
        // Assign user to group
        // -------------------------
        public void AddUserToGroup(string username, string groupName)
        {
            var user = UserPrincipal.FindByIdentity(_context, username);
            var group = GroupPrincipal.FindByIdentity(_context, groupName);

            if (user == null || group == null)
                return;

            if (!group.Members.Contains(user))
            {
                group.Members.Add(user);
                group.Save();
            }
        }
    }
}
