using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;

namespace IrisAuth.Services
{
    public class WindowsAccountService
    {
        private readonly PrincipalContext _context;

        public WindowsAccountService()
        {
            _context = new PrincipalContext(ContextType.Machine);
        }

        // -----------------------------
        // CREATE LOCAL WINDOWS USER
        // -----------------------------
        public void CreateUser(string username, string password)
        {
            if (UserPrincipal.FindByIdentity(_context, username) != null)
                return; // already exists

            var user = new UserPrincipal(_context);
            user.SamAccountName = username;
            user.Enabled = true;
            user.PasswordNeverExpires = true;

            // ✅ CORRECT: call method explicitly
            user.SetPassword(password);

            user.Save();
        }

        // -----------------------------
        // CREATE LOCAL WINDOWS GROUP
        // -----------------------------
        public void CreateGroup(string groupName)
        {
            if (GroupPrincipal.FindByIdentity(_context, groupName) != null)
                return;

            var group = new GroupPrincipal(_context);
            group.Name = groupName;
            group.Description = "Created by IrisAuth";

            group.Save();
        }

        // -----------------------------
        // ASSIGN USER TO GROUP
        // -----------------------------
        public void AddUserToGroup(string username, string groupName)
        {
            var user = UserPrincipal.FindByIdentity(_context, username);
            if (user == null)
                throw new Exception("Windows user not found");

            var group = GroupPrincipal.FindByIdentity(_context, groupName);
            if (group == null)
                throw new Exception("Windows group not found");

            if (!group.Members.Contains(user))
            {
                group.Members.Add(user);
                group.Save();
            }
        }
    }
}
