using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
namespace IrisAuth.Services
{
    public class WindowsGroupService
    {
        public void CreateLocalGroup(string groupName)
        {
            using (var ctx = new PrincipalContext(ContextType.Machine))
            {
                var group = GroupPrincipal.FindByIdentity(ctx, groupName);
                if (group != null)
                    return; // already exists

                group = new GroupPrincipal(ctx)
                {
                    Name = groupName,
                    Description = $"IrisAuth group {groupName}"
                };

                group.Save();
            }
        }
    }
}
