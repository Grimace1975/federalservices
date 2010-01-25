using Microsoft.ManagementConsole;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]
//: http://msdn.microsoft.com/en-us/library/microsoft.managementconsole.status.reportprogress(VS.85).aspx

namespace Federal
{
	/// <summary>
	/// The main entry point for the creation of the snap-in.
	/// </summary>
	[SnapInSettings("{CE255EF6-9E3D-42c8-B725-95CCC761B9DF}",
		DisplayName = "- Federal",
		Description = "Federal Explorer")
	]
	/*ConfigurationFile = @"C:\_APPLICATION2\NEUROX\MMC3\Neurox.Mmc\app.config" */
	public class SnapIn : Microsoft.ManagementConsole.SnapIn
	{
		/// <summary>
		/// Initializes the <see cref="SnapIn"/> class.
		/// </summary>
		static SnapIn()
		{
			Environment.Startup();
		}
		/// <summary>
		/// The constructor.
		/// </summary>
		public SnapIn()
		{
			Resource_.DefineImageIndex(this);
			RootNode = new Node.ServerNode(new Model.ServerModel());
			//+ tells mmc to save custom data
			//IsModified = true;
		}

		///// <summary>
		///// Shows the Initialization Wizard when the snapin is added to console
		///// </summary>
		///// <returns>
		///// true to continue loading the snap-in. false cancels snap-in loading.
		///// </returns>
		//protected override bool OnShowInitializationWizard()
		//{
		//    SnapInInitializationWizard initializationWizard = new SnapInInitializationWizard();
		//    bool isOk = (initializationWizard.ShowDialog() == System.Windows.Forms.DialogResult.OK);
		//    if (isOk == true)
		//    {
		//        //    this.RootNode.DisplayName = initializationWizard.SelectedSnapInName;
		//    }
		//    return isOk;
		//}

		//#region CUSTOM DATA
		///// <summary>
		///// Load in any saved data
		///// </summary>
		///// <param name="status">asynchronous status for updating the console</param>
		///// <param name="persistenceData">binary data stored in the console file</param>
		//protected override void OnLoadCustomData(AsyncStatus status, byte[] persistenceData)
		//{
		//    //// saved name? then set snap-in to the name
		//    //if (string.IsNullOrEmpty(Encoding.Unicode.GetString(persistenceData)))
		//    //{
		//    //    this.RootNode.DisplayName = "Unknown";
		//    //}
		//    //else
		//    //{
		//    //    this.RootNode.DisplayName = Encoding.Unicode.GetString(persistenceData);
		//    //}
		//}

		///// <summary>
		///// If snapIn 'IsModified', then save data
		///// </summary>
		///// <param name="status">status for updating the console</param>
		///// <returns>
		///// binary data to be stored in the console file
		///// </returns>
		//protected override byte[] OnSaveCustomData(SyncStatus status)
		//{
		//    return null;
		//    //return Encoding.Unicode.GetBytes(this.RootNode.DisplayName);
		//}
		//#endregion
	}
}
