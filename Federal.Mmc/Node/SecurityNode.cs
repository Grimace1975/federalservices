using Microsoft.ManagementConsole;
using System;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node
{
	/// <summary>
	/// Security Node
	/// </summary>
	public class SecurityNode : NodeBase
	{
		/// <summary>
		/// The constructor.
		/// </summary>
		public SecurityNode()
		{
			//+ define node
			DisplayName = "Security";
			Children.AddRange(new ScopeNode[] { new SecurityLoginsNode(), new SecurityServerRolesNode() });

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
	}
}
