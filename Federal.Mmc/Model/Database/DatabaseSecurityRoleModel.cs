using System.Data.SqlClient;

namespace Federal.Model.Database.Security
{
	/// <summary>
	/// Role Model
	/// </summary>
	public class DatabaseSecurityRoleModel : ModelBase
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
		public DatabaseSecurityRoleModel(Ordinal ordinal, SqlDataReader r)
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
		public class Fetch : CollectionChunkedFetch<Fetch, Ordinal, DatabaseSecurityRoleModel>
		{
			public Fetch(DatabaseModel databaseModel)
				: base(
					(() => KernelDatabase.ReplaceSqlValue(@"
USE [:P0:]
SELECT rl.name AS [Name], 'Server[@Name=' + quotename(CAST(serverproperty(N'Servername') AS sysname),'''') + ']' + '/Database[@Name=' + quotename(db_name(),'''') + ']' + '/Role[@Name=' + quotename(rl.name,'''') + ']' AS [Urn],
rl.principal_id AS [ID], CAST(CASE WHEN rl.principal_id > 16383 AND rl.principal_id < 16400 THEN 1 ELSE 0 END AS bit) AS [IsFixedRole], rl.create_date AS [CreateDate], ou.name AS [Owner]
FROM sys.database_principals AS rl
INNER JOIN sys.database_principals AS ou ON ou.principal_id = rl.owning_principal_id
WHERE (rl.type = 'R')
ORDER BY [Name] ASC", new string[] { "iP0" }, databaseModel.Name)),
					(r => new Ordinal(r)),
					((ordinal, r) => new DatabaseSecurityRoleModel(ordinal, r))
				) { }

		}
		#endregion
	}
}
