using System.Data.SqlClient;

namespace Federal.Model.Database.Security
{
	/// <summary>
	/// Login Model
	/// </summary>
	public class DatabaseSecurityLoginModel : ModelBase
	{
		public class Ordinal
		{
			public int Name;
			public int ID;
			public Ordinal(SqlDataReader r)
			{
				Name = r.GetOrdinal("Name");
				ID = r.GetOrdinal("ID");
			}
		}
		/// <summary>
		/// The constructor.
		/// </summary>
		public DatabaseSecurityLoginModel(Ordinal ordinal, SqlDataReader r)
		{
			Name = r.Field<string>(ordinal.Name);
			Id = r.Field<int>(ordinal.ID);
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; protected set; }

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; protected set; }

		#region Fetch
		public class Fetch : CollectionChunkedFetch<Fetch, Ordinal, DatabaseSecurityLoginModel>
		{
			public Fetch(DatabaseModel databaseModel)
				: base(
					(() => KernelDatabase.ReplaceSqlValue(@"
USE [:P0:]
SELECT u.name AS [Name], 'Server[@Name=' + quotename(CAST(serverproperty(N'Servername') AS sysname),'''') + ']' + '/Database[@Name=' + quotename(db_name(),'''') + ']' + '/User[@Name=' + quotename(u.name,'''') + ']' AS [Urn],
u.principal_id AS [ID], CAST(CASE dp.state WHEN N'G' THEN 1 WHEN 'W' THEN 1 ELSE 0 END AS bit) AS [HasDBAccess], u.create_date AS [CreateDate]
FROM sys.database_principals AS u
LEFT OUTER JOIN sys.database_permissions AS dp ON dp.grantee_principal_id = u.principal_id and dp.type = N'CO'
WHERE (u.type in ('U', 'S', 'G', 'C', 'K'))
ORDER BY [Name] ASC", new string[] { "iP0" }, databaseModel.Name)),
					(r => new Ordinal(r)),
					((ordinal, r) => new DatabaseSecurityLoginModel(ordinal, r))
				) { }
		}
		#endregion

		///// <summary>
		///// Fetches the login.
		///// </summary>
		//public static void FetchLogin(DatabaseModel databaseModel, AsyncStatus status, NamespaceSnapInBase snapIn, System.Action<ModelBase[]> action, object[] actionParam)
		//{
		//    //+ define buffer
		//    ModelBase[] buffer = new ModelBase[10];
		//    int bufferFlush = buffer.Length - 1;
		//    int bufferIndex = 0;
		//    //+ fetch table
		//    status.ReportProgress(0, 0, "Fetching...");
		//    DataRowCollection dataRowList = FetchLoginTable(databaseModel).Tables[0].Rows;
		//    int dataRowCount = dataRowList.Count;
		//    status.ReportProgress(0, dataRowCount, "Loading...");
		//    //+ parse table
		//    for (int dataRowIndex = 0; dataRowIndex < dataRowCount; dataRowIndex++)
		//    {
		//        System.Data.DataRow dataRow = dataRowList[dataRowIndex];
		//        buffer[bufferIndex] = new LoginModel(dataRow);
		//        if (bufferIndex < bufferFlush)
		//        {
		//            bufferIndex++;
		//            continue;
		//        }
		//        //+ new scope nodes must be created on the snapin's thread.
		//        snapIn.BeginInvoke(action, new object[] { buffer });
		//        status.ReportProgress(dataRowIndex + 1, dataRowCount, "Loading...");
		//        bufferIndex = 0;
		//    }
		//    if (bufferIndex > 0)
		//    {
		//        ModelBase[] partialBuffer = new ModelBase[bufferIndex];
		//        System.Array.Copy(buffer, partialBuffer, bufferIndex);
		//        //+ new scope nodes must be created on the snapin's thread.
		//        snapIn.BeginInvoke(action, new object[] { partialBuffer });
		//    }
		//    status.Complete("Load complete.", true);
		//}
	}
}
