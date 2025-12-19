using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using IrisAuth.Helpers;
namespace IrisAuth.Services
{
    public static class WindowsPrincipalContextFactory
    {
        public static PrincipalContext Create()
        {
            if (WindowsSecurityConfig.SecurityMode.Equals(
                "Domain", StringComparison.OrdinalIgnoreCase))
            {
                return new PrincipalContext(
                    ContextType.Domain,
                    WindowsSecurityConfig.DomainName
                );
            }

            // Default: Local machine
            return new PrincipalContext(ContextType.Machine);
        }
    }
}
