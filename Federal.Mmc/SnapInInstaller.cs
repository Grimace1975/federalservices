using Microsoft.ManagementConsole;
using System.ComponentModel;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal
{
    /// <summary>
    /// The RunInstaller attribute allows the .Net framework to install the assembly.
    /// </summary>
    [RunInstaller(true)]
    public class SnapInInstaller : Microsoft.ManagementConsole.SnapInInstaller
    {
    }
}
