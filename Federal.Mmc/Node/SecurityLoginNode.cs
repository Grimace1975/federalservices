using Microsoft.ManagementConsole;
using System;
using Federal.Model;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node
{
    /// <summary>
    /// Single Login Node
    /// </summary>
    public class SecurityLoginNode : NodeBase
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        public SecurityLoginNode(SecurityLoginModel loginModel)
        {
            LoginModel = loginModel;
            //+ define node
            DisplayName = loginModel.Name;
            int imageIndex;
            switch (loginModel.LoginType)
            {
                case SecurityLoginType.AsymmetricKey: imageIndex = (int)Federal.ImageIndex.Login; break;
				case SecurityLoginType.Certificate: imageIndex = (int)Federal.ImageIndex.Login; break;
				case SecurityLoginType.Group: imageIndex = (int)Federal.ImageIndex.NtGroup; break;
				case SecurityLoginType.SqlServer: imageIndex = (int)Federal.ImageIndex.Login; break;
				case SecurityLoginType.User: imageIndex = (int)Federal.ImageIndex.Login; break;
                default:
                    throw new InvalidOperationException();
            }
            ImageIndex = SelectedImageIndex = imageIndex;

            //+ define verbs
            EnabledStandardVerbs = StandardVerbs.Refresh;
        }

        /// <summary>
        /// Gets or sets the login model.
        /// </summary>
        /// <value>The login model.</value>
        public SecurityLoginModel LoginModel { get; set; }
    }
}
