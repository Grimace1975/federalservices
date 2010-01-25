using Microsoft.ManagementConsole;
using Federal.Model.Database.Security;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node.Database.Security
{
	/// <summary>
	/// Role Node
	/// </summary>
	public class DatabaseSecurityRolesNode : NodeBase
	{
		/// <summary>
		/// The constructor.
		/// </summary>
		public DatabaseSecurityRolesNode()
		{
			//+ define node
			DisplayName = "Roles";

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
			var databaseModel = ((DatabaseNode)Parent.Parent).DatabaseModel;
			StandardCollectionExpand(status, new DatabaseSecurityRoleModel.Fetch(databaseModel), (m => new DatabaseSecurityRoleNode(m)));
		}
	}
}
