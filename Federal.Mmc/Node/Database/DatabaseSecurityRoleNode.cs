using Microsoft.ManagementConsole;
using Federal.Model.Database.Security;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node.Database.Security
{
    /// <summary>
    /// Single Role Node
    /// </summary>
    public class DatabaseSecurityRoleNode : NodeBase
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        public DatabaseSecurityRoleNode(DatabaseSecurityRoleModel roleModel)
        {
            RoleModel = roleModel;
            //+ define node
            DisplayName = roleModel.Name;
			ImageIndex = SelectedImageIndex = (int)Federal.ImageIndex.Role;

            //+ define verbs
            EnabledStandardVerbs = StandardVerbs.Refresh;
        }

        /// <summary>
        /// Gets or sets the role model.
        /// </summary>
        /// <value>The role model.</value>
        public DatabaseSecurityRoleModel RoleModel { get; set; }
    }
}
