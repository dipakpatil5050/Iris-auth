using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrisAuth.Models;
using IrisAuth.Repositories;
namespace IrisAuth.Services
{
    public class GroupProvisioningService
    {
        private readonly GroupPermissionsRepository _repo;
        private readonly WindowsGroupService _windows;

        public GroupProvisioningService()
        {
            _repo = new GroupPermissionsRepository();
            _windows = new WindowsGroupService();
        }

        public void CreateGroup(GroupPermissionsModel model)
        {
            // 1️⃣ Create Windows local group
            _windows.CreateLocalGroup(model.GroupName);

            // 2️⃣ Create DB group
            _repo.CreateGroup(model);
        }
    }
}
