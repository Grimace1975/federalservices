using Microsoft.ManagementConsole;
using System;
using Federal.Model;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node
{
    /// <summary>
    /// Single Server-Role Node
    /// </summary>
    public class SecurityServerRoleNode : NodeBase
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        public SecurityServerRoleNode(SecurityServerRoleModel serverRoleModel)
        {
            ServerRoleModel = serverRoleModel;
            //+ define node
            DisplayName = serverRoleModel.Name;
            ImageIndex = SelectedImageIndex = (int)Federal.ImageIndex.Role;

            //+ define verbs
            EnabledStandardVerbs = StandardVerbs.Refresh;
        }

        /// <summary>
        /// Gets or sets the server-role model.
        /// </summary>
        /// <value>The server-role model.</value>
        public SecurityServerRoleModel ServerRoleModel { get; set; }
    }
}
