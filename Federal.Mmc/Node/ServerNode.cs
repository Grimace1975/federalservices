using Microsoft.ManagementConsole;
using System;
using Federal.Model;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node
{
	/// <summary>
	/// Server Node
	/// </summary>
	public class ServerNode : NodeBase
	{
		/// <summary>
		/// The constructor.
		/// </summary>
		public ServerNode(ServerModel serverModel)
		{
			ServerModel = serverModel;
			//+ define node
			DisplayName = serverModel.DisplayName;
			int imageIndex;
			switch (serverModel.Status)
			{
				case ServerStatus.Paused: imageIndex = (int)Federal.ImageIndex.NeuroxPaused; break;
				case ServerStatus.Started: imageIndex = (int)Federal.ImageIndex.NeuroxStarted; break;
				case ServerStatus.Stopped: imageIndex = (int)Federal.ImageIndex.NeuroxStopped; break;
				case ServerStatus.Unknown: imageIndex = (int)Federal.ImageIndex.NeuroxUnknown; break;
				default:
					throw new InvalidOperationException();
			}
			ImageIndex = SelectedImageIndex = imageIndex;
			Children.AddRange(new ScopeNode[] { new DatabasesNode(), new SecurityNode() });

			//+ define verbs
			EnabledStandardVerbs = StandardVerbs.Refresh;

			//+ define view
			//ViewDescriptions.Add(new FormViewDescription(typeof(ProcessManagerControl))
			//{
			//    DisplayName = "Processes",
			//    ViewType = typeof(ComputerFormView),
			//});
			ViewDescriptions.Add(new MmcListViewDescription()
			{
				DisplayName = "List",
				ViewType = typeof(SelectionListView),
				Options = MmcListViewOptions.ExcludeScopeNodes,
			});
			ViewDescriptions.DefaultIndex = 0;
		}

		/// <summary>
		/// Gets or sets the server model.
		/// </summary>
		/// <value>The server model.</value>
		public ServerModel ServerModel { get; set; }
	}
}
