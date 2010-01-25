using Microsoft.ManagementConsole;
using MmcAction = Microsoft.ManagementConsole.Action;
using System;
using Federal.Model.Database;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node
{
	/// <summary>
	/// Database Node
	/// </summary>
	public class DatabasesNode : NodeBase
	{
		/// <summary>
		/// DataContractMethod
		/// </summary>
		private class DataContractMethod
		{
			public const string AddDatabaseCommand = "AddDatabase";
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		public DatabasesNode()
		{
			//+ define node
			DisplayName = "Databases";

			//+ define verbs
			EnabledStandardVerbs = StandardVerbs.Refresh;
			ActionsPaneItems.Add(new MmcAction("Add Database", "Adds a new database under this node", -1, DataContractMethod.AddDatabaseCommand));

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
		/// Called when [action].
		/// </summary>
		/// <param name="action">The action.</param>
		/// <param name="status">The status.</param>
		protected override void OnAction(MmcAction action, AsyncStatus status)
		{
			switch ((string)action.Tag)
			{
				case DataContractMethod.AddDatabaseCommand:
					//ActionScopeNode actionScopeNode = new ActionScopeNode();
					//actionScopeNode.DisplayName = "Added " + System.DateTime.Now.ToLongTimeString();
					//Children.Add(actionScopeNode);
					break;
			}
		}

		/// <summary>
		/// Called when [expand].
		/// </summary>
		/// <param name="status">The status.</param>
		protected override void OnExpand(AsyncStatus status)
		{
			StandardCollectionExpand(status, new DatabaseModel.Fetch(), (m => new DatabaseNode(m)));
		}
	}
}
