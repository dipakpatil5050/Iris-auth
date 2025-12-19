using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using IrisAuth.Helpers;
namespace IrisAuth.Services
{
    public class WindowsGroupService
    {
        //public void CreateLocalGroup(string groupName)
        //{
        //    if (!WindowsSecurityConfig.EnableGroupCreation)
        //        return;

        //    var finalGroupName =
        //        $"{WindowsSecurityConfig.GroupPrefix}{groupName}".Trim();

        //    using (var ctx = new PrincipalContext(ContextType.Machine))
        //    {
        //        if (GroupPrincipal.FindByIdentity(ctx, finalGroupName) != null)
        //            return;

        //        var group = new GroupPrincipal(ctx)
        //        {
        //            Name = finalGroupName,
        //            Description = WindowsSecurityConfig.GroupDescription
        //        };

        //        group.Save();
        //    }
        //}
        public void CreateLocalGroup(string groupName)
        {
            if (!WindowsSecurityConfig.EnableGroupCreation)
                return;

            using (var ctx = WindowsPrincipalContextFactory.Create())
            {
                if (GroupPrincipal.FindByIdentity(ctx, groupName) != null)
                    return;

                var group = new GroupPrincipal(ctx)
                {
                    Name = groupName,
                    Description = WindowsSecurityConfig.GroupDescription
                };

                group.Save();
            }
        }
    }
}
