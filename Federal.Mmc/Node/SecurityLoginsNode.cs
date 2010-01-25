using Microsoft.ManagementConsole;
using System;
using Federal.Model;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node
{
	/// <summary>
	/// Login Node
	/// </summary>
	public class SecurityLoginsNode : NodeBase
	{
		/// <summary>
		/// The constructor.
		/// </summary>
		public SecurityLoginsNode()
		{
			//+ define node
			DisplayName = "Logins";

			//+ define verbs
			EnabledStandardVerbs = StandardVerbs.Refresh;

			//+ define view
			ViewDescriptions.Add(new MmcListViewDescription()
			{
				DisplayName = "List",
				ViewType = typeof(SelectionListView),
				Options = MmcListViewOptions.ExcludeScopeNodes,
			});
			ViewDescriptions.DefaultIndex = 0;
		}

		/// <summary>
		/// Called when [expand].
		/// </summary>
		/// <param name="status">The status.</param>
		protected override void OnExpand(AsyncStatus status)
		{
			StandardCollectionExpand(status, new SecurityLoginModel.Fetch(), (m => new SecurityLoginNode(m)));
		}
	}
}
