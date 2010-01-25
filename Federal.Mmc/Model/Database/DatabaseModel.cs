using System.Data;
using System.Data.SqlClient;

namespace Federal.Model.Database
{
	/// <summary>
	/// Database Model
	/// </summary>
	public class DatabaseModel : ModelBase
	{
		public class Ordinal
		{
			public int Database_Name;
			public Ordinal(SqlDataReader r)
			{
				Database_Name = r.GetOrdinal("Database_Name");
			}
		}

		/// <summary>
		/// The constructor.
		/// </summary>
		public DatabaseModel(Ordinal ordinal, SqlDataReader r)
		{
			Name = r.Field<string>(ordinal.Database_Name);
			Status = DatabaseStatus.Default;
			HasAccess = false;
			NeuroxContext = null;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is has access.
		/// Filled with FetchDatabaseAccess method
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is has access; otherwise, <c>false</c>.
		/// </value>
		public bool HasAccess { get; protected set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; protected set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		public DatabaseStatus Status { get; protected set; }

		/// <summary>
		/// Gets or sets the neurox context.
		/// </summary>
		/// <value>The neurox context.</value>
		public string NeuroxContext { get; protected set; }

		/// <summary>
		/// Sets the access.
		/// </summary>
		/// <param name="model">The model.</param>
		internal void SetAccess(Access access)
		{
			HasAccess = access.HasAccess;
			NeuroxContext = access.NeuroxContext;
		}

		#region Access
		/// <summary>
		/// 
		/// </summary>
		public class Access
		{
			public Access(SqlDataReader r)
			{
				int isAccessibleOrdinal = r.GetOrdinal("IsAccessible");
				HasAccess = r.Field<bool>(isAccessibleOrdinal);
				r.NextResult();
				int nameOrdinal = r.GetOrdinal("Name");
				int valueOrdinal = r.GetOrdinal("Value");
				while (r.Read())
				{
					switch (r.Field<string>(nameOrdinal))
					{
						case "NeuroxContext":
							NeuroxContext = r.Field<string>(valueOrdinal);
							break;
					}
				}
			}
			public bool HasAccess { get; protected set; }
			public string NeuroxContext { get; protected set; }
			/// <summary>
			/// 
			/// </summary>
			public class Fetch : SingleFetch<Fetch, Access>
			{
				public Fetch(DatabaseModel model)
					: base(
						(() => KernelDatabase.ReplaceSqlValue(@"
USE [:P0:]
SELECT CAST(has_dbaccess(dtb.name) AS bit) AS [IsAccessible]
FROM master.sys.databases AS dtb
WHERE (dtb.name=[:P1:])
SELECT p.name AS [Name],
CAST(p.value AS sql_variant) AS [Value]
FROM sys.extended_properties AS p
WHERE (p.major_id=0 AND p.minor_id=0 AND p.class=0)and(p.name=N'NeuroxContext')", new string[] { "iP0", "cP1" }, model.Name, model.Name)),
						((r) => new Access(r))
					) { }
			}
		}
		#endregion

		#region Fetch
		/// <summary>
		/// 
		/// </summary>
		public class Fetch : CollectionChunkedFetch<Fetch, Ordinal, DatabaseModel>
		{
			public Fetch()
				: base(
					(() => @"
SELECT dtb.name AS [Database_Name], 'Server[@Name=' + quotename(CAST(serverproperty(N'Servername') AS sysname),'''') + ']' + '/Database[@Name=' + quotename(dtb.name,'''') + ']' AS [Database_Urn],
case when DATABASEPROPERTY(dtb.name,'IsShutDown') is null then 0x200 else 0 end |
case when 1 = dtb.is_in_standby then 0x40 else 0 end |
case when 1 = dtb.is_cleanly_shutdown then 0x80 else 0 end |
case dtb.state when 1 then 0x2 when 2 then 0x8 when 3 then 0x4 when 4 then 0x10 when 5 then 0x100 when 6 then 0x20 else 1 end
AS [Database_Status],
dtb.compatibility_level AS [Database_CompatibilityLevel],
dmi.mirroring_role AS [Database_MirroringRole],
coalesce(dmi.mirroring_state + 1, 0) AS [Database_MirroringStatus],
dtb.recovery_model AS [RecoveryModel],
dtb.user_access AS [UserAccess],
dtb.is_read_only AS [ReadOnly],
dtb.name AS [Database_DatabaseName2]
FROM master.sys.databases AS dtb
LEFT OUTER JOIN sys.database_mirroring AS dmi ON dmi.database_id = dtb.database_id
WHERE (CAST(case when dtb.name in ('master','model','msdb','tempdb') then 1 else dtb.is_distributor end AS bit)=0 and CAST(isnull(dtb.source_database_id, 0) AS bit)=0)
ORDER BY [Database_Name] ASC"),
					(r => new Ordinal(r)),
					((ordinal, r) => new DatabaseModel(ordinal, r))
				) { }
		}
		#endregion
	}
}
