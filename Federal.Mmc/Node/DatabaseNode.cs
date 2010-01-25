using Microsoft.ManagementConsole;
using Federal.Model.Database;
using System;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node
{
	/// <summary>
	/// Single-Database Node
	/// </summary>
	public class DatabaseNode : NodeBase
	{
		/// <summary>
		/// The constructor.
		/// </summary>
		public DatabaseNode(DatabaseModel databaseModel)
		{
			DatabaseModel = databaseModel;
			//+ define node
			DisplayName = databaseModel.Name;
			int imageIndex;
			switch (databaseModel.Status)
			{
				case DatabaseStatus.Default: imageIndex = (int)Federal.ImageIndex.Database; break;
				case DatabaseStatus.EmergencyMode: imageIndex = (int)Federal.ImageIndex.DatabaseEmergencyMode; break;
				case DatabaseStatus.InRecovery: imageIndex = (int)Federal.ImageIndex.DatabaseInRecovery; break;
				case DatabaseStatus.Offline: imageIndex = (int)Federal.ImageIndex.DatabaseOffline; break;
				case DatabaseStatus.ReadOnly: imageIndex = (int)Federal.ImageIndex.DatabaseReadOnly; break;
				case DatabaseStatus.Restoring: imageIndex = (int)Federal.ImageIndex.DatabaseRestoring; break;
				case DatabaseStatus.SingleUser: imageIndex = (int)Federal.ImageIndex.DatabaseSingleUser; break;
				case DatabaseStatus.Suspect: imageIndex = (int)Federal.ImageIndex.DatabaseSuspect; break;
				default:
					throw new InvalidOperationException();
			}
			ImageIndex = SelectedImageIndex = imageIndex;

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
		/// Gets or sets the database model.
		/// </summary>
		/// <value>The database model.</value>
		public DatabaseModel DatabaseModel { get; set; }

		/// <summary>
		/// Called when [expand].
		/// </summary>
		/// <param name="status">The status.</param>
		protected override void OnExpand(AsyncStatus status)
		{
			if (DatabaseModel.Status == DatabaseStatus.Offline)
			{
				return;
			}
			StandardSingleExpand(status, new DatabaseModel.Access.Fetch(DatabaseModel), delegate(DatabaseModel.Access access)
			{
				var databaseModel = DatabaseModel;
				databaseModel.SetAccess(access);
				if (!databaseModel.HasAccess)
				{
					return null;
				}
				if (databaseModel.NeuroxContext != null)
				{
					return new ScopeNode[] { new Database.DatabaseOtherNode(), new Database.DatabaseSecurityNode() };
				}
				return new ScopeNode[] { new Database.DatabaseSecurityNode() };
			});
		}
	}
}
