using Microsoft.ManagementConsole;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node.Database
{
	/// <summary>
	/// OtherNode
	/// </summary>
	public class DatabaseOtherNode : NodeBase
	{
		/// <summary>
		/// The constructor.
		/// </summary>
		public DatabaseOtherNode()
		{
			//+ define node
			DisplayName = "Other";

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
