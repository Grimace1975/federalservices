using Microsoft.ManagementConsole;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node.Database
{
	/// <summary>
	/// Security Node
	/// </summary>
	public class DatabaseSecurityNode : NodeBase
	{
		/// <summary>
		/// The constructor.
		/// </summary>
		public DatabaseSecurityNode()
		{
			//+ define node
			DisplayName = "Security";
			Children.AddRange(new ScopeNode[] { new Security.DatabaseSecurityLoginsNode(), new Security.DatabaseSecurityRolesNode() });

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
