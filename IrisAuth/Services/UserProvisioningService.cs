using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrisAuth.Helpers;
using IrisAuth.Repositories;
using IrisAuth.Services;
namespace IrisAuth.Services
{
    public class UserProvisioningService
    {
        private readonly UserAccountRepository _repo;
        private readonly WindowsAccountService _windows;

        public UserProvisioningService()
        {
            _repo = new UserAccountRepository();
            _windows = new WindowsAccountService();
        }

        public void CreateUser(
            string username,
            string plainPassword,
            int groupId,
            string groupName)
        {
            // 1️⃣ Ensure Windows group
            _windows.CreateGroup(groupName);

            // 2️⃣ Create Windows user
            _windows.CreateUser(username, plainPassword);

            // 3️⃣ Assign user to group
            _windows.AddUserToGroup(username, groupName);

            // 4️⃣ Save to DB
            var hash = PasswordHasher.Hash(plainPassword);
            _repo.CreateUser(username, hash, groupId);
        }

        public void UpdateUser(int userId, int groupId, bool biometric)
        {
            _repo.UpdateUser(userId, groupId, biometric);
        }
    }
}
