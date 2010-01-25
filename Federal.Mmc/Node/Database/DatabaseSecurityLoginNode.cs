using Microsoft.ManagementConsole;
using Federal.Model.Database.Security;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node.Database.Security
{
    /// <summary>
    /// Single Login Node
    /// </summary>
    public class DatabaseSecurityLoginNode : NodeBase
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        public DatabaseSecurityLoginNode(DatabaseSecurityLoginModel loginModel)
        {
            LoginModel = loginModel;
            //+ define node
            DisplayName = loginModel.Name;
            ImageIndex = SelectedImageIndex = (int)Federal.ImageIndex.NtGroup;

            //+ define verbs
            EnabledStandardVerbs = StandardVerbs.Refresh;
        }

        /// <summary>
        /// Gets or sets the login model.
        /// </summary>
        /// <value>The login model.</value>
        public DatabaseSecurityLoginModel LoginModel { get; set; }
    }
}
