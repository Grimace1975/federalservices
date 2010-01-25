using Microsoft.ManagementConsole;
using System;
using Federal.Model;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node
{
	/// <summary>
	/// Server-Role Node
	/// </summary>
	public class SecurityServerRolesNode : NodeBase
	{
		/// <summary>
		/// The constructor.
		/// </summary>
		public SecurityServerRolesNode()
		{
			//+ define node
			DisplayName = "Server Roles";

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
			StandardCollectionExpand(status, new SecurityServerRoleModel.Fetch(), (m => new SecurityServerRoleNode(m)));
		}
	}
}
